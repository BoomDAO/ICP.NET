using System;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(GetDelegationResponseTag))]
	internal class GetDelegationResponse
	{
		[VariantTagProperty]
		public GetDelegationResponseTag Tag { get; set; }
		[VariantValueProperty]
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
		[CandidName("signed_delegation")]
		[VariantOptionType(typeof(SignedDelegation))]
		SignedDelegation,
		[CandidName("no_such_delegation")]
		NoSuchDelegation,
	}
}

