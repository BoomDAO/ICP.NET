namespace Sample.Shared.Governance.Models
{
	public enum Result4Type
	{
		Ok,
		Err,
	}
	public class Result4
	{
		public Result4Type Type { get; }
		private readonly object? value;
		
		public Result4(Result4Type type, object? value)
		{
			this.Type = type;
			this.value = value;
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
		
		private void ValidateType(Result4Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
