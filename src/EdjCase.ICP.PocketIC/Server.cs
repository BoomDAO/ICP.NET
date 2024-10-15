using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace EdjCase.ICP.PocketIC
{
	public class Server : IDisposable
	{
		private readonly Process _serverProcess;
		private readonly int _port;

		private Server(Process serverProcess, int port)
		{
			this._serverProcess = serverProcess;
			this._port = port;
		}

		public string GetUrl() => $"http://127.0.0.1:{this._port}";

		public async Task Stop()
		{
			if (!this._serverProcess.HasExited)
			{
				this._serverProcess.Kill();
				await this._serverProcess.WaitForExitAsync();
			}
		}

		public void Dispose()
		{
			this._serverProcess.Dispose();
		}



		public static async Task<Server> Start(
			bool showRuntimeLogs = false,
			bool showErrorLogs = true,
			CancellationToken? cancellationToken = null
		)
		{
			string binPath = GetBinPath();

			int pid = Process.GetCurrentProcess().Id;
			string picFilePrefix = $"pocket_ic_{pid}";
			string portFilePath = Path.Combine(Path.GetTempPath(), $"{picFilePrefix}.port");

			var startInfo = new ProcessStartInfo
			{
				FileName = binPath,
				Arguments = $"--pid {pid}",
				RedirectStandardOutput = !showRuntimeLogs,
				RedirectStandardError = !showErrorLogs,
				UseShellExecute = false
			};

			Process? serverProcess = Process.Start(startInfo);

			if (serverProcess == null)
			{
				throw new Exception("Failed to start PocketIC server process");
			}

			TimeSpan interval = TimeSpan.FromMilliseconds(20);
			TimeSpan timeout = TimeSpan.FromSeconds(30);
			Stopwatch stopwatch = Stopwatch.StartNew();
			int port = -1;
			while (true)
			{
				try
				{
					string portString = await File.ReadAllTextAsync(portFilePath);
					if (int.TryParse(portString, out port))
					{
						break;
					}
				}
				catch (Exception)
				{

				}
				if (stopwatch.Elapsed > timeout)
				{
					break;
				}
				await Task.Delay(interval); // wait to try again
			}
			if (port == -1)
			{
				throw new Exception($"Failed to start PocketIC server after {timeout}");
			}

			return new Server(serverProcess, port);
		}

		private static string GetBinPath()
		{
			string fileName = "pocket-ic";
			string? ridFolder = null;

			if (RuntimeInformation.OSArchitecture == Architecture.X64)
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					ridFolder = "linux-x64";
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					ridFolder = "osx-x64";
				}
			}
			if (ridFolder == null)
			{
				throw new PlatformNotSupportedException($"Unsupported operating system/architecture: {RuntimeInformation.RuntimeIdentifier}. Supported: linux-x64, osx-64");
			}

			string assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string assemblyDirectory = Path.GetDirectoryName(assemblyLocation)!;
			string binPath = Path.Combine(assemblyDirectory, "runtimes", ridFolder, "native", fileName);
			if (!File.Exists(binPath))
			{
				throw new FileNotFoundException("PocketIC binary not found", binPath);
			}
			return binPath;
		}
	}

	public class StartServerOptions
	{
		public bool ShowRuntimeLogs { get; set; } = false;
		public bool ShowCanisterLogs { get; set; } = false;
	}
}
