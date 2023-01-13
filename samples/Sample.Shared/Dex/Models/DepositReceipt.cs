using System;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(DepositReceiptTag))]
	public class DepositReceipt
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public DepositReceiptTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private DepositReceipt(DepositReceiptTag tag, System.Object? value)
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
		
		public DepositErr AsErr()
		{
			this.ValidateTag(DepositReceiptTag.Err);
			return (DepositErr)this.Value!;
		}
		
		public static DepositReceipt Ok(UnboundedUInt info)
		{
			return new DepositReceipt(DepositReceiptTag.Ok, info);
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
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(DepositErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(UnboundedUInt))]
		Ok,
	}
}

