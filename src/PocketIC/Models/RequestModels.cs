using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.PocketIC.Models;


public class CreateCanisterRequest
{
	[CandidName("settings")]
	public OptionalValue<CanisterSettings> Settings { get; set; } = OptionalValue<CanisterSettings>.NoValue();

	[CandidName("amount")]
	public OptionalValue<UnboundedUInt> Amount { get; set; } = OptionalValue<UnboundedUInt>.NoValue();

	[CandidName("specified_id")]
	public OptionalValue<Principal> SpecifiedId { get; set; } = OptionalValue<Principal>.NoValue();
}

public class CanisterSettings
{
	[CandidName("controllers")]
	public OptionalValue<List<Principal>> Controllers { get; set; } = OptionalValue<List<Principal>>.NoValue();

	[CandidName("compute_allocation")]
	public OptionalValue<UnboundedUInt> ComputeAllocation { get; set; } = OptionalValue<UnboundedUInt>.NoValue();

	[CandidName("memory_allocation")]
	public OptionalValue<UnboundedUInt> MemoryAllocation { get; set; } = OptionalValue<UnboundedUInt>.NoValue();

	[CandidName("freezing_threshold")]
	public OptionalValue<UnboundedUInt> FreezingThreshold { get; set; } = OptionalValue<UnboundedUInt>.NoValue();

	[CandidName("reserved_cycles_limit")]
	public OptionalValue<UnboundedUInt> ReservedCyclesLimit { get; set; } = OptionalValue<UnboundedUInt>.NoValue();
}


public class StartCanisterRequest
{
	[CandidName("canister_id")]
	public required Principal CanisterId { get; set; }
}

public class StopCanisterRequest
{
	[CandidName("canister_id")]
	public required Principal CanisterId { get; set; }
}

public class InstallCodeRequest
{
	[CandidName("canister_id")]
	public required Principal CanisterId { get; set; }

	[CandidName("arg")]
	public required byte[] Arg { get; set; }

	[CandidName("wasm_module")]
	public required byte[] WasmModule { get; set; }

	[CandidName("mode")]
	public required InstallCodeMode Mode { get; set; }
}

public enum InstallCodeMode
{
	[CandidName("install")]
	Install,
	[CandidName("reinstall")]
	Reinstall,
	[CandidName("upgrade")]
	Upgrade
}

public class UpdateCanisterSettingsRequest
{
	[CandidName("canister_id")]
	public required Principal CanisterId { get; set; }

	[CandidName("settings")]
	public required CanisterSettings Settings { get; set; }
}