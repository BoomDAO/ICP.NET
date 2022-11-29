namespace Sample.Shared.Dex.Models
{
	public enum OrderPlacementReceiptType
	{
		Err,
		Ok,
	}
	public class OrderPlacementReceipt : EdjCase.ICP.Candid.CandidVariantValueBase<OrderPlacementReceiptType>
	{
		public OrderPlacementReceipt(OrderPlacementReceiptType type, object? value)  : base(type, value)
		{
		}
		
		protected OrderPlacementReceipt()
		{
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
		
	}
}
