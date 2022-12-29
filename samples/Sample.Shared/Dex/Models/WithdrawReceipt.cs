using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	public class WithdrawReceipt : EdjCase.ICP.Candid.Models.CandidVariantValueBase<WithdrawReceiptType>
	{
		public WithdrawReceipt(WithdrawReceiptType type, System.Object? value)  : base(type, value)
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
	public enum WithdrawReceiptType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(WithdrawErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(EdjCase.ICP.Candid.UnboundedUInt))]
		Ok,
	}
}

