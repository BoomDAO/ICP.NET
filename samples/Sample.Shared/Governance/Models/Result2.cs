namespace Sample.Shared.Governance.Models
{
	public enum Result2Type
	{
		Ok,
		Err,
	}
	public class Result2 : EdjCase.ICP.Candid.CandidVariantValueBase<Result2Type>
	{
		public Result2(Result2Type type, object? value)  : base(type, value)
		{
		}
		
		protected Result2()
		{
		}
		
		public static Result2 Ok(Neuron info)
		{
			return new Result2(Result2Type.Ok, info);
		}
		
		public Neuron AsOk()
		{
			this.ValidateType(Result2Type.Ok);
			return (Neuron)this.value!;
		}
		
		public static Result2 Err(GovernanceError info)
		{
			return new Result2(Result2Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result2Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
}
