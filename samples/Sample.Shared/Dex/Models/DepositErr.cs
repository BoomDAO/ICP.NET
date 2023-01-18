namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(DepositErrTag))]
	public class DepositErr
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public DepositErrTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private DepositErr(DepositErrTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected DepositErr()
		{
		}
		
		public static DepositErr BalanceLow()
		{
			return new DepositErr(DepositErrTag.BalanceLow, null);
		}
		
		public static DepositErr TransferFailure()
		{
			return new DepositErr(DepositErrTag.TransferFailure, null);
		}
		
	}
	public enum DepositErrTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("BalanceLow")]
		BalanceLow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("TransferFailure")]
		TransferFailure,
	}
}

