using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum Result_5Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NodeProvider))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
	public class Result_5 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result_5Type>
	{
		public Result_5(Result_5Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result_5()
		{
		}
		
		public static Result_5 Ok(NodeProvider info)
		{
			return new Result_5(Result_5Type.Ok, info);
		}
		
		public NodeProvider AsOk()
		{
			this.ValidateType(Result_5Type.Ok);
			return (NodeProvider)this.value!;
		}
		
		public static Result_5 Err(GovernanceError info)
		{
			return new Result_5(Result_5Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result_5Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
}

