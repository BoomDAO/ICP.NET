namespace Sample.Shared.Governance.Models
{
	public enum Result_2Type
	{
		Ok,
		Err,
	}
	public class Result_2 : EdjCase.ICP.Candid.CandidVariantValueBase<Result_2Type>
	{
		public Result_2(Result_2Type type, object? value)  : base(type, value)
		{
		}
		
		protected Result_2()
		{
		}
		
		public static Result_2 Ok(Neuron info)
		{
			return new Result_2(Result_2Type.Ok, info);
		}
		
		public Neuron AsOk()
		{
			this.ValidateType(Result_2Type.Ok);
			return (Neuron)this.value!;
		}
		
		public static Result_2 Err(GovernanceError info)
		{
			return new Result_2(Result_2Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result_2Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
}
