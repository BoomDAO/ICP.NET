using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Result5 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result5Type>
	{
		public Result5(Result5Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result5()
		{
		}
		
		public static Result5 Ok(NodeProvider info)
		{
			return new Result5(Result5Type.Ok, info);
		}
		
		public NodeProvider AsOk()
		{
			this.ValidateType(Result5Type.Ok);
			return (NodeProvider)this.value!;
		}
		
		public static Result5 Err(GovernanceError info)
		{
			return new Result5(Result5Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result5Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
	public enum Result5Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NodeProvider))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

