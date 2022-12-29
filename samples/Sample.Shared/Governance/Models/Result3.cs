using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Result3 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result3Type>
	{
		public Result3(Result3Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result3()
		{
		}
		
		public static Result3 Ok(RewardNodeProviders info)
		{
			return new Result3(Result3Type.Ok, info);
		}
		
		public RewardNodeProviders AsOk()
		{
			this.ValidateType(Result3Type.Ok);
			return (RewardNodeProviders)this.value!;
		}
		
		public static Result3 Err(GovernanceError info)
		{
			return new Result3(Result3Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result3Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
	public enum Result3Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(RewardNodeProviders))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

