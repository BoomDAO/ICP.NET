using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	[Variant]
	public class CancelOrderReceipt
	{
		[VariantTagProperty]
		public CancelOrderReceiptTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public CancelOrderErr? Err { get => this.Tag == CancelOrderReceiptTag.Err ? (CancelOrderErr)this.Value! : default; set => (this.Tag, this.Value) = (CancelOrderReceiptTag.Err, value); }

		public OrderId? Ok { get => this.Tag == CancelOrderReceiptTag.Ok ? (OrderId)this.Value! : default; set => (this.Tag, this.Value) = (CancelOrderReceiptTag.Ok, value); }

		public CancelOrderReceipt(CancelOrderReceiptTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected CancelOrderReceipt()
		{
		}
	}

	public enum CancelOrderReceiptTag
	{
		Err,
		Ok
	}
}