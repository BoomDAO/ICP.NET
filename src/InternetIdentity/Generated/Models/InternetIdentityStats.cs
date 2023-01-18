using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using UserNumber = System.UInt64;
using PublicKey = System.Collections.Generic.List<System.Byte>;
using CredentialId = System.Collections.Generic.List<System.Byte>;
using DeviceKey = System.Collections.Generic.List<System.Byte>;
using UserKey = System.Collections.Generic.List<System.Byte>;
using SessionKey = System.Collections.Generic.List<System.Byte>;
using FrontendHostname = System.String;
using Timestamp = System.UInt64;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	public class InternetIdentityStats
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("users_registered")]
		public ulong UsersRegistered { get; set; }
		
		public class R1
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public ulong F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public ulong F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("assigned_user_number_range")]
		public R1 AssignedUserNumberRange { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("archive_info")]
		public ArchiveInfo ArchiveInfo { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("canister_creation_cycles_cost")]
		public ulong CanisterCreationCyclesCost { get; set; }
		
	}
}

