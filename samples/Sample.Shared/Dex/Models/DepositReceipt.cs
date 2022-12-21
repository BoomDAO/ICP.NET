using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Dex.Models
{
	public enum DepositReceiptType
	{
		Err,
		Ok,
	}
	public class DepositReceipt : EdjCase.ICP.Candid.CandidVariantValueBase<DepositReceiptType>
	{
		public DepositReceipt(DepositReceiptType type, object? value)  : base(type, value)
		{
		}
		
		protected DepositReceipt()
		{
		}
		
		public static DepositReceipt Err(DepositErr info)
		{
			return new DepositReceipt(DepositReceiptType.Err, info);
		}
		
		public DepositErr AsErr()
		{
			this.ValidateType(DepositReceiptType.Err);
			return (DepositErr)this.value!;
		}
		
		public static DepositReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new DepositReceipt(DepositReceiptType.Ok, info);
		}
		
		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateType(DepositReceiptType.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.value!;
		}
		
	}
}

