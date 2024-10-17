using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.PocketIC.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

public class CreateCanisterResponse
{
	[CandidName("canister_id")]
	public Principal CanisterId { get; set; }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.