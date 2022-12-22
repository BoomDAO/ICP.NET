using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum Result_1Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Error")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Error,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronId")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NeuronId))]
		NeuronId,
	}
	public class Result_1 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result_1Type>
	{
		public Result_1(Result_1Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result_1()
		{
		}
		
		public static Result_1 Error(GovernanceError info)
		{
			return new Result_1(Result_1Type.Error, info);
		}
		
		public GovernanceError AsError()
		{
			this.ValidateType(Result_1Type.Error);
			return (GovernanceError)this.value!;
		}
		
		public static Result_1 NeuronId(NeuronId info)
		{
			return new Result_1(Result_1Type.NeuronId, info);
		}
		
		public NeuronId AsNeuronId()
		{
			this.ValidateType(Result_1Type.NeuronId);
			return (NeuronId)this.value!;
		}
		
	}
}

