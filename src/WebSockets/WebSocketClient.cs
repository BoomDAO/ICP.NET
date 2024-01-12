using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Buffers;
using System.IO;

namespace EdjCase.ICP.WebSockets
{
	internal class WebSocketClient : IWebSocketClient
	{
		private readonly ClientWebSocket innerClient;

		public WebSocketClient()
		{
			this.innerClient = new ClientWebSocket();
			this.innerClient.Options.KeepAliveInterval = TimeSpan.Zero;
		}

		public bool IsOpen => this.innerClient.State == WebSocketState.Open;


		public async Task ConnectAsync(Uri gatewayUri, CancellationToken cancellationToken)
		{
			try
			{
				await this.innerClient.ConnectAsync(gatewayUri, cancellationToken);
			}
			catch (WebSocketException ex)
			{
				throw new Exception("Failed to connect to server with websocket. ErrorCode: " + ex.WebSocketErrorCode);
			}
		}

		public async Task SendAsync(byte[] messageBytes, CancellationToken cancellationToken)
		{
			try
			{
				await this.innerClient.SendAsync(
					messageBytes,
					WebSocketMessageType.Binary,
					true,
					cancellationToken
				);
			}
			catch (WebSocketException ex)
			{
				throw new Exception("Failed to send message to server with websocket. ErrorCode: " + ex.WebSocketErrorCode);
			}
		}

		public async Task<(byte[] Data, bool IsCloseMessage)> ReceiveAsync(CancellationToken cancellationToken)
		{
			ArrayPool<byte> arrayPool = ArrayPool<byte>.Shared;
			byte[] buffer = arrayPool.Rent(4096); // Used shared memory from pool
			try
			{
				using (MemoryStream memoryStream = new())
				{
					while (true)
					{
						WebSocketReceiveResult result = await this.innerClient.ReceiveAsync(
							new ArraySegment<byte>(buffer),
							cancellationToken
						);

						if (result.Count > 0)
						{
							memoryStream.Write(buffer, 0, result.Count);
						}

						if (result.EndOfMessage)
						{
							bool isClosed = result.MessageType == WebSocketMessageType.Close;
							return (memoryStream.ToArray(), isClosed);
						}
					}
				}
			}
			finally
			{
				arrayPool.Return(buffer); // Return the buffer to the pool
			}
		}

		public async Task CloseAsync(
			CancellationToken cancellationToken,
			string? policyViolationMessage = null
		)
		{
			try
			{
				await this.innerClient.CloseAsync(
					policyViolationMessage == null ? WebSocketCloseStatus.NormalClosure : WebSocketCloseStatus.PolicyViolation,
					policyViolationMessage ?? "Closing",
					cancellationToken
				);
			}
			catch (WebSocketException ex)
			{
				throw new Exception("Failed to close websocket connection. ErrorCode: " + ex.WebSocketErrorCode);
			}
		}

		public void Dispose()
		{
			this.innerClient.Dispose();
		}
	}
}
