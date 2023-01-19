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
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(GetDelegationResponseTag))]
	public class GetDelegationResponse
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public GetDelegationResponseTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private GetDelegationResponse(GetDelegationResponseTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected GetDelegationResponse()
		{
		}
		
		public static GetDelegationResponse SignedDelegation(SignedDelegation info)
		{
			return new GetDelegationResponse(GetDelegationResponseTag.SignedDelegation, info);
		}
		
		public SignedDelegation AsSignedDelegation()
		{
			this.ValidateTag(GetDelegationResponseTag.SignedDelegation);
			return (SignedDelegation)this.Value!;
		}
		
		public static GetDelegationResponse NoSuchDelegation()
		{
			return new GetDelegationResponse(GetDelegationResponseTag.NoSuchDelegation, null);
		}
		
		private void ValidateTag(GetDelegationResponseTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum GetDelegationResponseTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("signed_delegation")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(SignedDelegation))]
		SignedDelegation,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("no_such_delegation")]
		NoSuchDelegation,
	}
}

