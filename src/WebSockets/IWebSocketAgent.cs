using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace EdjCase.ICP.WebSockets
{
	public interface IWebSocketAgent<TMessage> : IAsyncDisposable
		where TMessage : notnull
	{
		Task ConnectAsync(
			CancellationToken? cancellationToken = null
		);

		Task SendAsync(
			TMessage message,
			CancellationToken? cancellationToken = null
		);

		Task ReceiveNextAsync(CancellationToken? cancellationToken = null);

		Task CloseAsync();

		WebSocketState State { get; }

		public bool IsConnectionEstablished => this.State == WebSocketState.Open;

		public async Task ReceiveAllAsync(CancellationToken? cancellationToken = null)
		{
			while (cancellationToken?.IsCancellationRequested != true
				&& this.IsConnectionEstablished == true)
			{
				await this.ReceiveNextAsync(cancellationToken);
			}
		}
	}

}
