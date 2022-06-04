namespace Sample.Shared.Governance.Models
{
	public enum Result5Type
	{
		Ok,
		Err,
	}
	public class Result5 : EdjCase.ICP.Candid.CandidVariantValueBase<Result5Type>
	{
		public Result5(Result5Type type, object? value)  : base(type, value)
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
}
