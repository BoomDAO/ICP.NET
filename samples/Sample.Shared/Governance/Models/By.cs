using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(ByTag))]
	public class By
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public ByTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private By(ByTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected By()
		{
		}
		
		public class O0
		{
		}
		public static By NeuronIdOrSubaccount(By.O0 info)
		{
			return new By(ByTag.NeuronIdOrSubaccount, info);
		}
		
		public By.O0 AsNeuronIdOrSubaccount()
		{
			this.ValidateType(ByTag.NeuronIdOrSubaccount);
			return (By.O0)this.Value!;
		}
		
		public static By MemoAndController(ClaimOrRefreshNeuronFromAccount info)
		{
			return new By(ByTag.MemoAndController, info);
		}
		
		public ClaimOrRefreshNeuronFromAccount AsMemoAndController()
		{
			this.ValidateType(ByTag.MemoAndController);
			return (ClaimOrRefreshNeuronFromAccount)this.Value!;
		}
		
		public static By Memo(ulong info)
		{
			return new By(ByTag.Memo, info);
		}
		
		public ulong AsMemo()
		{
			this.ValidateType(ByTag.Memo);
			return (ulong)this.Value!;
		}
		
		private void ValidateType(ByTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum ByTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronIdOrSubaccount")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(By.O0))]
		NeuronIdOrSubaccount,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("MemoAndController")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ClaimOrRefreshNeuronFromAccount))]
		MemoAndController,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Memo")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ulong))]
		Memo,
	}
}

