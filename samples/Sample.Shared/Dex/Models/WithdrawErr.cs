using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Dex.Models
{
	public enum WithdrawErrType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("BalanceLow")]
		BalanceLow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("TransferFailure")]
		TransferFailure,
	}
	public class WithdrawErr : EdjCase.ICP.Candid.Models.CandidVariantValueBase<WithdrawErrType>
	{
		public WithdrawErr(WithdrawErrType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected WithdrawErr()
		{
		}
		
		public static WithdrawErr BalanceLow()
		{
			return new WithdrawErr(WithdrawErrType.BalanceLow, null);
		}
		
		public static WithdrawErr TransferFailure()
		{
			return new WithdrawErr(WithdrawErrType.TransferFailure, null);
		}
		
	}
}

