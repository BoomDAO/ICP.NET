public class WebSocketManager : MonoBehaviour
{
	private ClientWebSocket ws = new ClientWebSocket();
	private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

	async void Start()
	{
		await ConnectWebSocket();
		StartCoroutine(ReceiveMessages());
	}

	async Task ConnectWebSocket()
	{
		await ws.ConnectAsync(new Uri("ws://example.com"), cancellationTokenSource.Token);
	}

	IEnumerator ReceiveMessages()
	{
		byte[] buffer = new byte[1024 * 4];

		while (ws.State == WebSocketState.Open)
		{
			var segment = new ArraySegment<byte>(buffer);
			var result = await ws.ReceiveAsync(segment, cancellationTokenSource.Token);
			OnMessage(segment.Array, result);
			yield return null; // Yield to avoid blocking
		}
	}

	void OnMessage(byte[] buffer, WebSocketReceiveResult result)
	{
		string message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
		// Handle message
	}

	void OnDestroy()
	{
		cancellationTokenSource.Cancel(); // Cancel any ongoing operations
		ws.Dispose();
	}
}