using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	internal class InternetIdentityStats
	{
		[CandidName("users_registered")]
		public ulong UsersRegistered { get; set; }
		
		public class R1
		{
			[CandidName("0")]
			public ulong F0 { get; set; }
			
			[CandidName("1")]
			public ulong F1 { get; set; }
			
		}
		[CandidName("assigned_user_number_range")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public R1 AssignedUserNumberRange { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("archive_info")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ArchiveInfo ArchiveInfo { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("canister_creation_cycles_cost")]
		public ulong CanisterCreationCyclesCost { get; set; }
		
	}
}

