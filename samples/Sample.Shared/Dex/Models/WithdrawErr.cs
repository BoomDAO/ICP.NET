using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Dex.Models
{
	public enum WithdrawErrType
	{
		BalanceLow,
		TransferFailure,
	}
	public class WithdrawErr : EdjCase.ICP.Candid.CandidVariantValueBase<WithdrawErrType>
	{
		public WithdrawErr(WithdrawErrType type, object? value)  : base(type, value)
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

