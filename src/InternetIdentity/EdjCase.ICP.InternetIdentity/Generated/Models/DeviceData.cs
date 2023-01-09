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
	public class DeviceData
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("pubkey")]
		public DeviceKey Pubkey { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("alias")]
		public string Alias { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("credential_id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<CredentialId> CredentialId { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("purpose")]
		public Purpose Purpose { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("key_type")]
		public KeyType KeyType { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("protection")]
		public DeviceProtection Protection { get; set; }
		
	}
}

