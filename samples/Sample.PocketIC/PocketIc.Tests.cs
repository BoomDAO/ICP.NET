using System.Net;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.PocketIC;
using EdjCase.ICP.PocketIC.Models;

namespace Sample.PocketIC
{
	public class PocketIcTests
	{
		private PocketIcServer server;

		[OneTimeSetUp]
		public async Task Setup()
		{
			this.server = await PocketIcServer.Start();
		}

		[OneTimeTearDown]
		public async Task Teardown()
		{
			if (this.server != null)
			{
				await this.server.Stop();
				this.server.Dispose();
			}
		}

		[Test]
		public async Task CreateCanisterAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				CreateCanisterResponse response = await pocketIc.CreateCanisterAsync();

				Assert.NotNull(response);
				Assert.NotNull(response.CanisterId);
			}
		}

		[Test]
		public async Task SetupCanisterAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			byte[] wasmModule = File.ReadAllBytes("CanisterWasmModules/counter.wasm");
			CandidArg arg = CandidArg.FromCandid();

			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				Principal canisterId = await pocketIc.SetupCanisterAsync(wasmModule, arg);

				Assert.NotNull(canisterId);
			}
		}

		[Test]
		public async Task StartCanisterAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				CreateCanisterResponse response = await pocketIc.CreateCanisterAsync();
				await pocketIc.StartCanisterAsync(new StartCanisterRequest { CanisterId = response.CanisterId });

				// No exception means success
				Assert.Pass();
			}
		}

		[Test]
		public async Task StopCanisterAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				CreateCanisterResponse response = await pocketIc.CreateCanisterAsync();
				await pocketIc.StopCanisterAsync(new StopCanisterRequest { CanisterId = response.CanisterId });

				// No exception means success
				Assert.Pass();
			}
		}

		[Test]
		public async Task InstallCodeAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			byte[] wasmModule = File.ReadAllBytes("CanisterWasmModules/counter.wasm");
			CandidArg arg = CandidArg.FromCandid();

			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				CreateCanisterResponse response = await pocketIc.CreateCanisterAsync();
				await pocketIc.InstallCodeAsync(new InstallCodeRequest
				{
					CanisterId = response.CanisterId,
					WasmModule = wasmModule,
					Arg = arg.Encode(),
					Mode = InstallCodeMode.Install
				});

				// No exception means success
				Assert.Pass();
			}
		}

		[Test]
		public async Task QueryCallAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			byte[] wasmModule = File.ReadAllBytes("CanisterWasmModules/counter.wasm");
			CandidArg arg = CandidArg.FromCandid();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				Principal canisterId = await pocketIc.SetupCanisterAsync(wasmModule, arg);
				var result = await pocketIc.QueryCallNoRequestAsync<UnboundedUInt>(
					Principal.Anonymous(),
					canisterId,
					"get"
				);

				Assert.NotNull(result);
			}
		}

		[Test]
		public async Task UpdateCallAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			byte[] wasmModule = File.ReadAllBytes("CanisterWasmModules/counter.wasm");
			CandidArg arg = CandidArg.FromCandid();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				Principal canisterId = await pocketIc.SetupCanisterAsync(wasmModule, arg);
				await pocketIc.UpdateCallNoRequestOrResponseAsync(
					Principal.Anonymous(),
					canisterId,
					"inc"
				);

				Assert.Pass();
			}
		}

		[Test]
		public async Task TickAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				await pocketIc.TickAsync();

				// No exception means success
				Assert.Pass();
			}
		}

		[Test]
		public async Task GetTimeAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				var time = await pocketIc.GetTimeAsync();

				Assert.NotNull(time);
			}
		}

		[Test]
		public async Task AdvanceTimeAsync__Basic__Valid()
		{
			string url = this.server.GetUrl();
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				await pocketIc.AdvanceTimeAsync(TimeSpan.FromMinutes(1));

				// No exception means success
				Assert.Pass();
			}
		}
	}
}