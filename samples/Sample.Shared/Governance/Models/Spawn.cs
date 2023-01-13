namespace Sample.Shared.Governance.Models
{
	public class Spawn
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("percentage_to_spawn")]
		public EdjCase.ICP.Candid.Models.OptionalValue<uint> PercentageToSpawn { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("new_controller")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> NewController { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("nonce")]
		public EdjCase.ICP.Candid.Models.OptionalValue<ulong> Nonce { get; set; }
		
	}
}

