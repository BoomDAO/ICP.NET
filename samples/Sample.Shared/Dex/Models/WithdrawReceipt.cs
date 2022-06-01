namespace Sample.Shared.Dex.Models
{
	public enum WithdrawReceiptType
	{
		Err,
		Ok,
	}
	public class WithdrawReceipt
	{
		public WithdrawReceiptType Type { get; }
		private readonly object? value;
		
		public WithdrawReceipt(WithdrawReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static WithdrawReceipt Err(WithdrawErr info)
		{
			return new WithdrawReceipt(WithdrawReceiptType.Err, info);
		}
		
		public WithdrawErr AsErr()
		{
			this.ValidateType(WithdrawReceiptType.Err);
			return (WithdrawErr)this.value!;
		}
		
		public static WithdrawReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new WithdrawReceipt(WithdrawReceiptType.Ok, info);
		}
		
		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateType(WithdrawReceiptType.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.value!;
		}
		
		private void ValidateType(WithdrawReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
