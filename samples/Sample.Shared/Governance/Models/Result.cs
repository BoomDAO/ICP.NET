using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(ResultTag))]
	public class Result
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public ResultTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Result(ResultTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Result()
		{
		}
		
		public static Result Ok()
		{
			return new Result(ResultTag.Ok, null);
		}
		
		public static Result Err(GovernanceError info)
		{
			return new Result(ResultTag.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(ResultTag.Err);
			return (GovernanceError)this.Value!;
		}
		
		private void ValidateType(ResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum ResultTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

