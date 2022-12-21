using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Dex.Models
{
	public enum CancelOrderReceiptType
	{
		Err,
		Ok,
	}
	public class CancelOrderReceipt : EdjCase.ICP.Candid.CandidVariantValueBase<CancelOrderReceiptType>
	{
		public CancelOrderReceipt(CancelOrderReceiptType type, object? value)  : base(type, value)
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

