using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using UserNumber = System.UInt64;
using PublicKey = System.Collections.Generic.List<System.Byte>;
using CredentialId = System.Collections.Generic.List<System.Byte>;
using DeviceKey = PublicKey;
using UserKey = PublicKey;
using SessionKey = PublicKey;
using FrontendHostname = System.String;
using Timestamp = System.UInt64;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	public class InternetIdentityInit
	{
		public class R0V0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("0")]
			public ulong F0 { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("1")]
			public ulong F1 { get; set; }
			
		}
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("assigned_user_number_range")]
		public EdjCase.ICP.Candid.Models.OptionalValue<R0V0> AssignedUserNumberRange { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("archive_module_hash")]
		public EdjCase.ICP.Candid.Models.OptionalValue<System.Collections.Generic.List<byte>> ArchiveModuleHash { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("canister_creation_cycles_cost")]
		public EdjCase.ICP.Candid.Models.OptionalValue<ulong> CanisterCreationCyclesCost { get; set; }
		
	}
}

