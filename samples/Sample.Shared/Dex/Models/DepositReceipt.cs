using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Dex.Models
{
	public enum DepositReceiptType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(DepositErr))]
		Err,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(EdjCase.ICP.Candid.UnboundedUInt))]
		Ok,
	}
	public class DepositReceipt : EdjCase.ICP.Candid.Models.CandidVariantValueBase<DepositReceiptType>
	{
		public DepositReceipt(DepositReceiptType type, System.Object? value)  : base(type, value)
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

