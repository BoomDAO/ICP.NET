using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(Result2Tag))]
	public class Result2
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public Result2Tag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Result2(Result2Tag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Result2()
		{
		}
		
		public static Result2 Ok(Neuron info)
		{
			return new Result2(Result2Tag.Ok, info);
		}
		
		public Neuron AsOk()
		{
			this.ValidateTag(Result2Tag.Ok);
			return (Neuron)this.Value!;
		}
		
		public static Result2 Err(GovernanceError info)
		{
			return new Result2(Result2Tag.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateTag(Result2Tag.Err);
			return (GovernanceError)this.Value!;
		}
		
		private void ValidateTag(Result2Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum Result2Tag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(Neuron))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

