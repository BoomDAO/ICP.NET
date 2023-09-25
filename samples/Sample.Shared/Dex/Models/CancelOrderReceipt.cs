using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using System;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	[Variant()]
	public class CancelOrderReceipt
	{
		[VariantTagProperty()]
		public CancelOrderReceiptTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public CancelOrderReceipt(CancelOrderReceiptTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected CancelOrderReceipt()
		{
		}

		public static CancelOrderReceipt Err(CancelOrderErr info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptTag.Err, info);
		}

		public static CancelOrderReceipt Ok(OrderId info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptTag.Ok, info);
		}

		public CancelOrderErr AsErr()
		{
			this.ValidateTag(CancelOrderReceiptTag.Err);
			return (CancelOrderErr)this.Value!;
		}

		public OrderId AsOk()
		{
			this.ValidateTag(CancelOrderReceiptTag.Ok);
			return (OrderId)this.Value!;
		}

		private void ValidateTag(CancelOrderReceiptTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum CancelOrderReceiptTag
	{
		Err,
		Ok
	}
}