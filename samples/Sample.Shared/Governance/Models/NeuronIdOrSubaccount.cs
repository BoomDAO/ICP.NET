using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class NeuronIdOrSubaccount
	{
		[VariantTagProperty()]
		public NeuronIdOrSubaccountTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public List<byte>? Subaccount { get => this.Tag == NeuronIdOrSubaccountTag.Subaccount ? (List<byte>)this.Value : default; set => (this.Tag, this.Value) = (NeuronIdOrSubaccountTag.Subaccount, value); }

		public NeuronId? NeuronId { get => this.Tag == NeuronIdOrSubaccountTag.NeuronId ? (NeuronId)this.Value : default; set => (this.Tag, this.Value) = (NeuronIdOrSubaccountTag.NeuronId, value); }

		public NeuronIdOrSubaccount(NeuronIdOrSubaccountTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected NeuronIdOrSubaccount()
		{
		}
	}

	public enum NeuronIdOrSubaccountTag
	{
		Subaccount,
		NeuronId
	}
}