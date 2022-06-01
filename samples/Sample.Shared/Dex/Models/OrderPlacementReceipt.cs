namespace Sample.Shared.Dex.Models
{
	public enum OrderPlacementReceiptType
	{
		Err,
		Ok,
	}
	public class OrderPlacementReceipt
	{
		public OrderPlacementReceiptType Type { get; }
		private readonly object? value;
		
		public OrderPlacementReceipt(OrderPlacementReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static OrderPlacementReceipt Err(OrderPlacementErr info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptType.Err, info);
		}
		
		public OrderPlacementErr AsErr()
		{
			this.ValidateType(OrderPlacementReceiptType.Err);
			return (OrderPlacementErr)this.value!;
		}
		
		public static OrderPlacementReceipt Ok(Order? info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptType.Ok, info);
		}
		
		public Order? AsOk()
		{
			this.ValidateType(OrderPlacementReceiptType.Ok);
			return (Order?)this.value!;
		}
		
		private void ValidateType(OrderPlacementReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
