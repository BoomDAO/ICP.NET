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
	public class Delegation
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("pubkey")]
		public PublicKey Pubkey { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("expiration")]
		public Timestamp Expiration { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("targets")]
		public EdjCase.ICP.Candid.Models.OptionalValue<System.Collections.Generic.List<EdjCase.ICP.Candid.Models.Principal>> Targets { get; set; }
		
	}
}

