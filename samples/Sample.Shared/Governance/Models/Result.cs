using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum ResultType
	{
		Ok,
		Err,
	}
	public class Result : EdjCase.ICP.Candid.CandidVariantValueBase<ResultType>
	{
		public Result(ResultType type, object? value)  : base(type, value)
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
}

