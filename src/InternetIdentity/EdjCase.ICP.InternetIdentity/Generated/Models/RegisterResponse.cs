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
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(RegisterResponseTag))]
	public class RegisterResponse
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public RegisterResponseTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private RegisterResponse(RegisterResponseTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected RegisterResponse()
		{
		}
		
		public class O0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("user_number")]
			public UserNumber UserNumber { get; set; }
			
		}
		public static RegisterResponse Registered(RegisterResponse.O0 info)
		{
			return new RegisterResponse(RegisterResponseTag.Registered, info);
		}
		
		public RegisterResponse.O0 AsRegistered()
		{
			this.ValidateTag(RegisterResponseTag.Registered);
			return (RegisterResponse.O0)this.Value!;
		}
		
		public static RegisterResponse CanisterFull()
		{
			return new RegisterResponse(RegisterResponseTag.CanisterFull, null);
		}
		
		public static RegisterResponse BadChallenge()
		{
			return new RegisterResponse(RegisterResponseTag.BadChallenge, null);
		}
		
		private void ValidateTag(RegisterResponseTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum RegisterResponseTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("registered")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RegisterResponse.O0))]
		Registered,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("canister_full")]
		CanisterFull,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("bad_challenge")]
		BadChallenge,
	}
}

