using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.WebSockets.Models;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Formats.Cbor;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace EdjCase.ICP.WebSockets
{
	public interface IWebSocket<TMessage> : IAsyncDisposable
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
			Action<TMessage> onMessage,
			Action<Exception> onError,
			Action<OnCloseContext> onClose,
			CancellationToken? cancellationToken = null
		);

		Task CloseAsync();

		WebSocketState State { get; }

		bool IsConnectionEstablished => this.State == WebSocketState.Open;
	}

	internal class WebSocket<TMessage> : IWebSocket<TMessage>
		where TMessage : notnull
	{
		private Principal canisterId { get; }
		private Uri gatewayUri { get; }
		private IIdentity identity { get; }
		private CandidConverter? customConverter { get; }
		private ClientWebSocket socket { get; }

		private ulong sequenceNumber { get; set; } = 0;
		private Principal? gatewayPrincipal { get; set; }
		private ulong? clientNonce { get; set; }

		public WebSocketState State => this.socket.State;

		public WebSocket(
			Principal canisterId,
			Uri gatewayUri,
			IIdentity identity,
			CandidConverter? customConverter = null
		)
		{
			this.canisterId = canisterId;
			this.gatewayUri = gatewayUri;
			this.identity = identity;
			this.customConverter = customConverter;
			this.socket = new ClientWebSocket();
		}

		public async Task ConnectAsync(
			CancellationToken? cancellationToken = null
		)
		{
			await this.socket.ConnectAsync(
				this.gatewayUri,
				cancellationToken ?? CancellationToken.None
			);
		}

		public async Task SendAsync(
			TMessage message,
			CancellationToken? cancellationToken = null
		)
		{
			CandidTypedValue messageCandid = CandidTypedValue.FromObject(message, this.customConverter);
			CandidArg arg = new(new List<CandidTypedValue>
			{
				messageCandid
			});
			byte[] messageBytes = arg.Encode();
			//WebSocketMessage wsMessage = new(
			//	sequenceNumber: this.sequenceNumber,
			//	content: messageBytes,
			//	clientKey: this.clientKey,
			//	timestamp: ,
			//	isServiceMessage: false
			//);
			//this.sequenceNumber++;
			//RequestMessage<CallRequest> request = new ();
			//byte[] requestCbor = request.ToCbor();
			byte[] requestCbor = new byte[0]; // TODO
			try
			{
				await this.socket.SendAsync(
					requestCbor,
					WebSocketMessageType.Binary,
					endOfMessage: true,
					cancellationToken: cancellationToken ?? CancellationToken.None
				);
			}
			catch (ObjectDisposedException ex)
			{
				throw new InvalidOperationException("Websocket connection is closed. Cannot send message.", ex);
			}
			catch (InvalidOperationException ex)
			{
				throw new InvalidOperationException("Websocket connection has not been opened. Cannot send message.", ex);
			}
		}

		public async Task ReceiveNextAsync(
			Action<TMessage> onMessage,
			Action<Exception> onError,
			Action<OnCloseContext> onClose,
			CancellationToken? cancellationToken = null
		)
		{
			byte[] data;
			bool isClosed;
			try
			{
				(data, isClosed) = await this.ReceiveInternalAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				onError(new Exception("Failed to receive message", ex));
				return;
			}
			if (isClosed)
			{
				onClose(new OnCloseContext()); // TODO
				return;
			}
			string hex = BitConverter.ToString(data).Replace("-", "");
			CborReader reader = new CborReader(data);
			if (reader.PeekState() == CborReaderState.Tag)
			{
				reader.ReadTag();
				if (reader.PeekState() == CborReaderState.StartMap)
				{
					int? fieldCount = reader.ReadStartMap();
					string firstKey = reader.ReadTextString();
					if (firstKey == "gateway_principal")
					{
						byte[] gatewayPrincipalBytes = reader.ReadByteString();
						this.gatewayPrincipal = Principal.FromBytes(gatewayPrincipalBytes);
						await this.SendOpenMessageAsync(this.gatewayPrincipal);
						return;
					}
					throw new NotImplementedException();
				}

			}
			// Convert bytes to candid
			CandidArg arg;
			try
			{
				arg = CandidArg.FromBytes(data);
			}
			catch (Exception ex)
			{
				onError(new Exception("Failed to parse bytes as candid", ex));
				return;
			}

			// Convert candid to message type
			TMessage message;
			try
			{
				message = arg.ToObjects<TMessage>(this.customConverter);
			}
			catch (Exception ex)
			{
				onError(new Exception("Failed to convert candid to message", ex));
				return;
			}
			onMessage(message);
		}

		private ulong GetOrCreateClientNonce()
		{
			if (this.clientNonce == null)
			{
				// Create random ulong
				// TODO is pseudo random ok?
				Random rand = new();
				byte[] buf = new byte[8];
				rand.NextBytes(buf);
				this.clientNonce = BitConverter.ToUInt64(buf, 0);
			}
			return this.clientNonce.Value;
		}

		private async Task SendOpenMessageAsync(Principal gatewayPrincipal)
		{
			ulong clientNonce = this.GetOrCreateClientNonce();
			CandidArg arg = CandidArg.FromCandid(
				new CandidTypedValue(
					new CandidRecord(
						new Dictionary<CandidTag, CandidValue>
						{
							{ "client_nonce", CandidValue.Nat64(clientNonce) },
							{ "gateway_principal", CandidValue.Principal(gatewayPrincipal) }
						}
					),
					new CandidRecordType(
						new Dictionary<CandidTag, CandidType>
						{
							{ "client_nonce", CandidType.Nat64() },
							{ "gateway_principal", CandidType.Principal() }
						}
					)
				)
			);
			Principal sender = this.identity.GetPrincipal();
			CallRequest request = new (
				this.canisterId,
				"ws_open",
				arg,
				sender,
				ICTimestamp.Future(TimeSpan.FromSeconds(30))
			);
			SignedContent signedContent = this.identity
				.SignContent(request.BuildHashableItem());

			CborWriter writer = new CborWriter();
			writer.WriteStartMap(1);
			writer.WriteTextString("envelope");
			signedContent.WriteCbor(writer);
			writer.WriteEndMap();
			byte[] message = writer.Encode();
			await this.socket.SendAsync(message, WebSocketMessageType.Binary, true, CancellationToken.None);
		}

		public async Task CloseAsync()
		{
			await this.socket.CloseAsync(
				WebSocketCloseStatus.NormalClosure,
				"Closing",
				CancellationToken.None
			);
		}

		public async ValueTask DisposeAsync()
		{
			try
			{
				await this.socket.CloseAsync(
					WebSocketCloseStatus.NormalClosure,
					"NormalClosure",
					cancellationToken: CancellationToken.None
				);
			}
			finally
			{
				this.socket.Dispose();
			}
		}

		private async Task<(byte[] Data, bool Closed)> ReceiveInternalAsync(
			CancellationToken? cancellationToken = null
		)
		{
			ArrayPool<byte> arrayPool = ArrayPool<byte>.Shared;
			byte[] buffer = arrayPool.Rent(4096); // Used shared memory from pool
			try
			{
				using (MemoryStream memoryStream = new())
				{
					while (true)
					{
						WebSocketReceiveResult result = await this.socket.ReceiveAsync(
							new ArraySegment<byte>(buffer),
							cancellationToken ?? CancellationToken.None
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


	}
}
