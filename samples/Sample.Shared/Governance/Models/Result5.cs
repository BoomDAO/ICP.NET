using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(Result5Tag))]
	public class Result5
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public Result5Tag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Result5(Result5Tag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Result5()
		{
		}
		
		public static Result5 Ok(NodeProvider info)
		{
			return new Result5(Result5Tag.Ok, info);
		}
		
		public NodeProvider AsOk()
		{
			this.ValidateType(Result5Tag.Ok);
			return (NodeProvider)this.Value!;
		}
		
		public static Result5 Err(GovernanceError info)
		{
			return new Result5(Result5Tag.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result5Tag.Err);
			return (GovernanceError)this.Value!;
		}
		
		private void ValidateType(Result5Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum Result5Tag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(NodeProvider))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

