namespace Sample.Shared.Governance.Models
{
	public enum Result2Type
	{
		Ok,
		Err,
	}
	public class Result2
	{
		public Result2Type Type { get; }
		private readonly object? value;
		
		public Result2(Result2Type type, object? value)
		{
			this.Type = type;
			this.value = value;
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
		
		private void ValidateType(Result2Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
