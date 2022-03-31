using Dfinity.Agent.Auth;
using Dfinity.Agent.Requests;
using Dfinity.Agent.Responses;
using Dfinity.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Path = Dfinity.Common.Models.Path;

namespace Dfinity.Agent.Agents
{
	public class HttpAgent : IAgent
    {
        private const string CBOR_CONTENT_TYPE = "application/cbor";

        public IIdentity Identity { get; }

        private readonly HttpClient httpClient;

        public HttpAgent(IIdentity identity, Uri? boundryCanisterUrl = null)
        {
            this.Identity = identity;
            this.httpClient = new HttpClient
			{
                BaseAddress = boundryCanisterUrl
			};
            this.httpClient.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(CBOR_CONTENT_TYPE));
        }

        public async Task CallAsync(PrincipalId canisterId, string method, EncodedArgument encodedArgument, PrincipalId? targetCanisterOverride = null, IIdentity? identityOverride = null)
        {
            if (targetCanisterOverride == null)
            {
                targetCanisterOverride = canisterId;
            }
            await this.SendWithNoResponseAsync($"/api/v2/canister/{targetCanisterOverride.ToText()}/query", BuildRequest, identityOverride);

            CallRequest BuildRequest(PrincipalId sender, ICTimestamp now)
            {
                return new CallRequest(canisterId, method, encodedArgument, sender, now);
            }
        }

        public PrincipalId GetPrincipal()
        {
            return this.Identity.Principal;
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

        public async Task<QueryResponse> QueryAsync(PrincipalId canisterId, string method, EncodedArgument encodedArgument, IIdentity? identityOverride = null)
        {
            return await this.SendAsync<QueryRequest, QueryResponse>($"/api/v2/canister/{canisterId.ToText()}/query", BuildRequest, identityOverride);

            QueryRequest BuildRequest(PrincipalId sender, ICTimestamp now)
            {
                return new QueryRequest(canisterId, method, encodedArgument, sender, now);
            }
        }

        public async Task<ReadStateResponse> ReadStateAsync(PrincipalId canisterId, List<Path> paths, IIdentity? identityOverride)
        {
            return await this.SendAsync<ReadStateRequest, ReadStateResponse>($"/api/v2/canister/{canisterId.ToText()}/read_state", BuildRequest, identityOverride);

            ReadStateRequest BuildRequest(PrincipalId sender, ICTimestamp now)
            {
                return new ReadStateRequest(paths, sender, now);
            }
        }


        private async Task SendWithNoResponseAsync<TRequest>(string url, Func<PrincipalId, ICTimestamp, TRequest> getRequest, IIdentity? identityOverride)
            where TRequest : IRepresentationIndependentHashItem
        {
            _ = await this.SendInternalAsync(url, getRequest, identityOverride);
        }

        private async Task<TResponse> SendAsync<TResponse>(string url)
        {
            Func<Task<Stream>> streamFunc = await this.SendRawAsync(url, null);
            Stream stream = await streamFunc();
            return await CborUtil.DeserializeAsync<TResponse>(stream);
        }

        private async Task<TResponse> SendAsync<TRequest, TResponse>(string url, Func<PrincipalId, ICTimestamp, TRequest> getRequest, IIdentity? identityOverride)
            where TRequest : IRepresentationIndependentHashItem
        {
            Func<Task<Stream>> streamFunc = await this.SendInternalAsync(url, getRequest, identityOverride);
            Stream stream = await streamFunc();
            return await CborUtil.DeserializeAsync<TResponse>(stream);
        }

        private async Task<Func<Task<Stream>>> SendInternalAsync<TRequest>(string url, Func<PrincipalId, ICTimestamp, TRequest> getRequest, IIdentity? identityOverride)
            where TRequest : IRepresentationIndependentHashItem
        {
            if (identityOverride == null)
            {
                identityOverride = this.Identity;
            }
            TRequest request = getRequest(identityOverride.Principal, ICTimestamp.Now());
            Dictionary<string, IHashable> content = request.BuildHashableItem();
            SignedContent signedContent = identityOverride.CreateSignedContent(content);

            byte[] cborBody = CborUtil.Serialize(signedContent);

            return await this.SendRawAsync(url, cborBody);
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
                throw new Exception($"Response returned a failed status. HttpStatusCode={response.StatusCode} Reason={response.ReasonPhrase}");
            }
            return async () => await response.Content.ReadAsStreamAsync();
        }
    }
}


