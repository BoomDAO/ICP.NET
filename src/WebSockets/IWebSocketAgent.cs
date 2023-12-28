using System;
using System.Threading;
using System.Threading.Tasks;

namespace EdjCase.ICP.WebSockets
{
	/// <summary>
	/// Agent for the Internet Computer using websockets
	/// </summary>
	/// <typeparam name="TMessage">The type of the message that will be sent back and forth
	/// from client/server</typeparam>
	public interface IWebSocketAgent<TMessage> : IAsyncDisposable
		where TMessage : notnull
	{
		/// <summary>
		/// Connects to the server with a websocket connection. Will update `State` property
		/// when connected
		/// </summary>
		/// <param name="cancellationToken">Optional. Cancellation token used to stop the network requests before completion</param>
		/// <returns></returns>
		Task ConnectAsync(
			CancellationToken? cancellationToken = null
		);

		/// <summary>
		/// Sends a message to the server via websockets
		/// </summary>
		/// <param name="message">The message that should be sent to the server</param>
		/// <param name="cancellationToken">Optional. Cancellation token used to stop the network requests before completion</param>
		/// <returns></returns>
		Task SendAsync(
			TMessage message,
			CancellationToken? cancellationToken = null
		);

		/// <summary>
		/// Gets the next message from the server and processes it with the defined callbacks
		/// </summary>
		/// <param name="cancellationToken">Optional. Cancellation token used to stop the network requests before completion</param>
		Task ReceiveNextAsync(CancellationToken? cancellationToken = null);

		/// <summary>
		/// Closes the websocket connection. Will change the `State` property
		/// </summary>
		Task CloseAsync();

		/// <summary>
		/// Will return true if the connection is open and false otherwise
		/// </summary>
		public bool IsOpen { get; }

		/// <summary>
		/// Waits and receives all messages from the server until the connection is closed or
		/// the cancellation token is canceled
		/// </summary>
		/// <param name="cancellationToken">Optional. Cancellation token used to stop the loop and network requests before completion</param>
		/// <returns></returns>
		public async Task ReceiveAllAsync(CancellationToken? cancellationToken = null)
		{
			while (this.IsOpen)
			{
				if (cancellationToken?.IsCancellationRequested == true)
				{
					break;
				}
				await this.ReceiveNextAsync(cancellationToken);
			}
		}
	}

}
