using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(Result1Tag))]
	public class Result1
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public Result1Tag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Result1(Result1Tag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Result1()
		{
		}
		
		public static Result1 Error(GovernanceError info)
		{
			return new Result1(Result1Tag.Error, info);
		}
		
		public GovernanceError AsError()
		{
			this.ValidateTag(Result1Tag.Error);
			return (GovernanceError)this.Value!;
		}
		
		public static Result1 NeuronId(NeuronId info)
		{
			return new Result1(Result1Tag.NeuronId, info);
		}
		
		public NeuronId AsNeuronId()
		{
			this.ValidateTag(Result1Tag.NeuronId);
			return (NeuronId)this.Value!;
		}
		
		private void ValidateTag(Result1Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum Result1Tag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Error")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Error,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronId")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(NeuronId))]
		NeuronId,
	}
}

