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

		Task ReceiveNextAsync(
			Action onOpen,
			Action<TMessage> onMessage,
			Action<Exception> onError,
			Action<OnCloseContext> onClose,
			CancellationToken? cancellationToken = null
		);

		Task CloseAsync();

		WebSocketState State { get; }

		bool IsConnectionEstablished => this.State == WebSocketState.Open;
	}

}
