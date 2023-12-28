using System.Threading.Tasks;
using System.Threading;
using System;


namespace EdjCase.ICP.WebSockets
{
	/// <summary>
	/// Represents a WebSocket client.
	/// </summary>
	public interface IWebSocketClient : IDisposable
	{
		/// <summary>
		/// Gets a value indicating whether the WebSocket connection is open.
		/// </summary>
		bool IsOpen { get; }

		/// <summary>
		/// Connects to the specified WebSocket gateway
		/// </summary>
		/// <param name="gatewayUri">The URI of the WebSocket gateway.</param>
		/// <param name="cancellationToken">To cancel connection in mid operation</param>
		Task ConnectAsync(Uri gatewayUri, CancellationToken cancellationToken);

		/// <summary>
		/// Sends a message to the WebSocket server
		/// </summary>
		/// <param name="messageBytes">The message to send as a byte array.</param>
		/// <param name="cancellationToken">To cancel sending in mid operation</param>
		Task SendAsync(byte[] messageBytes, CancellationToken cancellationToken);

		/// <summary>
		/// Receives the next message from the WebSocket server
		/// </summary>
		/// <param name="cancellationToken">To cancel receiving in mid operation</param>
		/// <returns>The received bytes and a flag indicating if the message is a close message.</returns>
		Task<(byte[] Data, bool IsCloseMessage)> ReceiveAsync(CancellationToken cancellationToken);

		/// <summary>
		/// Closes the WebSocket connection
		/// </summary>
		/// <param name="cancellationToken">To cancel closing in mid operation</param>
		/// <param name="policyViolationMessage">The optional policy violation message.</param>
		Task CloseAsync(CancellationToken cancellationToken, string? policyViolationMessage = null);
	}
}
