using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Agents.Http;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.BLS;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EdjCase.ICP.WebSockets
{
	public class WebSocketBuilder<TMessage>
			where TMessage : notnull
	{
		private Principal canisterId { get; }
		private Uri gatewayUri { get; }
		private IIdentity? identity { get; set; }
		private IBlsCryptography? bls { get; set; }
		private SubjectPublicKeyInfo? rootPublicKey { get; set; }
		private CandidConverter? customConverter { get; set; }
		private Action<TMessage>? onMessage { get; set; }
		private Action? onOpen { get; set; }
		private Action? onClose { get; set; }
		private Action<Exception>? onError { get; set; }


		public WebSocketBuilder(
			Principal canisterId,
			Uri gatewayUri
		)
		{
			this.canisterId = canisterId;
			this.gatewayUri = gatewayUri;
		}

		public WebSocketBuilder<TMessage> OnOpen(Action onOpen)
		{
			this.onOpen = onOpen;
			return this;
		}

		public WebSocketBuilder<TMessage> OnMessage(Action<TMessage> onMessage)
		{
			this.onMessage = onMessage;
			return this;
		}

		public WebSocketBuilder<TMessage> OnError(Action<Exception> onError)
		{
			this.onError = onError;
			return this;
		}

		public WebSocketBuilder<TMessage> OnClose(Action onClose)
		{
			this.onClose = onClose;
			return this;
		}


		public WebSocketBuilder<TMessage> WithRootKey(byte[] derEncodedRootKey)
		{
			this.rootPublicKey = SubjectPublicKeyInfo.FromDerEncoding(derEncodedRootKey);
			return this;
		}

		public WebSocketBuilder<TMessage> WithRootKey(SubjectPublicKeyInfo subjectPublicKeyInfo)
		{
			this.rootPublicKey = subjectPublicKeyInfo;
			return this;
		}

		public WebSocketBuilder<TMessage> WithIdentity(IIdentity identity)
		{
			this.identity = identity;
			return this;
		}

		public WebSocketBuilder<TMessage> WithCustomCandidConverter(CandidConverter customConverter)
		{
			this.customConverter = customConverter;
			return this;
		}

		public WebSocketBuilder<TMessage> WithCustomBlsCryptography(IBlsCryptography bls)
		{
			this.bls = bls;
			return this;
		}

		public async Task<IWebSocketAgent<TMessage>> BuildAsync(
			bool connect = true,
			CancellationToken? cancellationToken = null
		)
		{
			if (this.identity == null)
			{
				// Generate ephemral identity if not specified
				this.identity = Ed25519Identity.Generate();
			}
			if (this.rootPublicKey == null)
			{
				this.rootPublicKey = SubjectPublicKeyInfo.MainNetRootPublicKey;
			}
			if (this.bls == null)
			{
				this.bls = new WasmBlsCryptography();
			}
			if (this.onMessage == null)
			{
				throw new InvalidOperationException("Web socket requires an OnMessage action");
			}
			WebSocketAgent<TMessage> agent = new(
				this.canisterId,
				this.gatewayUri,
				this.rootPublicKey,
				this.identity,
				this.bls,
				this.onMessage,
				this.onOpen,
				this.onError,
				this.onClose,
				this.customConverter
			);
			if (connect)
			{
				await agent.ConnectAsync(cancellationToken);
			}
			return agent;
		}

	}
}
