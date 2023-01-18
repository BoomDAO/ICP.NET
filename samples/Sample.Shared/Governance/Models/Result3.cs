using System;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(Result3Tag))]
	public class Result3
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public Result3Tag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Result3(Result3Tag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Result3()
		{
		}
		
		public static Result3 Ok(RewardNodeProviders info)
		{
			return new Result3(Result3Tag.Ok, info);
		}
		
		public RewardNodeProviders AsOk()
		{
			this.ValidateTag(Result3Tag.Ok);
			return (RewardNodeProviders)this.Value!;
		}
		
		public static Result3 Err(GovernanceError info)
		{
			return new Result3(Result3Tag.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateTag(Result3Tag.Err);
			return (GovernanceError)this.Value!;
		}
		
		private void ValidateTag(Result3Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum Result3Tag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(RewardNodeProviders))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

