namespace Sample.Shared.Governance.Models
{
	public enum Result3Type
	{
		Ok,
		Err,
	}
	public class Result3 : EdjCase.ICP.Candid.CandidVariantValueBase<Result3Type>
	{
		public Result3(Result3Type type, object? value)  : base(type, value)
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
}
