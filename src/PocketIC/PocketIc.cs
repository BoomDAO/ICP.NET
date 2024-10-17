using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.PocketIC.Client;
using EdjCase.ICP.PocketIC.Models;

namespace EdjCase.ICP.PocketIC
{
	public class PocketIc : IAsyncDisposable
	{
		private static readonly Principal MANAGEMENT_CANISTER_ID = Principal.FromText("aaaaa-aa");

		private readonly PocketIcClient client;
		private readonly CandidConverter candidConverter;

		private PocketIc(
			PocketIcClient client,
			CandidConverter? candidConverter = null
		)
		{
			this.client = client;
			this.candidConverter = candidConverter ?? CandidConverter.Default;
		}

		public async Task<Principal> SetupCanisterAsync(
			byte[] wasmModule,
			CandidArg arg, // TODO can we take in a generic arg type? but issue is there can be multiple args
			CreateCanisterRequest? request = null
		)
		{
			CreateCanisterResponse createCanisterResponse = await this.CreateCanisterAsync(request);
			byte[] argBytes = arg.Encode();
			await this.InstallCodeAsync(new InstallCodeRequest
			{
				CanisterId = createCanisterResponse.CanisterId,
				WasmModule = wasmModule,
				Arg = argBytes,
				Mode = InstallCodeMode.Install
			});
			return createCanisterResponse.CanisterId;
		}

		public async Task<CreateCanisterResponse> CreateCanisterAsync(CreateCanisterRequest? request = null)
		{
			request ??= new CreateCanisterRequest();
			return await this.UpdateCallAsync<CreateCanisterRequest, CreateCanisterResponse>(
				Principal.Anonymous(),
				MANAGEMENT_CANISTER_ID,
				"provisional_create_canister_with_cycles",
				request
			);
		}

		public async Task StartCanisterAsync(StartCanisterRequest request)
		{
			await this.UpdateCallNoResponseAsync(
				Principal.Anonymous(),
				MANAGEMENT_CANISTER_ID,
				"start_canister",
				request
			);
		}

		public async Task StopCanisterAsync(StopCanisterRequest request)
		{
			await this.UpdateCallNoResponseAsync(
				Principal.Anonymous(),
				MANAGEMENT_CANISTER_ID,
				"stop_canister",
				request
			);
		}

		public async Task InstallCodeAsync(InstallCodeRequest request)
		{
			await this.UpdateCallNoResponseAsync(
				Principal.Anonymous(),
				MANAGEMENT_CANISTER_ID,
				"install_code",
				request
			);
		}


		public async Task<TResponse> QueryCallAsync<TRequest, TResponse>(
			Principal sender,
			Principal canisterId,
			string method,
			TRequest request,
			EffectivePrincipal? effectivePrincipal = null
		)
			where TRequest : notnull
		{
			CandidTypedValue requestValue = this.candidConverter.FromTypedObject(request);
			CandidArg arg = CandidArg.FromCandid(requestValue);
			CandidArg responseArg = await this.QueryCallRawAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
			return responseArg.ToObjects<TResponse>(this.candidConverter);
		}


		public async Task<TResponse> QueryCallNoRequestAsync<TResponse>(
			Principal sender,
			Principal canisterId,
			string method,
			EffectivePrincipal? effectivePrincipal = null
		)
		{
			CandidArg arg = CandidArg.FromCandid();
			CandidArg responseArg = await this.QueryCallRawAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
			return responseArg.ToObjects<TResponse>(this.candidConverter);
		}



		public async Task<CandidArg> QueryCallRawAsync(
			Principal sender,
			Principal canisterId,
			string method,
			CandidArg arg,
			EffectivePrincipal? effectivePrincipal = null
		)
		{
			return await this.client.QueryCallAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
		}

		public async Task<TResponse> UpdateCallAsync<TRequest, TResponse>(
			Principal sender,
			Principal canisterId,
			string method,
			TRequest request,
			EffectivePrincipal? effectivePrincipal = null
		)
			where TRequest : notnull
		{
			CandidTypedValue requestValue = this.candidConverter.FromTypedObject(request);
			CandidArg arg = CandidArg.FromCandid(requestValue);
			CandidArg responseArg = await this.UpdateCallRawAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
			return responseArg.ToObjects<TResponse>(this.candidConverter);
		}

		public async Task<TResponse> UpdateCallNoRequestAsync<TResponse>(
			Principal sender,
			Principal canisterId,
			string method,
			EffectivePrincipal? effectivePrincipal = null
		)
		{
			CandidArg arg = CandidArg.FromCandid();
			CandidArg responseArg = await this.UpdateCallRawAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
			return responseArg.ToObjects<TResponse>(this.candidConverter);
		}

		public async Task UpdateCallNoResponseAsync<TRequest>(
			Principal sender,
			Principal canisterId,
			string method,
			TRequest request,
			EffectivePrincipal? effectivePrincipal = null
		)
			where TRequest : notnull
		{
			CandidTypedValue requestValue = this.candidConverter.FromTypedObject(request);
			CandidArg arg = CandidArg.FromCandid(requestValue);
			await this.UpdateCallRawAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
		}

		public async Task UpdateCallNoRequestOrResponseAsync(
			Principal sender,
			Principal canisterId,
			string method,
			EffectivePrincipal? effectivePrincipal = null
		)
		{
			CandidArg arg = CandidArg.FromCandid();
			await this.UpdateCallRawAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
		}


		public async Task<CandidArg> UpdateCallRawAsync(
			Principal sender,
			Principal canisterId,
			string method,
			CandidArg arg,
			EffectivePrincipal? effectivePrincipal = null
		)
		{
			return await this.client.UpdateCallAsync(
				sender,
				canisterId,
				method,
				arg,
				effectivePrincipal
			);
		}


		public async Task TickAsync(int times = 1)
		{
			for (int i = 0; i < times; i++)
			{
				await this.client.TickAsync();
			}
		}

		public Task<ICTimestamp> GetTimeAsync()
		{
			return this.client.GetTimeAsync();
		}

		public async Task ResetTimeAsync()
		{
			await this.SetTimeAsync(ICTimestamp.Now());
		}

		public Task SetTimeAsync(ICTimestamp time)
		{
			return this.client.SetTimeAsync(time);
		}

		public async Task AdvanceTimeAsync(TimeSpan duration)
		{
			var currentTime = await this.GetTimeAsync();
			var newTime = ICTimestamp.FromNanoSeconds(currentTime.NanoSeconds + ((ulong)duration.TotalMilliseconds * 1_000_000));
			await this.SetTimeAsync(newTime);
		}

		public Task<byte[]> GetPublicKeyAsync(Principal subnetId)
		{
			return this.client.GetPublicKeyAsync(subnetId);
		}

		public Task<Principal?> GetCanisterSubnetIdAsync(Principal canisterId)
		{
			return this.client.GetCanisterSubnetIdAsync(canisterId);
		}

		public List<SubnetTopology> GetTopology()
		{
			return this.client.GetTopology().Values.ToList();
		}

		public SubnetTopology? GetBitcoinSubnet()
		{
			return this.GetTopology().FirstOrDefault(s => s.Type == SubnetType.Bitcoin);
		}

		public SubnetTopology? GetFiduciarySubnet()
		{
			return this.GetTopology().FirstOrDefault(s => s.Type == SubnetType.Fiduciary);
		}

		public SubnetTopology? GetInternetIdentitySubnet()
		{
			return this.GetTopology().FirstOrDefault(s => s.Type == SubnetType.InternetIdentity);
		}

		public SubnetTopology? GetNnsSubnet()
		{
			return this.GetTopology().FirstOrDefault(s => s.Type == SubnetType.NNS);
		}

		public SubnetTopology? GetSnsSubnet()
		{
			return this.GetTopology().FirstOrDefault(s => s.Type == SubnetType.SNS);
		}

		public List<SubnetTopology> GetApplicationSubnets()
		{
			return this.GetTopology().Where(s => s.Type == SubnetType.Application).ToList();
		}

		public List<SubnetTopology> GetSystemSubnets()
		{
			return this.GetTopology().Where(s => s.Type == SubnetType.System).ToList();
		}

		public Task<ulong> GetCyclesBalanceAsync(Principal canisterId)
		{
			return this.client.GetCyclesBalanceAsync(canisterId);
		}

		public Task<ulong> AddCyclesAsync(Principal canisterId, ulong amount)
		{
			return this.client.AddCyclesAsync(canisterId, amount);
		}

		public Task SetStableMemoryAsync(Principal canisterId, byte[] stableMemory)
		{
			return this.client.SetStableMemoryAsync(canisterId, stableMemory);
		}

		public Task<byte[]> GetStableMemoryAsync(Principal canisterId)
		{
			return this.client.GetStableMemoryAsync(canisterId);
		}

		public async ValueTask DisposeAsync()
		{
			await this.client.DisposeAsync();
		}



		public static async Task<PocketIc> CreateAsync(
			string url,
			List<SubnetConfig>? applicationSubnets = null,
			SubnetConfig? bitcoinSubnet = null,
			SubnetConfig? fiduciarySubnet = null,
			SubnetConfig? iiSubnet = null,
			SubnetConfig? nnsSubnet = null,
			SubnetConfig? snsSubnet = null,
			List<SubnetConfig>? systemSubnets = null,
			List<SubnetConfig>? verifiedApplicationSubnets = null,
			bool nonmainnetFeatures = false,
			CandidConverter? candidConverter = null
		)
		{
			PocketIcClient client = await PocketIcClient.CreateAsync(
				url,
				applicationSubnets,
				bitcoinSubnet,
				fiduciarySubnet,
				iiSubnet,
				nnsSubnet,
				snsSubnet,
				systemSubnets,
				verifiedApplicationSubnets,
				nonmainnetFeatures
			);
			return new PocketIc(client, candidConverter);
		}
	}

}