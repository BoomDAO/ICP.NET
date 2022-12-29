using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	public class OrderPlacementErr : EdjCase.ICP.Candid.Models.CandidVariantValueBase<OrderPlacementErrType>
	{
		public OrderPlacementErr(OrderPlacementErrType type, System.Object? value)  : base(type, value)
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
	public enum OrderPlacementErrType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("InvalidOrder")]
		InvalidOrder,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("OrderBookFull")]
		OrderBookFull,
	}
}

