namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(OrderPlacementErrTag))]
	public class OrderPlacementErr
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public OrderPlacementErrTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private OrderPlacementErr(OrderPlacementErrTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected OrderPlacementErr()
		{
		}
		
		public static OrderPlacementErr InvalidOrder()
		{
			return new OrderPlacementErr(OrderPlacementErrTag.InvalidOrder, null);
		}
		
		public static OrderPlacementErr OrderBookFull()
		{
			return new OrderPlacementErr(OrderPlacementErrTag.OrderBookFull, null);
		}
		
	}
	public enum OrderPlacementErrTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("InvalidOrder")]
		InvalidOrder,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("OrderBookFull")]
		OrderBookFull,
	}
}

