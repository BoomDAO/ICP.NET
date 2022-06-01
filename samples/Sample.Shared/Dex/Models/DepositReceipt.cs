namespace Sample.Shared.Dex.Models
{
	public enum DepositReceiptType
	{
		Err,
		Ok,
	}
	public class DepositReceipt
	{
		public DepositReceiptType Type { get; }
		private readonly object? value;
		
		public DepositReceipt(DepositReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static DepositReceipt Err(DepositErr info)
		{
			return new DepositReceipt(DepositReceiptType.Err, info);
		}
		
		public DepositErr AsErr()
		{
			this.ValidateType(DepositReceiptType.Err);
			return (DepositErr)this.value!;
		}
		
		public static DepositReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new DepositReceipt(DepositReceiptType.Ok, info);
		}
		
		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateType(DepositReceiptType.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.value!;
		}
		
		private void ValidateType(DepositReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
