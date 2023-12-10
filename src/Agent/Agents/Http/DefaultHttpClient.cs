using EdjCase.ICP.Candid.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Agents.Http
{
	/// <summary>
	/// The default http client to use with the built in `HttpClient`
	/// </summary>
	public class DefaultHttpClient : IHttpClient
	{
		private const string CBOR_CONTENT_TYPE = "application/cbor";

		private readonly HttpClient httpClient;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="client">HTTP client to use</param>
		public DefaultHttpClient(HttpClient client)
		{
			this.httpClient = client;
		}

		/// <inheritdoc />
		public async Task<HttpResponse> GetAsync(
			string url,
			CancellationToken? cancellationToken = null
		)
		{
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			return await this.SendAsync(request, cancellationToken);
		}

		/// <inheritdoc />
		public async Task<HttpResponse> PostAsync(
			string url,
			byte[] cborBody,
			CancellationToken? cancellationToken = null
		)
		{
			var content = new ByteArrayContent(cborBody);
			content.Headers.Remove("Content-Type");
			content.Headers.Add("Content-Type", CBOR_CONTENT_TYPE);
			HttpRequestMessage request = new (HttpMethod.Post, url)
			{
				Content = content
			};
			return await this.SendAsync(request, cancellationToken);
		}

		private async Task<HttpResponse> SendAsync(HttpRequestMessage message, CancellationToken? cancellationToken = null)
		{
			message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CBOR_CONTENT_TYPE));

			HttpResponseMessage response = await this.httpClient.SendAsync(message, cancellationToken ?? CancellationToken.None);

			return new HttpResponse(response.StatusCode, response.Content.ReadAsByteArrayAsync);
		}
	}
}
