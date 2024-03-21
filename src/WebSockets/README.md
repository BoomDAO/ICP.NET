# ICP.NET WebSockets


## Usage (Minimal)

```cs
Principal canisterId = ...; // Id of the canister to communicate with over websockets
Uri gatewayUri = ...; // Uri to the websocket gateway server
IWebSocketAgent<AppMessage> webSocket = await new WebSocketBuilder(canisterId, gatewayUri)
    .OnMessage((msg) => {}) // Action to receive messages from the server
    .BuildAndConnectAsync<AppMessage>();
```

## Usage (Advanced)

```cs
IWebSocketAgent<AppMessage> webSocket = await new WebSocketBuilder(canisterId, gatewayUri)
    .OnMessage((msg) => {}) // Action to receive messages from the server
    .OnOpen(() => {}) // Action to indicate when the connection opens
    .OnError((ex) => {}) // Action to output an error
    .OnClose(() => {}) // Action to indicate when the connection closes
    .WithIdentity(identity) // Used to specify an IIdentity, otherwise will generate an ephemeral identity
    .WithRootKey(rootKey) // Used to specify the network root public key, required if using a dev or other ic network
    .WithCustomCandidConverter(customConverter) // Used if `AppMessage` requires a custom CandidConverter for serialization
    .WithCustomClient(client) // Used to override the default WebSocket client
    .WithCustomBlsCryptography(bls) // Used to override the default bls library.
    .BuildAndConnectAsync<AppMessage>(cancellationToken);
```

# Unity
## Example using MonoBehavior
```cs

using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.WebSockets;
using System.Threading;
using System;
using UnityEngine;
using EdjCase.ICP.Agent;
using EdjCase.ICP.Candid.Mapping;

public class WebSocketManager : MonoBehaviour
{

    public class AppMessage
    {
        [CandidName("text")]
        public string Text { get; set; }

        [CandidName("timestamp")]
        public ulong Timestamp { get; set; }
    }

    private Principal devCanisterId = Principal.FromText("bkyz2-fmaaa-aaaaa-qaaaq-cai");
    private Uri devGatewayUri = new Uri("ws://localhost:8080");
    private Uri devBoundryNodeUri = new Uri("http://localhost:4943");

    private Principal prodCanisterId = Principal.FromText("bkyz2-fmaaa-aaaaa-qaaaq-cai");
    private Uri prodGatewayUri = new Uri("wss://icwebsocketgateway.app.runonflux.io");

    private IWebSocketAgent<AppMessage> agent;
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    async void Start()
    {
        bool development = true;
        Principal canisterId;
        Uri gatewayUri;
        if (development)
        {
            canisterId = devCanisterId;
            gatewayUri = devGatewayUri;
        }
        else
        {
            canisterId = prodCanisterId;
            gatewayUri = prodGatewayUri;
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
                httpBoundryNodeUrl: devBoundryNodeUri
            ).GetRootKeyAsync();
            builder = builder.WithRootKey(devRootKey);
        }
        this.agent = await builder.BuildAndConnectAsync(cancellationToken: cancellationTokenSource.Token);
        await this.agent.ReceiveAllAsync(cancellationTokenSource.Token);
    }

    void OnOpen()
    {
        Debug.Log("Open");
    }
    async void OnMessage(AppMessage message)
    {
        Debug.Log("Received Message: "+ message.Text);
        ICTimestamp.Now().NanoSeconds.TryToUInt64(out ulong now);
        var replyMessage = new AppMessage
        {
            Text = "pong",
            Timestamp = now
        };
        await this.agent.SendAsync(replyMessage, cancellationTokenSource.Token);
        Debug.Log("Sent Message: " + replyMessage.Text);
    }
    void OnError(Exception ex)
    {
        Debug.Log("Error: " + ex);
    }
    void OnClose()
    {
        Debug.Log("Close");
    }

    async void OnDestroy()
    {
        cancellationTokenSource.Cancel(); // Cancel any ongoing operations
        if (this.agent != null)
        {
            await this.agent.DisposeAsync();
        }
    }
}
```