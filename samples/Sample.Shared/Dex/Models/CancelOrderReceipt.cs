namespace Sample.Shared.Dex.Models
{
	public enum CancelOrderReceiptType
	{
		Err,
		Ok,
	}
	public class CancelOrderReceipt
	{
		public CancelOrderReceiptType Type { get; }
		private readonly object? value;
		
		public CancelOrderReceipt(CancelOrderReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static CancelOrderReceipt Err(CancelOrderErr info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptType.Err, info);
		}
		
		public CancelOrderErr AsErr()
		{
			this.ValidateType(CancelOrderReceiptType.Err);
			return (CancelOrderErr)this.value!;
		}
		
		public static CancelOrderReceipt Ok(OrderId info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptType.Ok, info);
		}
		
		public OrderId AsOk()
		{
			this.ValidateType(CancelOrderReceiptType.Ok);
			return (OrderId)this.value!;
		}
		
		private void ValidateType(CancelOrderReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
