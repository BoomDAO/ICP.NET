namespace Sample.Shared.Governance.Models
{
	public enum Result1Type
	{
		Error,
		NeuronId,
	}
	public class Result1
	{
		public Result1Type Type { get; }
		private readonly object? value;
		
		public Result1(Result1Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static Result1 Error(GovernanceError info)
		{
			return new Result1(Result1Type.Error, info);
		}
		
		public GovernanceError AsError()
		{
			this.ValidateType(Result1Type.Error);
			return (GovernanceError)this.value!;
		}
		
		public static Result1 NeuronId(NeuronId info)
		{
			return new Result1(Result1Type.NeuronId, info);
		}
		
		public NeuronId AsNeuronId()
		{
			this.ValidateType(Result1Type.NeuronId);
			return (NeuronId)this.value!;
		}
		
		private void ValidateType(Result1Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
