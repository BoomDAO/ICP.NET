namespace Sample.Shared.Governance.Models
{
	public enum ByType
	{
		NeuronIdOrSubaccount,
		MemoAndController,
		Memo,
	}
	public class By : EdjCase.ICP.Candid.CandidVariantValueBase<ByType>
	{
		public By(ByType type, object? value)  : base(type, value)
		{
		}
		
		protected By()
		{
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
		
		public class NeuronIdOrSubaccountInfo
		{
		}
	}
}
