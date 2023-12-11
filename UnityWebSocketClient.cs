
public class AppMessage
{

}

public class WebSocketManager : MonoBehaviour
{
	private Principal devCanisterId = Principal.FromText("{devCanisterId}");
	private Uri devGatewayUri = new Uri("ws://localhost:8080");
	private Uri devBoundryNodeUri = new Uri("http://localhost:4943");

	private Principal prodCanisterId = Principal.FromText("{prodCanisterId}");
	private Uri prodGatewayUri = new Uri("wss://icwebsocketgateway.app.runonflux.io");

	private IWebSocket<AppMessage>? websocket;
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
		this.websocket = await builder.BuildAsync(cancellationToken: cancellationTokenSource.Token);
		await this.websocket.ReceiveAllAsync(cancellationTokenSource.Token);
	}

	void OnOpen()
	{
	}
	void OnMessage(AppMessage message)
	{
	}
	void OnError(Exception ex)
	{
	}
	void OnClose()
	{
	}

	void OnDestroy()
	{
		cancellationTokenSource.Cancel(); // Cancel any ongoing operations
		webSocket?.Dispose();
	}
}