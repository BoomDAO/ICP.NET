using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(Result4Tag))]
	public class Result4
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public Result4Tag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Result4(Result4Tag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Result4()
		{
		}
		
		public static Result4 Ok(NeuronInfo info)
		{
			return new Result4(Result4Tag.Ok, info);
		}
		
		public NeuronInfo AsOk()
		{
			this.ValidateType(Result4Tag.Ok);
			return (NeuronInfo)this.Value!;
		}
		
		public static Result4 Err(GovernanceError info)
		{
			return new Result4(Result4Tag.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result4Tag.Err);
			return (GovernanceError)this.Value!;
		}
		
		private void ValidateType(Result4Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum Result4Tag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(NeuronInfo))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

