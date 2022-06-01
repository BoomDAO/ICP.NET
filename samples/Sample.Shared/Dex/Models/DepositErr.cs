namespace Sample.Shared.Dex.Models
{
	public enum DepositErrType
	{
		BalanceLow,
		TransferFailure,
	}
	public class DepositErr
	{
		public DepositErrType Type { get; }
		private readonly object? value;
		
		public DepositErr(DepositErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static DepositErr BalanceLow()
		{
			return new DepositErr(DepositErrType.BalanceLow, null);
		}
		
		public static DepositErr TransferFailure()
		{
			return new DepositErr(DepositErrType.TransferFailure, null);
		}
		
		private void ValidateType(DepositErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
