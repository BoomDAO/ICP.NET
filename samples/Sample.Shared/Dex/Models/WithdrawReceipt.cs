using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Dex.Models
{
	[Variant]
	public class WithdrawReceipt
	{
		[VariantTagProperty]
		public WithdrawReceiptTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public WithdrawErr? Err { get => this.Tag == WithdrawReceiptTag.Err ? (WithdrawErr)this.Value! : default; set => (this.Tag, this.Value) = (WithdrawReceiptTag.Err, value); }

		public UnboundedUInt? Ok { get => this.Tag == WithdrawReceiptTag.Ok ? (UnboundedUInt)this.Value! : default; set => (this.Tag, this.Value) = (WithdrawReceiptTag.Ok, value); }

		public WithdrawReceipt(WithdrawReceiptTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected WithdrawReceipt()
		{
		}
	}

	public enum WithdrawReceiptTag
	{
		Err,
		Ok
	}
}