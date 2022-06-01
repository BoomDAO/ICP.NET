namespace Sample.Shared.Governance.Models
{
	public enum ByType
	{
		NeuronIdOrSubaccount,
		MemoAndController,
		Memo,
	}
	public class By
	{
		public ByType Type { get; }
		private readonly object? value;
		
		public By(ByType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static By NeuronIdOrSubaccount(NeuronIdOrSubaccountInfo info)
		{
			return new By(ByType.NeuronIdOrSubaccount, info);
		}
		
		public NeuronIdOrSubaccountInfo AsNeuronIdOrSubaccount()
		{
			this.ValidateType(ByType.NeuronIdOrSubaccount);
			return (NeuronIdOrSubaccountInfo)this.value!;
		}
		
		public static By MemoAndController(ClaimOrRefreshNeuronFromAccount info)
		{
			return new By(ByType.MemoAndController, info);
		}
		
		public ClaimOrRefreshNeuronFromAccount AsMemoAndController()
		{
			this.ValidateType(ByType.MemoAndController);
			return (ClaimOrRefreshNeuronFromAccount)this.value!;
		}
		
		public static By Memo(ulong info)
		{
			return new By(ByType.Memo, info);
		}
		
		public ulong AsMemo()
		{
			this.ValidateType(ByType.Memo);
			return (ulong)this.value!;
		}
		
		private void ValidateType(ByType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
		public class NeuronIdOrSubaccountInfo
		{
		}
	}
}
