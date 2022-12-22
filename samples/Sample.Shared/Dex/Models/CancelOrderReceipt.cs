using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Dex.Models
{
	public enum CancelOrderReceiptType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(CancelOrderErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(OrderId))]
		Ok,
	}
	public class CancelOrderReceipt : EdjCase.ICP.Candid.Models.CandidVariantValueBase<CancelOrderReceiptType>
	{
		public CancelOrderReceipt(CancelOrderReceiptType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected CancelOrderReceipt()
		{
		}
		
		public static CancelOrderReceipt Err(CancelOrderErr info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptType.Err, info);
		}
		
		public CancelOrderErr AsErr()
		{
			this.ValidateType(CancelOrderReceiptType.Err);
			return (CancelOrderErr)this.value!;
		}
		
		public static CancelOrderReceipt Ok(OrderId info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptType.Ok, info);
		}
		
		public OrderId AsOk()
		{
			this.ValidateType(CancelOrderReceiptType.Ok);
			return (OrderId)this.value!;
		}
		
	}
}

