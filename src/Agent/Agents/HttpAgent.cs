using Agent.Cbor;
using Dahomey.Cbor;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Path = EdjCase.ICP.Candid.Models.Path;

namespace EdjCase.ICP.Agent.Agents
{
	public class HttpAgent : IAgent
	{
		private const string CBOR_CONTENT_TYPE = "application/cbor";

		private static readonly Lazy<CborOptions> cborOptionsLazy = new Lazy<CborOptions>(() =>
		{
			var options = new CborOptions();
			var provider = new CborConverterProvider();
			options.Registry.ConverterRegistry.RegisterConverterProvider(provider);
			options.Registry.ConverterRegistry.RegisterConverter(typeof(IHashable), new HashableCborConverter(options.Registry.ConverterRegistry));
			return options;
		}, isThreadSafe: true);

		public IIdentity Identity { get; }

		private readonly HttpClient httpClient;

		public HttpAgent(IIdentity identity, Uri? boundryCanisterUrl = null)
		{
			this.Identity = identity;
			this.httpClient = new HttpClient
			{
				BaseAddress = boundryCanisterUrl ?? new Uri("https://ic0.app/")
			};
			this.httpClient.DefaultRequestHeaders
				.Accept
				.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(CBOR_CONTENT_TYPE));
		}

		public async Task<RequestId> CallAsync(Principal canisterId, string method, CandidArg encodedArgument, Principal? targetCanisterOverride = null, IIdentity? identityOverride = null)
		{
			if (targetCanisterOverride == null)
			{
				targetCanisterOverride = canisterId;
			}
			return await this.SendWithNoResponseAsync($"/api/v2/canister/{targetCanisterOverride.ToText()}/call", BuildRequest, identityOverride);

			CallRequest BuildRequest(Principal sender, ICTimestamp now)
			{
				return new CallRequest(canisterId, method, encodedArgument, sender, now);
			}
		}

		public async Task<RequestStatus?> GetRequestStatusAsync(Principal canisterId, RequestId id)
		{
			var pathRequestStatus = Path.FromSegments("request_status", id.RawValue);
			var paths = new List<Path> { pathRequestStatus }; ReadStateResponse response = await this.ReadStateAsync(canisterId, paths);
			HashTree? requestStatus = response.Certificate.Tree.GetValue(pathRequestStatus);
			string? status = requestStatus?.GetValue("status")?.AsLeaf().AsUtf8();
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
					Blob r = requestStatus!.GetValue("reply")!.AsLeaf();
					return RequestStatus.Replied(CandidArg.FromBytes(r.Value));
				case "rejected":
					UnboundedUInt code = requestStatus!.GetValue("reject_code")!.AsLeaf().AsNat();
					string message = requestStatus.GetValue("reject_message")!.AsLeaf().AsUtf8();
					string? errorCode = requestStatus.GetValue("error_code")?.AsLeaf().AsUtf8();
					return RequestStatus.Rejected(code, message, errorCode);
				case "done":
					return RequestStatus.Done();
				default:
					throw new NotImplementedException($"Invalid request status '{status}'");
			}
		}

		public Principal GetPrincipal()
		{
			return this.Identity.GetPrincipal();
		}

		public async Task<Key> GetRootKeyAsync()
		{
			// TODO cache
			StatusResponse jsonObject = await this.GetStatusAsync();
			return jsonObject.DevelopmentRootKey ?? throw new NotImplementedException("Get root key from trusted source");
		}

		public async Task<StatusResponse> GetStatusAsync()
		{
			return await this.SendAsync<StatusResponse>("/api/v2/status");
		}

		public async Task<QueryResponse> QueryAsync(Principal canisterId, string method, CandidArg arg, IIdentity? identityOverride = null)
		{
			return await this.SendAsync<QueryRequest, QueryResponse>($"/api/v2/canister/{canisterId.ToText()}/query", BuildRequest, identityOverride);

			QueryRequest BuildRequest(Principal sender, ICTimestamp now)
			{
				return new QueryRequest(canisterId, method, arg, sender, now);
			}
		}

		public async Task<ReadStateResponse> ReadStateAsync(Principal canisterId, List<Path> paths, IIdentity? identityOverride = null)
		{
			return await this.SendAsync<ReadStateRequest, ReadStateResponse>($"/api/v2/canister/{canisterId.ToText()}/read_state", BuildRequest, identityOverride);

			ReadStateRequest BuildRequest(Principal sender, ICTimestamp now)
			{
				return new ReadStateRequest(paths, sender, now);
			}
		}


		private async Task<RequestId> SendWithNoResponseAsync<TRequest>(string url, Func<Principal, ICTimestamp, TRequest> getRequest, IIdentity? identityOverride)
			where TRequest : IRepresentationIndependentHashItem
		{
			(Func<Task<Stream>> streamFunc, RequestId requestId) = await this.SendInternalAsync(url, getRequest, identityOverride);

			return requestId;
		}

		private async Task<TResponse> SendAsync<TResponse>(string url)
		{
			Func<Task<Stream>> streamFunc = await this.SendRawAsync(url, null);
			Stream stream = await streamFunc();
			return await Dahomey.Cbor.Cbor.DeserializeAsync<TResponse>(stream, HttpAgent.cborOptionsLazy.Value);
		}

		private async Task<TResponse> SendAsync<TRequest, TResponse>(string url, Func<Principal, ICTimestamp, TRequest> getRequest, IIdentity? identityOverride)
			where TRequest : IRepresentationIndependentHashItem
		{
			(Func<Task<Stream>> streamFunc, RequestId requestId) = await this.SendInternalAsync(url, getRequest, identityOverride);
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

		private async Task<(Func<Task<Stream>> ResponseFunc, RequestId RequestId)> SendInternalAsync<TRequest>(string url, Func<Principal, ICTimestamp, TRequest> getRequest, IIdentity? identityOverride)
			where TRequest : IRepresentationIndependentHashItem
		{
			if (identityOverride == null)
			{
				identityOverride = this.Identity;
			}
			TRequest request = getRequest(identityOverride.GetPrincipal(), ICTimestamp.Now());
			Dictionary<string, IHashable> content = request.BuildHashableItem();
			SignedContent signedContent = identityOverride.CreateSignedContent(content);


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


