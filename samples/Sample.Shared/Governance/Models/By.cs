using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public enum ByType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronIdOrSubaccount")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(By))]
		NeuronIdOrSubaccount,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MemoAndController")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ClaimOrRefreshNeuronFromAccount))]
		MemoAndController,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Memo")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ulong))]
		Memo,
	}
	public class By : EdjCase.ICP.Candid.Models.CandidVariantValueBase<ByType>
	{
		public By(ByType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected By()
		{
		}
		
		public static By NeuronIdOrSubaccount(By info)
		{
			return new By(ByType.NeuronIdOrSubaccount, info);
		}
		
		public By AsNeuronIdOrSubaccount()
		{
			this.ValidateType(ByType.NeuronIdOrSubaccount);
			return (By)this.value!;
		}
		
		public static By MemoAndController(ClaimOrRefreshNeuronFromAccount info)
		{
			return new By(ByType.MemoAndController, info);
		}
		
		public ClaimOrRefreshNeuronFromAccount AsMemoAndController()
		{
			this.ValidateType(ByType.MemoAndController);
			return (ClaimOrRefreshNeuronFromAccount)this.value!;
		}
		
		public static By Memo(ulong info)
		{
			return new By(ByType.Memo, info);
		}
		
		public ulong AsMemo()
		{
			this.ValidateType(ByType.Memo);
			return (ulong)this.value!;
		}
		
	}
}

