using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class ChangeFeeCollector
	{
		[VariantTagProperty()]
		public ChangeFeeCollectorTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Account? SetTo { get => this.Tag == ChangeFeeCollectorTag.SetTo ? (Account)this.Value : default; set => (this.Tag, this.Value) = (ChangeFeeCollectorTag.SetTo, value); }

		public ChangeFeeCollector(ChangeFeeCollectorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ChangeFeeCollector()
		{
		}
	}

	public enum ChangeFeeCollectorTag
	{
		Unset,
		SetTo
	}
}