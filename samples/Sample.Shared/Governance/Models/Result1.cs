using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Result1 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result1Type>
	{
		public Result1(Result1Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result1()
		{
		}
		
		public static Result1 Error(GovernanceError info)
		{
			return new Result1(Result1Type.Error, info);
		}
		
		public GovernanceError AsError()
		{
			this.ValidateType(Result1Type.Error);
			return (GovernanceError)this.value!;
		}
		
		public static Result1 NeuronId(NeuronId info)
		{
			return new Result1(Result1Type.NeuronId, info);
		}
		
		public NeuronId AsNeuronId()
		{
			this.ValidateType(Result1Type.NeuronId);
			return (NeuronId)this.value!;
		}
		
	}
	public enum Result1Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Error")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Error,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronId")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NeuronId))]
		NeuronId,
	}
}

