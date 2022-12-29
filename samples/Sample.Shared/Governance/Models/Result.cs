using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Result : EdjCase.ICP.Candid.Models.CandidVariantValueBase<ResultType>
	{
		public Result(ResultType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result()
		{
		}
		
		public static Result Ok()
		{
			return new Result(ResultType.Ok, null);
		}
		
		public static Result Err(GovernanceError info)
		{
			return new Result(ResultType.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(ResultType.Err);
			return (GovernanceError)this.value!;
		}
		
	}
	public enum ResultType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

