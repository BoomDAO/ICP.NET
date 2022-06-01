namespace Sample.Shared.Dex.Models
{
	public enum OrderPlacementErrType
	{
		InvalidOrder,
		OrderBookFull,
	}
	public class OrderPlacementErr
	{
		public OrderPlacementErrType Type { get; }
		private readonly object? value;
		
		public OrderPlacementErr(OrderPlacementErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static OrderPlacementErr InvalidOrder()
		{
			return new OrderPlacementErr(OrderPlacementErrType.InvalidOrder, null);
		}
		
		public static OrderPlacementErr OrderBookFull()
		{
			return new OrderPlacementErr(OrderPlacementErrType.OrderBookFull, null);
		}
		
		private void ValidateType(OrderPlacementErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
