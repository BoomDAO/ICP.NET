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
	public class WebSocketBuilder
	{
		private Principal canisterId { get; }
		private Uri gatewayUri { get; }
		private IIdentity? identity { get; set; }
		private IBlsCryptography? bls { get; set; }
		private SubjectPublicKeyInfo? rootPublicKey { get; set; }
		private Uri? boundryNodeUri { get; set; }
		private CandidConverter? customConverter { get; set; }

		public WebSocketBuilder(
			Principal canisterId,
			Uri gatewayUri,
			SubjectPublicKeyInfo rootPublicKey
		)
		{
			this.canisterId = canisterId;
			this.gatewayUri = gatewayUri;
			this.rootPublicKey = rootPublicKey;
		}

		public WebSocketBuilder(
			Principal canisterId,
			Uri gatewayUri,
			Uri? boundryNodeUri = null
		)
		{
			this.canisterId = canisterId;
			this.gatewayUri = gatewayUri;
			this.boundryNodeUri = boundryNodeUri;
		}

		public WebSocketBuilder WithIdentity(IIdentity identity)
		{
			this.identity = identity;
			return this;
		}

		public WebSocketBuilder WithCustomCandidConverter(CandidConverter customConverter)
		{
			this.customConverter = customConverter;
			return this;
		}

		public WebSocketBuilder WithCustomBlsCryptography(IBlsCryptography bls)
		{
			this.bls = bls;
			return this;
		}

		public async Task<IWebSocketAgent<TMessage>> BuildAsync<TMessage>(
			bool connect = true,
			CancellationToken? cancellationToken = null
		)
			where TMessage : notnull
		{
			if (this.identity == null)
			{
				// Generate ephemral identity if not specified
				this.identity = Ed25519Identity.Generate();
			}
			if (this.rootPublicKey == null)
			{
				IHttpClient httpClient = new DefaultHttpClient(new System.Net.Http.HttpClient
				{
					BaseAddress = this.boundryNodeUri ?? new Uri("https://ic0.app/")
				});
				HttpAgent httpAgent= new (httpClient, null, this.bls);
				this.rootPublicKey = await httpAgent.GetRootKeyAsync(cancellationToken);
			}
			if (this.bls == null)
			{
				this.bls = new WasmBlsCryptography();
			}
			var agent = new WebSocketAgent<TMessage>(
				this.canisterId,
				this.gatewayUri,
				this.rootPublicKey,
				this.identity,
				this.bls,
				this.customConverter
			);
			if (connect)
			{
				await agent.ConnectAsync(cancellationToken);
			}
			return agent;
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
