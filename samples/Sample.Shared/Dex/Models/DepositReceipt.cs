using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Dex.Models;
using EdjCase.ICP.Candid.Models;
using System;

namespace Sample.Shared.Dex.Models
{
	[Variant(typeof(DepositReceiptTag))]
	public class DepositReceipt
	{
		[VariantTagProperty()]
		public DepositReceiptTag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public DepositReceipt(DepositReceiptTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected DepositReceipt()
		{
		}

		public static DepositReceipt Err(DepositErr info)
		{
			return new DepositReceipt(DepositReceiptTag.Err, info);
		}

		public static DepositReceipt Ok(UnboundedUInt info)
		{
			return new DepositReceipt(DepositReceiptTag.Ok, info);
		}

		public DepositErr AsErr()
		{
			this.ValidateTag(DepositReceiptTag.Err);
			return (DepositErr)this.Value!;
		}

		public UnboundedUInt AsOk()
		{
			this.ValidateTag(DepositReceiptTag.Ok);
			return (UnboundedUInt)this.Value!;
		}

		private void ValidateTag(DepositReceiptTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum DepositReceiptTag
	{
		[VariantOptionType(typeof(DepositErr))]
		Err,
		[VariantOptionType(typeof(UnboundedUInt))]
		Ok
	}
}