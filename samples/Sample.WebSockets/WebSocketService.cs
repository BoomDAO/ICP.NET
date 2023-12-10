using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.WebSockets;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private Stopwatch stopwatch = Stopwatch.StartNew();

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			Principal canisterId = Principal.FromText("bkyz2-fmaaa-aaaaa-qaaaq-cai");
			Uri gatewayUri = new Uri("ws://localhost:8080");
			Uri boundryNodeUrl = new Uri("http://localhost:4943");
			this.webSocket = await new WebSocketBuilder(canisterId, gatewayUri, boundryNodeUrl)
				.WithIdentity(Secp256k1Identity.Generate())
				.BuildAsync<AppMessage>(cancellationToken: cancellationToken);
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
			Console.WriteLine("Opened: " + this.stopwatch.Elapsed);
			
		}

		private void OnMessage(AppMessage message)
		{
			Console.WriteLine("Received message: " + message.Text);
			ICTimestamp.Now().NanoSeconds.TryToUInt64(out ulong now);
			Console.WriteLine("Sending message:  pong" + this.stopwatch.Elapsed);
			this.webSocket!.SendAsync(new AppMessage
			{
				Text = "pong",
				Timestamp = now
			});
		}
		private void OnError(Exception ex)
		{
			Console.WriteLine("Error: " + ex.ToString());
		}

		private void OnClose(OnCloseContext context)
		{
			Console.WriteLine("Closed" + this.stopwatch.Elapsed);
		}

		public override async Task StopAsync(CancellationToken cancellationToken)
		{
			await this.webSocket!.DisposeAsync();
			await base.StopAsync(cancellationToken);
		}
	}
}
