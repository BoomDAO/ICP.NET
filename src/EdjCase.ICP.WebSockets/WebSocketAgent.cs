using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.WebSockets.Models;
using System.Runtime.InteropServices;

namespace EdjCase.ICP.WebSockets
{
	internal class WebSocketAgent<TMessage> : IWebSocketAgent<TMessage>
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

		public WebSocketAgent(
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
				// CanisterWsMessageArguments
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
			//RequestMessage<CallRequest> request = new();
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
			Action onOpen,
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

			await this.ProcessMessageAsync(data, onOpen, onMessage, onError);
		}

		private async Task ProcessMessageAsync(
			byte[] data,
			Action onOpen,
			Action<TMessage> onMessage,
			Action<Exception> onError
		)
		{
			CborReader reader = new (data);
			switch (reader.PeekState())
			{
				case CborReaderState.Tag:
					{
						switch (reader.ReadTag())
						{
							case CborTag.SelfDescribeCbor:
								switch (reader.PeekState())
								{
									case CborReaderState.StartMap:
										// Open message with gateway principal
										reader.ReadStartMap();
										string firstKey = reader.ReadTextString();
										if (firstKey == "gateway_principal")
										{
											byte[] gatewayPrincipalBytes = reader.ReadByteString();
											this.gatewayPrincipal = Principal.FromBytes(gatewayPrincipalBytes);
											await this.SendOpenMessageAsync(this.gatewayPrincipal);
											return;
										}
										break;
								}
								break;
						}
						break;
					}
				case CborReaderState.StartMap:
					{
						ClientIncomingMessage clientMessage;
						try
						{
							clientMessage = ClientIncomingMessage.FromCbor(reader);
						}
						catch (Exception ex)
						{
							onError(new Exception("Unable to parse incoming message", ex));
							return;
						}
						WebSocketMessage wsMessage;
						try
						{
							CborReader wsReader = new CborReader(clientMessage.Content);
							wsReader.ReadTag();
							wsMessage = WebSocketMessage.FromCbor(wsReader);
						}
						catch (Exception ex)
						{
							onError(new Exception("Unable to parse websocket message", ex));
							return;
						}
						

						// Convert bytes to candid
						CandidArg arg;
						try
						{
							arg = CandidArg.FromBytes(wsMessage.Content);
						}
						catch (Exception ex)
						{
							onError(new Exception("Failed to parse bytes as candid", ex));
							return;
						}
						if (wsMessage.IsServiceMessage)
						{
							await this.ProcessServiceMessageAsync(arg, onOpen);
							return;
						}
						else
						{
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
							return;
						}
					}
			};
			string hex = BitConverter.ToString(data).Replace("-", "");
			onError(new Exception("Unrecognized message format. Unable to parse. Message hex: " + hex));
		}

		private async Task ProcessServiceMessageAsync(CandidArg arg, Action onOpen)
		{
			ServiceMessage serviceMessage = arg.ToObjects<ServiceMessage>();
			switch(serviceMessage.Tag) {
				case ServiceMessageTag.AckMessage:
					{
						AckMessage message = serviceMessage.AsAckMessage();
						break;
					}
				case ServiceMessageTag.OpenMessage:
					{
						OpenMessage message = serviceMessage.AsOpenMessage();
						onOpen();
						return ;
					}
				case ServiceMessageTag.KeepAliveMessage:
					{
						KeepAliveMessage message = serviceMessage.AsKeepAliveMessage();
						break;
					}
				default:
					throw new NotImplementedException();
			}
		}

		private ulong GetOrCreateClientNonce()
		{
			if (this.clientNonce == null)
			{
				// Create random ulong
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
			CallRequest request = new(
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
				await this.CloseAsync();
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
