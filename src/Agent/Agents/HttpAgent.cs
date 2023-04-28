using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Agent.Agents.Http;
using System.Net.Http.Headers;
using System.Formats.Cbor;
using EdjCase.ICP.Candid.Encodings;
using System.Linq;

namespace EdjCase.ICP.Agent.Agents
{
	/// <summary>
	/// An `IAgent` implementation using HTTP to make requests to the IC
	/// </summary>
	public class HttpAgent : IAgent
	{
		private const string CBOR_CONTENT_TYPE = "application/cbor";
		private static byte[] mainnetRootKey = ByteUtil.FromHexString("308182301d060d2b0601040182dc7c0503010201060c2b0601040182dc7c05030201036100814c0e6ec71fab583b08bd81373c255c3c371b2e84863c98a4f1e08b74235d14fb5d9c0cd546d9685f913a0c0b2cc5341583bf4b4392e467db96d65b9bb4cb717112f8472e0d5a4d14505ffd7484b01291091c5f87b98883463f98091a0baaae");
		private byte[]? rootKeyCache = null;

		/// <summary>
		/// The identity that will be used on each request unless overriden
		/// This identity can be anonymous
		/// </summary>
		public IIdentity? Identity { get; set; }

		private readonly IHttpClient httpClient;

		/// <param name="identity">Optional. Identity to use for each request. If unspecified, will use anonymous identity</param>
		/// <param name="httpClient">Optional. Sets the http client to use, otherwise will use the default http client</param>
		public HttpAgent(IHttpClient httpClient, IIdentity? identity = null)
		{
			this.Identity = identity;
			this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		}

		/// <param name="identity">Optional. Identity to use for each request. If unspecified, will use anonymous identity</param>
		/// <param name="httpBoundryNodeUrl">Url to the boundry node to connect to. Defaults to `https://ic0.app/`</param>
		public HttpAgent(IIdentity? identity = null, Uri? httpBoundryNodeUrl = null)
		{
			this.Identity = identity;
			this.httpClient = new DefaultHttpClient(new HttpClient()
			{
				BaseAddress = httpBoundryNodeUrl ?? new Uri("https://ic0.app/")
			});
		}


		/// <inheritdoc/>
		public async Task<RequestId> CallAsync(
			Principal canisterId,
			string method,
			CandidArg arg,
			Principal? effectiveCanisterId = null)
		{
			if (effectiveCanisterId == null)
			{
				effectiveCanisterId = canisterId;
			}
			(CallRejectedResponse? response, RequestId requestId) = await this.SendAsync($"/api/v2/canister/{effectiveCanisterId.ToText()}/call", BuildRequest, async httpResponse =>
			{
				await this.ThrowIfError(httpResponse);
				if (httpResponse.StatusCode == System.Net.HttpStatusCode.OK)
				{
					// If returns with a body, then an error happened https://forum.dfinity.org/t/breaking-changes-to-the-replica-api-agent-developers-take-note/19651

					byte[] cborBytes = await httpResponse.Content.ReadAsByteArrayAsync();
					var reader = new CborReader(cborBytes);
					return CallRejectedResponse.FromCbor(reader);
				}
				return null;
			});
			if(response != null)
			{
				throw new CallRejectedException(response.Code, response.Message, response.ErrorCode);
			}
			return requestId;

			CallRequest BuildRequest(Principal sender, ICTimestamp now)
			{
				return new CallRequest(canisterId, method, arg, sender, now);
			}
		}

		/// <inheritdoc/>
		public async Task<QueryResponse> QueryAsync(
			Principal canisterId,
			string method,
			CandidArg arg)
		{
			(QueryResponse response, RequestId requestId) = await this.SendAsync($"/api/v2/canister/{canisterId.ToText()}/query", BuildRequest, async httpResponse =>
			{
				await this.ThrowIfError(httpResponse);
				byte[] cborBytes = await httpResponse.Content.ReadAsByteArrayAsync();
				return QueryResponse.ReadCbor(new CborReader(cborBytes));
			});
			return response;

			QueryRequest BuildRequest(Principal sender, ICTimestamp now)
			{
				return new QueryRequest(canisterId, method, arg, sender, now);
			}
		}

		/// <inheritdoc/>
		public async Task<ReadStateResponse> ReadStateAsync(Principal canisterId, List<StatePath> paths)
		{
			string url = $"/api/v2/canister/{canisterId.ToText()}/read_state";
			(ReadStateResponse response, RequestId requestId) = await this.SendAsync(url, BuildRequest, async httpResponse =>
			{
				await this.ThrowIfError(httpResponse);
				byte[] cborBytes = await httpResponse.Content.ReadAsByteArrayAsync();
				var reader = new CborReader(cborBytes);
				return ReadStateResponse.ReadCbor(reader);
			});

			byte[] rootPublicKey = await this.GetRootKeyAsync();
			if (!response.Certificate.IsValid(rootPublicKey))
			{
				throw new InvalidCertificateException("Certificate signature does not match the IC public key");
			}

			return response;

			ReadStateRequest BuildRequest(Principal sender, ICTimestamp now)
			{
				return new ReadStateRequest(paths, sender, now);
			}
		}

		/// <inheritdoc/>
		public async Task<RequestStatus?> GetRequestStatusAsync(Principal canisterId, RequestId id)
		{
			var pathRequestStatus = StatePath.FromSegments("request_status", id.RawValue);
			var paths = new List<StatePath> { pathRequestStatus };
			ReadStateResponse response = await this.ReadStateAsync(canisterId, paths);
			HashTree? requestStatus = response.Certificate.Tree.GetValueOrDefault(pathRequestStatus);
			string? status = requestStatus?.GetValueOrDefault("status")?.AsLeaf().AsUtf8();
			//received, processing, replied, rejected or done
			switch (status)
			{
				case null:
					return null;
				case "received":
					return RequestStatus.Received();
				case "processing":
					return RequestStatus.Processing();
				case "replied":
					HashTree.EncodedValue r = requestStatus!.GetValueOrDefault("reply")!.AsLeaf();
					return RequestStatus.Replied(CandidArg.FromBytes(r));
				case "rejected":
					RejectCode code = (RejectCode)(ulong)requestStatus!.GetValueOrDefault("reject_code")!.AsLeaf().AsNat();
					string message = requestStatus.GetValueOrDefault("reject_message")!.AsLeaf().AsUtf8();
					string? errorCode = requestStatus.GetValueOrDefault("error_code")?.AsLeaf().AsUtf8();
					return RequestStatus.Rejected(code, message, errorCode);
				case "done":
					return RequestStatus.Done();
				default:
					throw new NotImplementedException($"Invalid request status '{status}'");
			}
		}


		/// <inheritdoc/>
		public async Task<byte[]> GetRootKeyAsync()
		{
			if (this.rootKeyCache == null)
			{
				StatusResponse jsonObject = await this.GetReplicaStatusAsync();
				this.rootKeyCache = jsonObject.DevelopmentRootKey ?? mainnetRootKey;
			}
			return this.rootKeyCache;
		}


		/// <inheritdoc/>
		public async Task<StatusResponse> GetReplicaStatusAsync()
		{
			return await this.SendRawAsync("/api/v2/status", null, async response =>
			{
				byte[] bytes = await response.Content.ReadAsByteArrayAsync();
				return StatusResponse.ReadCbor(new CborReader(bytes));
			});
		}

		private async Task ThrowIfError(HttpResponseMessage response)
		{
			if (!response.IsSuccessStatusCode)
			{
				byte[] bytes = await response.Content.ReadAsByteArrayAsync();
				string message = Encoding.UTF8.GetString(bytes);
				throw new Exception($"Response returned a failed status. HttpStatusCode={response.StatusCode} Reason={response.ReasonPhrase} Message={message}");
			}
		}

		private async Task<(T Response, RequestId RequestId)> SendAsync<T, TRequest>(
			string url,
			Func<Principal, ICTimestamp, TRequest> getRequest,
			Func<HttpResponseMessage, Task<T>> handleResponse
		)
			where TRequest : IRepresentationIndependentHashItem
		{
			Principal principal;
			if (this.Identity == null)
			{
				principal = Principal.Anonymous();
			}
			else
			{
				DerEncodedPublicKey publicKey = this.Identity.GetPublicKey();
				principal = Principal.SelfAuthenticating(publicKey.Value);
			}
			TRequest request = getRequest(principal, ICTimestamp.Now());
			Dictionary<string, IHashable> content = request.BuildHashableItem();

			SignedContent signedContent;
			if (this.Identity == null)
			{
				signedContent = new SignedContent(content, null, null, null);
			}
			else
			{
				signedContent = this.Identity.SignContent(content);
			}


			byte[] cborBody = this.SerializeSignedContent(signedContent);
#if DEBUG
			string hex = ByteUtil.ToHexString(cborBody);
#endif
			T response = await this.SendRawAsync(url, cborBody, handleResponse);
			var sha256 = SHA256HashFunction.Create();
			RequestId requestId = RequestId.FromObject(content, sha256); // TODO this is redundant, `CreateSignedContent` hashes it too
			return (response, requestId);

		}

		private byte[] SerializeSignedContent(SignedContent signedContent)
		{
			var writer = new CborWriter();
			writer.WriteTag(CborTag.SelfDescribeCbor);
			signedContent.WriteCbor(writer);
			return writer.Encode();
		}

		private async Task<T> SendRawAsync<T>(string url, byte[]? cborBody, Func<HttpResponseMessage, Task<T>> handleResponse)
		{
			HttpRequestMessage request;
			if (cborBody != null)
			{
				var content = new ByteArrayContent(cborBody);
				content.Headers.Remove("Content-Type");
				content.Headers.Add("Content-Type", CBOR_CONTENT_TYPE);
				request = new HttpRequestMessage(HttpMethod.Post, url)
				{
					Content = content
				};
				request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CBOR_CONTENT_TYPE));
			}
			else
			{
				request = new HttpRequestMessage(HttpMethod.Get, url);

			}
			HttpResponseMessage response = await this.httpClient.SendAsync(request);
			return await handleResponse(response);
		}
	}

}


