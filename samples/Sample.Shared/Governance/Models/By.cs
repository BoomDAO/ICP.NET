using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class By
	{
		[VariantTagProperty()]
		public ByTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public By.NeuronIdOrSubaccountInfo? NeuronIdOrSubaccount { get => this.Tag == ByTag.NeuronIdOrSubaccount ? (By.NeuronIdOrSubaccountInfo)this.Value : default; set => (this.Tag, this.Value) = (ByTag.NeuronIdOrSubaccount, value); }

		public ClaimOrRefreshNeuronFromAccount? MemoAndController { get => this.Tag == ByTag.MemoAndController ? (ClaimOrRefreshNeuronFromAccount)this.Value : default; set => (this.Tag, this.Value) = (ByTag.MemoAndController, value); }

		public ulong? Memo { get => this.Tag == ByTag.Memo ? (ulong)this.Value : default; set => (this.Tag, this.Value) = (ByTag.Memo, value); }

		public By(ByTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected By()
		{
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
		NeuronIdOrSubaccount,
		MemoAndController,
		Memo
	}
}