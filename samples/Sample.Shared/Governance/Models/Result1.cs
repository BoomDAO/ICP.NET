namespace Sample.Shared.Governance.Models
{
	public enum Result1Type
	{
		Error,
		NeuronId,
	}
	public class Result1 : EdjCase.ICP.Candid.CandidVariantValueBase<Result1Type>
	{
		public Result1(Result1Type type, object? value)  : base(type, value)
		{
		}
		
		protected Result1()
		{
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
		
	}
}
