using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class By : EdjCase.ICP.Candid.Models.CandidVariantValueBase<ByType>
	{
		public By(ByType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected By()
		{
		}
		
		public class O0
		{
		}
		public static By NeuronIdOrSubaccount(By.O0 info)
		{
			return new By(ByType.NeuronIdOrSubaccount, info);
		}
		
		public By.O0 AsNeuronIdOrSubaccount()
		{
			this.ValidateType(ByType.NeuronIdOrSubaccount);
			return (By.O0)this.value!;
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
	public enum ByType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronIdOrSubaccount")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(By.O0))]
		NeuronIdOrSubaccount,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MemoAndController")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ClaimOrRefreshNeuronFromAccount))]
		MemoAndController,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Memo")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ulong))]
		Memo,
	}
}

