using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Spawn
	{
		[CandidName("percentage_to_spawn")]
		public OptionalValue<uint> PercentageToSpawn { get; set; }

		[CandidName("new_controller")]
		public OptionalValue<Principal> NewController { get; set; }

		[CandidName("nonce")]
		public OptionalValue<ulong> Nonce { get; set; }

		public Spawn(OptionalValue<uint> percentageToSpawn, OptionalValue<Principal> newController, OptionalValue<ulong> nonce)
		{
			this.PercentageToSpawn = percentageToSpawn;
			this.NewController = newController;
			this.Nonce = nonce;
		}

		public Spawn()
		{
		}
	}
}