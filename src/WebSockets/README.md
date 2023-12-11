# ICP.NET WebSockets


## Usage (Minimal)

```cs
Principal canisterId = ...; // Id of the canister to communicate with over websockets
Uri gatewayUri = ...; // Uri to the websocket gateway server
IWebSocketAgent<AppMessage> webSocket = await new WebSocketBuilder(canisterId, gatewayUri)
    .OnMessage((msg) => {}) // Action to receive messages from the server
	.BuildAsync<AppMessage>();
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
    .WithCustomBlsCryptography(bls) // Used to override the default bls library. See `WebGL Builds` below
    .BuildAsync<AppMessage>();
```


# WebGL Builds
Due to how WebGL works by converting C# to JS/WASM using IL2CPP there are a few additional steps to avoid
incompatibilities. 
- WebGlBlsCryptography - The BLS signature verification relies on a 3rd party library and due to that library not being directly compatible with the WebGL builds, `WebGlBlsCryptography` needs to be used instead of the default `WasmBlsCryptography` class.
    
    ```cs
    var bls = new WebGlBlsCrytography();
    IWebSocketAgent<AppMessage> webSocket = await new WebSocketBuilder(canisterId, gatewayUri)
        .WithCustomBlsCryptography(bls)
        .BuildAsync<AppMessage>();
    ```