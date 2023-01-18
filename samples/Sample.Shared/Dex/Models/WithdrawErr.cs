namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(WithdrawErrTag))]
	public class WithdrawErr
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public WithdrawErrTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private WithdrawErr(WithdrawErrTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected WithdrawErr()
		{
		}
		
		public static WithdrawErr BalanceLow()
		{
			return new WithdrawErr(WithdrawErrTag.BalanceLow, null);
		}
		
		public static WithdrawErr TransferFailure()
		{
			return new WithdrawErr(WithdrawErrTag.TransferFailure, null);
		}
		
	}
	public enum WithdrawErrTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("BalanceLow")]
		BalanceLow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("TransferFailure")]
		TransferFailure,
	}
}

