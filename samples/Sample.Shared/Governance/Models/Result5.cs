namespace Sample.Shared.Governance.Models
{
	public enum Result5Type
	{
		Ok,
		Err,
	}
	public class Result5
	{
		public Result5Type Type { get; }
		private readonly object? value;
		
		public Result5(Result5Type type, object? value)
		{
			this.Type = type;
			this.value = value;
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
		
		private void ValidateType(Result5Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
