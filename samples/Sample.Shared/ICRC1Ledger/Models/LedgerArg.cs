using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using System;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant]
	public class LedgerArg
	{
		[VariantTagProperty]
		public LedgerArgTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public LedgerArg(LedgerArgTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected LedgerArg()
		{
		}

		public static LedgerArg Init(InitArgs info)
		{
			return new LedgerArg(LedgerArgTag.Init, info);
		}

		public static LedgerArg Upgrade(OptionalValue<UpgradeArgs> info)
		{
			return new LedgerArg(LedgerArgTag.Upgrade, info);
		}

		public InitArgs AsInit()
		{
			this.ValidateTag(LedgerArgTag.Init);
			return (InitArgs)this.Value!;
		}

		public OptionalValue<UpgradeArgs> AsUpgrade()
		{
			this.ValidateTag(LedgerArgTag.Upgrade);
			return (OptionalValue<UpgradeArgs>)this.Value!;
		}

		private void ValidateTag(LedgerArgTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum LedgerArgTag
	{
		Init,
		Upgrade
	}
}