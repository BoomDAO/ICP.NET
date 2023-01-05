using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(OrderPlacementReceiptTag))]
	public class OrderPlacementReceipt
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public OrderPlacementReceiptTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private OrderPlacementReceipt(OrderPlacementReceiptTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected OrderPlacementReceipt()
		{
		}
		
		public static OrderPlacementReceipt Err(OrderPlacementErr info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptTag.Err, info);
		}
		
		public OrderPlacementErr AsErr()
		{
			this.ValidateTag(OrderPlacementReceiptTag.Err);
			return (OrderPlacementErr)this.Value!;
		}
		
		public static OrderPlacementReceipt Ok(EdjCase.ICP.Candid.Models.OptionalValue<Order> info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptTag.Ok, info);
		}
		
		public EdjCase.ICP.Candid.Models.OptionalValue<Order> AsOk()
		{
			this.ValidateTag(OrderPlacementReceiptTag.Ok);
			return (EdjCase.ICP.Candid.Models.OptionalValue<Order>)this.Value!;
		}
		
		private void ValidateTag(OrderPlacementReceiptTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum OrderPlacementReceiptTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(OrderPlacementErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(EdjCase.ICP.Candid.Models.OptionalValue<Order>))]
		Ok,
	}
}

