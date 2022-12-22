using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum Result_4Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NeuronInfo))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
	public class Result_4 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result_4Type>
	{
		public Result_4(Result_4Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result_4()
		{
		}
		
		public static Result_4 Ok(NeuronInfo info)
		{
			return new Result_4(Result_4Type.Ok, info);
		}
		
		public NeuronInfo AsOk()
		{
			this.ValidateType(Result_4Type.Ok);
			return (NeuronInfo)this.value!;
		}
		
		public static Result_4 Err(GovernanceError info)
		{
			return new Result_4(Result_4Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result_4Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
}

