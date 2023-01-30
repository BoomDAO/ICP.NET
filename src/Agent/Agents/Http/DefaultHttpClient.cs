using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Agents.Http
{
	/// <summary>
	/// The default http client to use with the built in `HttpClient`
	/// </summary>
	public class DefaultHttpClient : IHttpClient
	{
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
		public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
		{
			return await this.httpClient.SendAsync(message);
		}
	}
}
