using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	public class CancelOrderErr : EdjCase.ICP.Candid.Models.CandidVariantValueBase<CancelOrderErrType>
	{
		public CancelOrderErr(CancelOrderErrType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected CancelOrderErr()
		{
		}
		
		public static CancelOrderErr NotAllowed()
		{
			return new CancelOrderErr(CancelOrderErrType.NotAllowed, null);
		}
		
		public static CancelOrderErr NotExistingOrder()
		{
			return new CancelOrderErr(CancelOrderErrType.NotExistingOrder, null);
		}
		
	}
	public enum CancelOrderErrType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NotAllowed")]
		NotAllowed,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NotExistingOrder")]
		NotExistingOrder,
	}
}

