using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Dex.Models
{
	[Variant]
	public class OrderPlacementReceipt
	{
		[VariantTagProperty]
		public OrderPlacementReceiptTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public OrderPlacementErr? Err { get => this.Tag == OrderPlacementReceiptTag.Err ? (OrderPlacementErr)this.Value! : default; set => (this.Tag, this.Value) = (OrderPlacementReceiptTag.Err, value); }

		public OptionalValue<Order>? Ok { get => this.Tag == OrderPlacementReceiptTag.Ok ? (OptionalValue<Order>)this.Value! : default; set => (this.Tag, this.Value) = (OrderPlacementReceiptTag.Ok, value); }

		public OrderPlacementReceipt(OrderPlacementReceiptTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected OrderPlacementReceipt()
		{
		}
	}

	public enum OrderPlacementReceiptTag
	{
		Err,
		Ok
	}
}