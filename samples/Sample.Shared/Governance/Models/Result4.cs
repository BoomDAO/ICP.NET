namespace Sample.Shared.Governance.Models
{
	public enum Result4Type
	{
		Ok,
		Err,
	}
	public class Result4 : EdjCase.ICP.Candid.CandidVariantValueBase<Result4Type>
	{
		public Result4(Result4Type type, object? value)  : base(type, value)
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
}
