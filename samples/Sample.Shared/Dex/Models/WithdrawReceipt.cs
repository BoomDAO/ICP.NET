using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Dex.Models
{
	public enum WithdrawReceiptType
	{
		Err,
		Ok,
	}
	public class WithdrawReceipt : EdjCase.ICP.Candid.CandidVariantValueBase<WithdrawReceiptType>
	{
		public WithdrawReceipt(WithdrawReceiptType type, object? value)  : base(type, value)
		{
		}
		
		protected WithdrawReceipt()
		{
		}
		
		public static WithdrawReceipt Err(WithdrawErr info)
		{
			return new WithdrawReceipt(WithdrawReceiptType.Err, info);
		}
		
		public WithdrawErr AsErr()
		{
			this.ValidateType(WithdrawReceiptType.Err);
			return (WithdrawErr)this.value!;
		}
		
		public static WithdrawReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new WithdrawReceipt(WithdrawReceiptType.Ok, info);
		}
		
		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateType(WithdrawReceiptType.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.value!;
		}
		
	}
}

