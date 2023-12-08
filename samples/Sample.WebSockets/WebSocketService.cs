using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Mapping;
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
	public class AppMessage
	{
		[CandidName("text")]
		public string Text { get; set; }

		[CandidName("timestamp")]
		public ulong Timestamp { get; set; }
	}

	public class WebSocketService : BackgroundService
	{
		private IWebSocketAgent<AppMessage>? webSocket;

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			Principal canisterId = Principal.FromText("bkyz2-fmaaa-aaaaa-qaaaq-cai");
			Uri gatewayUri = new Uri("ws://localhost:8080");
			this.webSocket = await new WebSocketBuilder(canisterId, gatewayUri)
				.WithIdentity(Secp256k1Identity.Generate())
				.BuildAndConnectAsync<AppMessage>();
			await base.StartAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (this.webSocket?.IsConnectionEstablished == true)
			{
				await this.webSocket.ReceiveNextAsync(
					this.OnOpen,
					this.OnMessage,
					this.OnError,
					this.OnClose,
					stoppingToken
				);
			}
		}

		private void OnOpen()
		{
			Console.WriteLine("Opened");
		}

		private void OnMessage(AppMessage message)
		{
			Console.WriteLine("Received message: " + message.Text);
		}
		private void OnError(Exception ex)
		{
			Console.WriteLine("Error: " + ex.ToString());
		}

		private void OnClose(OnCloseContext context)
		{
			Console.WriteLine("Closed");
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			await this.webSocket!.DisposeAsync();
			await base.StopAsync(cancellationToken);
		}
	}
}
