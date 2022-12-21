using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Dex.Models
{
	public enum DepositErrType
	{
		BalanceLow,
		TransferFailure,
	}
	public class DepositErr : EdjCase.ICP.Candid.CandidVariantValueBase<DepositErrType>
	{
		public DepositErr(DepositErrType type, object? value)  : base(type, value)
		{
		}
		
		protected DepositErr()
		{
		}
		
		public static DepositErr BalanceLow()
		{
			return new DepositErr(DepositErrType.BalanceLow, null);
		}
		
		public static DepositErr TransferFailure()
		{
			return new DepositErr(DepositErrType.TransferFailure, null);
		}
		
	}
}

