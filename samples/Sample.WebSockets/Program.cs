using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sample.WebSockets;


public class Program
{
	// In Program.cs or Startup.cs
	public static async Task Main(string[] args)
	{
		IHost host = await Host.CreateDefaultBuilder(args)
			.ConfigureServices((hostContext, services) =>
			{
				services.AddHostedService<WebSocketService>();
			})
			.StartAsync();
		await host.WaitForShutdownAsync();
	}

}
