using System.Net.Http;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Agents.Http
{
	/// <summary>
	/// A simple http request interface for sending messages
	/// </summary>
	public interface IHttpClient
	{
		/// <summary>
		/// Sends an http request and awaits a response
		/// </summary>
		/// <param name="message">Http request to send</param>
		/// <returns>Response from the request</returns>
		Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);
	}
}
