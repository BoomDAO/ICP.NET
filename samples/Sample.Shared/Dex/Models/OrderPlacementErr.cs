using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Dex.Models
{
	public enum OrderPlacementErrType
	{
		InvalidOrder,
		OrderBookFull,
	}
	public class OrderPlacementErr : EdjCase.ICP.Candid.CandidVariantValueBase<OrderPlacementErrType>
	{
		public OrderPlacementErr(OrderPlacementErrType type, object? value)  : base(type, value)
		{
		}
		
		protected OrderPlacementErr()
		{
		}
		
		public static OrderPlacementErr InvalidOrder()
		{
			return new OrderPlacementErr(OrderPlacementErrType.InvalidOrder, null);
		}
		
		public static OrderPlacementErr OrderBookFull()
		{
			return new OrderPlacementErr(OrderPlacementErrType.OrderBookFull, null);
		}
		
	}
}

