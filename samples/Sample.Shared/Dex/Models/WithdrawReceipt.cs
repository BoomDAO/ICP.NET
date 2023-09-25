using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using EdjCase.ICP.Candid.Models;
using System;

namespace Sample.Shared.Dex.Models
{
	[Variant()]
	public class WithdrawReceipt
	{
		[VariantTagProperty()]
		public WithdrawReceiptTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public WithdrawReceipt(WithdrawReceiptTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected WithdrawReceipt()
		{
		}

		public static WithdrawReceipt Err(WithdrawErr info)
		{
			return new WithdrawReceipt(WithdrawReceiptTag.Err, info);
		}

		public static WithdrawReceipt Ok(UnboundedUInt info)
		{
			return new WithdrawReceipt(WithdrawReceiptTag.Ok, info);
		}

		public WithdrawErr AsErr()
		{
			this.ValidateTag(WithdrawReceiptTag.Err);
			return (WithdrawErr)this.Value!;
		}

		public UnboundedUInt AsOk()
		{
			this.ValidateTag(WithdrawReceiptTag.Ok);
			return (UnboundedUInt)this.Value!;
		}

		private void ValidateTag(WithdrawReceiptTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum WithdrawReceiptTag
	{
		Err,
		Ok
	}
}