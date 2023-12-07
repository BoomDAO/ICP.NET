using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.WebSockets;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Sample.WebSockets
{
	public class WebSocketService : BackgroundService
	{
		private IWebSocket<string>? webSocket;

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			Principal canisterId = Principal.Anonymous();
			Uri gatewayUri = new Uri("wss://gateway.icws.io");
			this.webSocket = await new WebSocketBuilder(canisterId, gatewayUri)
				.WithIdentity(Secp256k1Identity.Generate())
				.BuildAndConnectAsync<string>();
			await base.StartAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (this.webSocket?.IsConnectionEstablished == true)
			{
				await this.webSocket.ReceiveNextAsync(
					this.OnMessage,
					this.OnError,
					this.OnClose,
					stoppingToken
				);
			}
		}

		private void OnMessage(string message)
		{

		}
		private void OnError(Exception ex)
		{

		}

		private void OnClose(OnCloseContext context)
		{

		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			await this.webSocket!.DisposeAsync();
			await base.StopAsync(cancellationToken);
		}
	}
}
