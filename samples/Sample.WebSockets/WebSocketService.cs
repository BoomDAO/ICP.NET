using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
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

		public AppMessage(string text)
		{
			this.Text = text;
		}
	}

	public class WebSocketService : BackgroundService
	{
		private IWebSocketAgent<AppMessage>? webSocket;
		private Stopwatch stopwatch = Stopwatch.StartNew();

		public override async Task StartAsync(CancellationToken cancellationToken)
		{
			bool development = false;
			Principal canisterId;
			Uri gatewayUri;
			if (development)
			{
				canisterId = Principal.FromText("j4n55-giaaa-aaaap-qb3wq-cai");
				gatewayUri = new Uri("ws://localhost:8080");
			}
			else
			{
				canisterId = Principal.FromText("j4n55-giaaa-aaaap-qb3wq-cai");
				gatewayUri = new Uri("wss://gateway.icws.io");
				//gatewayUri = new Uri("wss://icwebsocketgateway.app.runonflux.io");
				//gatewayUri = new Uri("ws://localhost:8080");
			}
			var builder = new WebSocketBuilder<AppMessage>(canisterId, gatewayUri)
				.OnMessage(this.OnMessage)
				.OnOpen(this.OnOpen)
				.OnError(this.OnError)
				.OnClose(this.OnClose);
			if (development)
			{
				// Set the root key as the dev network key
				SubjectPublicKeyInfo devRootKey = await new HttpAgent(
					httpBoundryNodeUrl: new Uri("http://localhost:4943")
				).GetRootKeyAsync();
				builder = builder.WithRootKey(devRootKey);
			}
			this.webSocket = await builder.BuildAndConnectAsync(cancellationToken: cancellationToken);
			await base.StartAsync(cancellationToken);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await this.webSocket!.ReceiveAllAsync(stoppingToken);
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
			this.webSocket!.SendAsync(new AppMessage("pong"));
		}
		private void OnError(Exception ex)
		{
			Console.WriteLine("Error: " + ex.ToString());
		}

		private void OnClose()
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
