using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum Result_5Type
	{
		Ok,
		Err,
	}
	public class Result_5 : EdjCase.ICP.Candid.CandidVariantValueBase<Result_5Type>
	{
		public Result_5(Result_5Type type, object? value)  : base(type, value)
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

