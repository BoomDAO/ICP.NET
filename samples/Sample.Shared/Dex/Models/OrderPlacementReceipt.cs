using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	public class OrderPlacementReceipt : EdjCase.ICP.Candid.Models.CandidVariantValueBase<OrderPlacementReceiptType>
	{
		public OrderPlacementReceipt(OrderPlacementReceiptType type, System.Object? value)  : base(type, value)
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
		
		public static OrderPlacementReceipt Ok(EdjCase.ICP.Candid.Models.OptionalValue<Order> info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptType.Ok, info);
		}
		
		public EdjCase.ICP.Candid.Models.OptionalValue<Order> AsOk()
		{
			this.ValidateType(OrderPlacementReceiptType.Ok);
			return (EdjCase.ICP.Candid.Models.OptionalValue<Order>)this.value!;
		}
		
	}
	public enum OrderPlacementReceiptType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(OrderPlacementErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(EdjCase.ICP.Candid.Models.OptionalValue<Order>))]
		Ok,
	}
}

