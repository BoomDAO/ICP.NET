namespace Sample.Shared.Governance.Models
{
	public enum ResultType
	{
		Ok,
		Err,
	}
	public class Result
	{
		public ResultType Type { get; }
		private readonly object? value;
		
		public Result(ResultType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static Result Ok()
		{
			return new Result(ResultType.Ok, null);
		}
		
		public static Result Err(GovernanceError info)
		{
			return new Result(ResultType.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(ResultType.Err);
			return (GovernanceError)this.value!;
		}
		
		private void ValidateType(ResultType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
