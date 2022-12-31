using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

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
			this.ValidateType(DepositReceiptTag.Err);
			return (DepositErr)this.Value!;
		}
		
		public static DepositReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new DepositReceipt(DepositReceiptTag.Ok, info);
		}
		
		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateType(DepositReceiptTag.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.Value!;
		}
		
		private void ValidateType(DepositReceiptTag tag)
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
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(EdjCase.ICP.Candid.UnboundedUInt))]
		Ok,
	}
}

