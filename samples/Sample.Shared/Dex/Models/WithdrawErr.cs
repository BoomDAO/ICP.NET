namespace Sample.Shared.Dex.Models
{
	public enum WithdrawErrType
	{
		BalanceLow,
		TransferFailure,
	}
	public class WithdrawErr
	{
		public WithdrawErrType Type { get; }
		private readonly object? value;
		
		public WithdrawErr(WithdrawErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static WithdrawErr BalanceLow()
		{
			return new WithdrawErr(WithdrawErrType.BalanceLow, null);
		}
		
		public static WithdrawErr TransferFailure()
		{
			return new WithdrawErr(WithdrawErrType.TransferFailure, null);
		}
		
		private void ValidateType(WithdrawErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
