using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.WebSockets.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace EdjCase.ICP.WebSockets
{
	internal class WebSocket<TMessage> where TMessage : notnull
	{
		private Principal canisterId { get; }
		private IIdentity identity { get; }
		private int? ackMessageTimeout { get; }
		private int? maxCertificateAgeInMinutes { get; }
		private CandidConverter? customConverter { get; }
		private Func<OnCloseContext, Task>? onClose { get; }
		private Func<OnErrorContext, Task>? onError { get; }
		private Func<OnMessageContext, Task>? onMessage { get; }
		private Func<OnOpenContext, Task>? onOpen { get;  }

		private ClientWebSocket socket { get; }

		private BaseQueue<ArrayBuffer> incomingMessagesQueue;
		private BaseQueue<byte[]> outgoingMessagesQueue;
		private AckMessagesQueue ackMessagesQueue;

		public WebSocketState State => this.socket.State;

		public WebSocket(
			Principal canisterId,
			string networkUrl,
			IIdentity? identity = null,
			int? ackMessageTimeout = 450000,
			int? maxCertificateAgeInMinutes = 5,
			Func<OnCloseContext, Task>? onClose = null,
			Func<OnErrorContext, Task>? onError = null,
			Func<OnMessageContext, Task>? onMessage = null,
			Func<OnOpenContext, Task>? onOpen = null,
			CandidConverter? customConverter = null)
		{
			this.Config = config;
			this.CustomConverter = customConverter;
			this.socket = new ClientWebSocket();
			this.socket.ConnectAsync()
		}

		public void Send(TMessage message)
		{
			CandidTypedValue messageCandid = CandidTypedValue.FromObject(message);
			CandidArg arg = new (new List<CandidTypedValue>
			{
				messageCandid
			});
			byte[] messageBytes = arg.Encode();
			this.outgoingMessagesQueue.Add(messageBytes);
		}

		public Principal GetPrincipal()
		{
		}

		public async Task CloseAsync()
		{
			await this.socket.CloseAsync(
				WebSocketCloseStatus.NormalClosure,
				"Closing",
				CancellationToken.None
			);
		}

		public bool IsConnectionEstablished()
		{
			return this.socket.State == WebSocketState.Open;
		}
	}
}
