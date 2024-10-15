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
			EnsureExecutablePermission(binPath);

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


			// Check environment variable first
			string? envPath = Environment.GetEnvironmentVariable("POCKET_IC_PATH");
			if (!string.IsNullOrEmpty(envPath))
			{
				if (File.Exists(envPath))
				{
					return envPath;
				}
				else
				{
					Console.WriteLine($"Warning: POCKET_IC_PATH environment variable is set, but file does not exist: {envPath}");
				}
			}

			// List of possible locations to search for the binary
			var searchPaths = new[]
			{
				AppContext.BaseDirectory,
				Path.GetDirectoryName(typeof(Server).Assembly.Location),
				Environment.CurrentDirectory,
			};

			foreach (var basePath in searchPaths)
			{
				if (basePath == null) continue;

				string[] possiblePaths = new[]
				{
					Path.Combine(basePath, "runtimes", ridFolder, "native", fileName),
					Path.Combine(basePath, fileName),
				};

				foreach (var path in possiblePaths)
				{
					if (File.Exists(path))
					{
						return path;
					}
				}
			}

			throw new FileNotFoundException($"PocketIC binary not found. Searched in {string.Join(", ", searchPaths)}, and POCKET_IC_PATH environment variable");
		}
		private static void EnsureExecutablePermission(string filePath)
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				try
				{
					var fileInfo = new FileInfo(filePath);
					var unixFileMode = (UnixFileMode)fileInfo.UnixFileMode;
					unixFileMode |= UnixFileMode.UserExecute | UnixFileMode.GroupExecute | UnixFileMode.OtherExecute;
					File.SetUnixFileMode(filePath, unixFileMode);
				}
				catch (Exception ex)
				{
					throw new InvalidOperationException($"Failed to set executable permission on '{filePath}': {ex.Message}", ex);
				}
			}
		}
	}

	public class StartServerOptions
	{
		public bool ShowRuntimeLogs { get; set; } = false;
		public bool ShowCanisterLogs { get; set; } = false;
	}
}
