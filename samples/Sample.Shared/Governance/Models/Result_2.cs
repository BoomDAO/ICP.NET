using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum Result_2Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Neuron))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
	public class Result_2 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result_2Type>
	{
		public Result_2(Result_2Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result_2()
		{
		}
		
		public static Result_2 Ok(Neuron info)
		{
			return new Result_2(Result_2Type.Ok, info);
		}
		
		public Neuron AsOk()
		{
			this.ValidateType(Result_2Type.Ok);
			return (Neuron)this.value!;
		}
		
		public static Result_2 Err(GovernanceError info)
		{
			return new Result_2(Result_2Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result_2Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
}

