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

		public static By NeuronIdOrSubaccount(By.NeuronIdOrSubaccountRecord info)
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

		public By.NeuronIdOrSubaccountRecord AsNeuronIdOrSubaccount()
		{
			this.ValidateTag(ByTag.NeuronIdOrSubaccount);
			return (By.NeuronIdOrSubaccountRecord)this.Value!;
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

		public class NeuronIdOrSubaccountRecord
		{
			public NeuronIdOrSubaccountRecord()
			{
			}
		}
	}

	public enum ByTag
	{
		[VariantOptionType(typeof(By.NeuronIdOrSubaccountRecord))]
		NeuronIdOrSubaccount,
		[VariantOptionType(typeof(ClaimOrRefreshNeuronFromAccount))]
		MemoAndController,
		[VariantOptionType(typeof(ulong))]
		Memo
	}
}