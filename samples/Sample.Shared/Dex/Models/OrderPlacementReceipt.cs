using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using EdjCase.ICP.Candid.Models;
using System;

namespace Sample.Shared.Dex.Models
{
	[Variant(typeof(OrderPlacementReceiptTag))]
	public class OrderPlacementReceipt
	{
		[VariantTagProperty()]
		public OrderPlacementReceiptTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public OrderPlacementReceipt(OrderPlacementReceiptTag tag, object? value)
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

		public static OrderPlacementReceipt Ok(OptionalValue<Order> info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptTag.Ok, info);
		}

		public OrderPlacementErr AsErr()
		{
			this.ValidateTag(OrderPlacementReceiptTag.Err);
			return (OrderPlacementErr)this.Value!;
		}

		public OptionalValue<Order> AsOk()
		{
			this.ValidateTag(OrderPlacementReceiptTag.Ok);
			return (OptionalValue<Order>)this.Value!;
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
		[VariantOptionType(typeof(OrderPlacementErr))]
		Err,
		[VariantOptionType(typeof(OptionalValue<Order>))]
		Ok
	}
}