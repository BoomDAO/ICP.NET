using System;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(WithdrawReceiptTag))]
	public class WithdrawReceipt
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public WithdrawReceiptTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private WithdrawReceipt(WithdrawReceiptTag tag, System.Object? value)
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
		
		public WithdrawErr AsErr()
		{
			this.ValidateTag(WithdrawReceiptTag.Err);
			return (WithdrawErr)this.Value!;
		}
		
		public static WithdrawReceipt Ok(UnboundedUInt info)
		{
			return new WithdrawReceipt(WithdrawReceiptTag.Ok, info);
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
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(WithdrawErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(UnboundedUInt))]
		Ok,
	}
}

