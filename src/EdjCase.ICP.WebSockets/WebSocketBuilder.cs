using EdjCase.ICP.Agent.Identities;
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
	public class WebSocketBuilder
	{
		private Principal canisterId { get; }
		private Uri gatewayUri { get; }
		private IIdentity? identity { get; set; }
		private CandidConverter? customConverter { get; set; }

		public WebSocketBuilder(Principal canisterId, Uri gatewayUri)
		{
			this.canisterId = canisterId;
			this.gatewayUri = gatewayUri;
		}

		public WebSocketBuilder WithIdentity(IIdentity identity)
		{
			this.identity = identity;
			return this;
		}

		public WebSocketBuilder WithCandidConverter(CandidConverter customConverter)
		{
			this.customConverter = customConverter;
			return this;
		}

		public IWebSocketAgent<TMessage> Build<TMessage>()
			where TMessage : notnull
		{
			if (this.identity == null)
			{
				// Generate ephemral identity if not specified
				this.identity = Ed25519Identity.Generate();
			}
			return new WebSocketAgent<TMessage>(
				this.canisterId,
				this.gatewayUri,
				this.identity,
				this.customConverter
			);
		}

		public async Task<IWebSocketAgent<TMessage>> BuildAndConnectAsync<TMessage>(
			CancellationToken? cancellationToken = null
		)
			where TMessage : notnull
		{
			IWebSocketAgent<TMessage> ws = this.Build<TMessage>();
			await ws.ConnectAsync(cancellationToken);
			return ws;
		}
	}
	public class OnCloseContext
	{

	}

	public class OnErrorContext
	{

	}

	public class OnMessageContext
	{

	}

	public class OnOpenContext
	{

	}
}
