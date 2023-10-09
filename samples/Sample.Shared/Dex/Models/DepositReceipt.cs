using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Dex.Models
{
	[Variant]
	public class DepositReceipt
	{
		[VariantTagProperty]
		public DepositReceiptTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public DepositErr? Err { get => this.Tag == DepositReceiptTag.Err ? (DepositErr)this.Value! : default; set => (this.Tag, this.Value) = (DepositReceiptTag.Err, value); }

		public UnboundedUInt? Ok { get => this.Tag == DepositReceiptTag.Ok ? (UnboundedUInt)this.Value! : default; set => (this.Tag, this.Value) = (DepositReceiptTag.Ok, value); }

		public DepositReceipt(DepositReceiptTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected DepositReceipt()
		{
		}
	}

	public enum DepositReceiptTag
	{
		Err,
		Ok
	}
}