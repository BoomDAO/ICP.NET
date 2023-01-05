using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

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
		
		public static WithdrawReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new WithdrawReceipt(WithdrawReceiptTag.Ok, info);
		}
		
		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateTag(WithdrawReceiptTag.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.Value!;
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
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(EdjCase.ICP.Candid.UnboundedUInt))]
		Ok,
	}
}

