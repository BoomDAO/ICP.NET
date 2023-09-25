using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class LedgerArg
	{
		[VariantTagProperty()]
		public LedgerArgTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public InitArgs? Init { get => this.Tag == LedgerArgTag.Init ? (InitArgs)this.Value : default; set => (this.Tag, this.Value) = (LedgerArgTag.Init, value); }

		public OptionalValue<UpgradeArgs>? Upgrade { get => this.Tag == LedgerArgTag.Upgrade ? (OptionalValue<UpgradeArgs>)this.Value : default; set => (this.Tag, this.Value) = (LedgerArgTag.Upgrade, value); }

		public LedgerArg(LedgerArgTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected LedgerArg()
		{
		}
	}

	public enum LedgerArgTag
	{
		Init,
		Upgrade
	}
}