using Agent.Cbor;
using Dahomey.Cbor;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Agent.Cbor.Converters;

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

		private static readonly Lazy<CborOptions> cborOptionsLazy = new Lazy<CborOptions>(() =>
		{
			var options = new CborOptions();
			var provider = new CborConverterProvider();
			options.Registry.ConverterRegistry.RegisterConverterProvider(provider);
			options.Registry.ConverterRegistry.RegisterConverter(typeof(IHashable), new HashableCborConverter(options));
			return options;
		}, isThreadSafe: true);

		/// <summary>
		/// The identity that will be used on each request unless overriden
		/// This identity can be anonymous
		/// </summary>
		public IIdentity? Identity { get; set; }

		private readonly HttpClient httpClient;

		/// <param name="identity">Optional. Identity to use for each request. If unspecified, will use anonymous identity</param>
		/// <param name="httpBoundryNodeUrl">Url to the boundry node to connect to. Defaults to `https://ic0.app/`</param>
		public HttpAgent(IIdentity? identity = null, Uri? httpBoundryNodeUrl = null)
		{
			this.Identity = identity;
			this.httpClient = new HttpClient
			{
				BaseAddress = httpBoundryNodeUrl ?? new Uri("https://ic0.app/")
			};
			this.httpClient.DefaultRequestHeaders
				.Accept
				.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(CBOR_CONTENT_TYPE));
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
			return await this.SendWithNoResponseAsync($"/api/v2/canister/{effectiveCanisterId.ToText()}/call", BuildRequest);

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
			return await this.SendAsync<QueryRequest, QueryResponse>($"/api/v2/canister/{canisterId.ToText()}/query", BuildRequest);

			QueryRequest BuildRequest(Principal sender, ICTimestamp now)
			{
				return new QueryRequest(canisterId, method, arg, sender, now);
			}
		}

		/// <inheritdoc/>
		public async Task<ReadStateResponse> ReadStateAsync(Principal canisterId, List<StatePath> paths)
		{
			string url = $"/api/v2/canister/{canisterId.ToText()}/read_state";
			ReadStateResponse response = await this.SendAsync<ReadStateRequest, ReadStateResponse>(url, BuildRequest);

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
				this.rootKeyCache = jsonObject.DevelopmentRootKey ?? HttpAgent.mainnetRootKey;
			}
			return this.rootKeyCache;
		}


		/// <inheritdoc/>
		public async Task<StatusResponse> GetReplicaStatusAsync()
		{
			return await this.SendAsync<StatusResponse>("/api/v2/status");
		}




		private async Task<RequestId> SendWithNoResponseAsync<TRequest>(string url, Func<Principal, ICTimestamp, TRequest> getRequest)
			where TRequest : IRepresentationIndependentHashItem
		{
			(Func<Task<Stream>> streamFunc, RequestId requestId) = await this.SendInternalAsync(url, getRequest);

			return requestId;
		}

		private async Task<TResponse> SendAsync<TResponse>(string url)
		{
			Func<Task<Stream>> streamFunc = await this.SendRawAsync(url, null);
			Stream stream = await streamFunc();

			return await Dahomey.Cbor.Cbor.DeserializeAsync<TResponse>(stream, HttpAgent.cborOptionsLazy.Value);
		}

		private async Task<TResponse> SendAsync<TRequest, TResponse>(string url, Func<Principal, ICTimestamp, TRequest> getRequest)
			where TRequest : IRepresentationIndependentHashItem
		{
			(Func<Task<Stream>> streamFunc, RequestId requestId) = await this.SendInternalAsync(url, getRequest);
			Stream stream = await streamFunc();
#if DEBUG
			string cborHex;
			using (var memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				byte[] cborBytes = memoryStream.ToArray();
				cborHex = ByteUtil.ToHexString(cborBytes);
			}
			stream.Position = 0;
#endif
			return await Dahomey.Cbor.Cbor.DeserializeAsync<TResponse>(stream, HttpAgent.cborOptionsLazy.Value);
		}

		private async Task<(Func<Task<Stream>> ResponseFunc, RequestId RequestId)> SendInternalAsync<TRequest>(string url, Func<Principal, ICTimestamp, TRequest> getRequest)
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
				signedContent = await this.Identity.SignContentAsync(content);
			}


			byte[] cborBody = this.SerializeSignedContent(signedContent);
#if DEBUG
			string hex = ByteUtil.ToHexString(cborBody);
#endif
			Func<Task<Stream>> responseFunc = await this.SendRawAsync(url, cborBody);
			var sha256 = SHA256HashFunction.Create();
			RequestId requestId = RequestId.FromObject(content, sha256); // TODO this is redundant, `CreateSignedContent` hashes it too
			return (responseFunc, requestId);

		}

		private byte[] SerializeSignedContent(SignedContent signedContent)
		{
			using (ByteBufferWriter bufferWriter = new ByteBufferWriter())
			{
				var writer = new CborWriter(bufferWriter);
				writer.WriteSemanticTag(55799);
				IBufferWriter<byte> b = bufferWriter;
				Dahomey.Cbor.Cbor.Serialize(signedContent, in b, HttpAgent.cborOptionsLazy.Value);
				return bufferWriter.WrittenSpan.ToArray();
			}
		}

		private async Task<Func<Task<Stream>>> SendRawAsync(string url, byte[]? cborBody = null)
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
			}
			else
			{
				request = new HttpRequestMessage(HttpMethod.Get, url);

			}
			HttpResponseMessage response = await this.httpClient.SendAsync(request);
			if (!response.IsSuccessStatusCode)
			{
				byte[] bytes = await response.Content.ReadAsByteArrayAsync();
				string message = Encoding.UTF8.GetString(bytes);
				throw new Exception($"Response returned a failed status. HttpStatusCode={response.StatusCode} Reason={response.ReasonPhrase} Message={message}");
			}
			return async () => await response.Content.ReadAsStreamAsync();
		}
	}
}


