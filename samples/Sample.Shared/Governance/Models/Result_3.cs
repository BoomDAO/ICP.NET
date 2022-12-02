namespace Sample.Shared.Governance.Models
{
	public enum Result_3Type
	{
		Ok,
		Err,
	}
	public class Result_3 : EdjCase.ICP.Candid.CandidVariantValueBase<Result_3Type>
	{
		public Result_3(Result_3Type type, object? value)  : base(type, value)
		{
		}
		
		protected Result_3()
		{
		}
		
		public static Result_3 Ok(RewardNodeProviders info)
		{
			return new Result_3(Result_3Type.Ok, info);
		}
		
		public RewardNodeProviders AsOk()
		{
			this.ValidateType(Result_3Type.Ok);
			return (RewardNodeProviders)this.value!;
		}
		
		public static Result_3 Err(GovernanceError info)
		{
			return new Result_3(Result_3Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result_3Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
}
