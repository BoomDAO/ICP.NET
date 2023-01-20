using EdjCase.ICP.Candid.Mapping;
using PublicKey = System.Collections.Generic.List<System.Byte>;

namespace EdjCase.ICP.InternetIdentity.Models
{
	public class InternetIdentityInit
	{
		public class R0V0
		{
			[CandidName("0")]
			public ulong F0 { get; set; }
			
			[CandidName("1")]
			public ulong F1 { get; set; }
			
		}
		[CandidName("assigned_user_number_range")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Candid.Models.OptionalValue<R0V0> AssignedUserNumberRange { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("archive_module_hash")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Candid.Models.OptionalValue<PublicKey> ArchiveModuleHash { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("canister_creation_cycles_cost")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Candid.Models.OptionalValue<ulong> CanisterCreationCyclesCost { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
	}
}

