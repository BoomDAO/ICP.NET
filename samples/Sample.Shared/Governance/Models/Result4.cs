using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Result4 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result4Type>
	{
		public Result4(Result4Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result4()
		{
		}
		
		public static Result4 Ok(NeuronInfo info)
		{
			return new Result4(Result4Type.Ok, info);
		}
		
		public NeuronInfo AsOk()
		{
			this.ValidateType(Result4Type.Ok);
			return (NeuronInfo)this.value!;
		}
		
		public static Result4 Err(GovernanceError info)
		{
			return new Result4(Result4Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result4Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
	public enum Result4Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NeuronInfo))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

