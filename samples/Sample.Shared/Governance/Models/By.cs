using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(ByTag))]
	public class By
	{
		[VariantTagProperty()]
		public ByTag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public By(ByTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected By()
		{
		}

		public static By NeuronIdOrSubaccount(By.NeuronIdOrSubaccountInfo info)
		{
			return new By(ByTag.NeuronIdOrSubaccount, info);
		}

		public static By MemoAndController(ClaimOrRefreshNeuronFromAccount info)
		{
			return new By(ByTag.MemoAndController, info);
		}

		public static By Memo(ulong info)
		{
			return new By(ByTag.Memo, info);
		}

		public By.NeuronIdOrSubaccountInfo AsNeuronIdOrSubaccount()
		{
			this.ValidateTag(ByTag.NeuronIdOrSubaccount);
			return (By.NeuronIdOrSubaccountInfo)this.Value!;
		}

		public ClaimOrRefreshNeuronFromAccount AsMemoAndController()
		{
			this.ValidateTag(ByTag.MemoAndController);
			return (ClaimOrRefreshNeuronFromAccount)this.Value!;
		}

		public ulong AsMemo()
		{
			this.ValidateTag(ByTag.Memo);
			return (ulong)this.Value!;
		}

		private void ValidateTag(ByTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}

		public class NeuronIdOrSubaccountInfo
		{
			public NeuronIdOrSubaccountInfo()
			{
			}
		}
	}

	public enum ByTag
	{
		[VariantOptionType(typeof(By.NeuronIdOrSubaccountInfo))]
		NeuronIdOrSubaccount,
		[VariantOptionType(typeof(ClaimOrRefreshNeuronFromAccount))]
		MemoAndController,
		[VariantOptionType(typeof(ulong))]
		Memo
	}
}