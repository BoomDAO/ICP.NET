namespace Sample.Shared.Governance.Models
{
	public enum Result3Type
	{
		Ok,
		Err,
	}
	public class Result3
	{
		public Result3Type Type { get; }
		private readonly object? value;
		
		public Result3(Result3Type type, object? value)
		{
			this.Type = type;
			this.value = value;
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
		
		private void ValidateType(Result3Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
