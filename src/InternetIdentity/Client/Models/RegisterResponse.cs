using System;
using EdjCase.ICP.Candid.Mapping;
using UserNumber = System.UInt64;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(RegisterResponseTag))]
	public class RegisterResponse
	{
		[VariantTagProperty]
		public RegisterResponseTag Tag { get; set; }
		[VariantValueProperty]
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
			[CandidName("user_number")]
			public UserNumber UserNumber { get; set; }
			
		}
		public static RegisterResponse Registered(O0 info)
		{
			return new RegisterResponse(RegisterResponseTag.Registered, info);
		}
		
		public O0 AsRegistered()
		{
			this.ValidateTag(RegisterResponseTag.Registered);
			return (O0)this.Value!;
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
		[CandidName("registered")]
		[VariantOptionType(typeof(RegisterResponse.O0))]
		Registered,
		[CandidName("canister_full")]
		CanisterFull,
		[CandidName("bad_challenge")]
		BadChallenge,
	}
}

