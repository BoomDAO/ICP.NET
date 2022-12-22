using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Dex.Models
{
	public enum DepositErrType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("BalanceLow")]
		BalanceLow,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("TransferFailure")]
		TransferFailure,
	}
	public class DepositErr : EdjCase.ICP.Candid.Models.CandidVariantValueBase<DepositErrType>
	{
		public DepositErr(DepositErrType type, System.Object? value)  : base(type, value)
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

