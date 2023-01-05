using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(CancelOrderReceiptTag))]
	public class CancelOrderReceipt
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public CancelOrderReceiptTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private CancelOrderReceipt(CancelOrderReceiptTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected CancelOrderReceipt()
		{
		}
		
		public static CancelOrderReceipt Err(CancelOrderErr info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptTag.Err, info);
		}
		
		public CancelOrderErr AsErr()
		{
			this.ValidateTag(CancelOrderReceiptTag.Err);
			return (CancelOrderErr)this.Value!;
		}
		
		public static CancelOrderReceipt Ok(OrderId info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptTag.Ok, info);
		}
		
		public OrderId AsOk()
		{
			this.ValidateTag(CancelOrderReceiptTag.Ok);
			return (OrderId)this.Value!;
		}
		
		private void ValidateTag(CancelOrderReceiptTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum CancelOrderReceiptTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(CancelOrderErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(OrderId))]
		Ok,
	}
}

