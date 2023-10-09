using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class NeuronIdOrSubaccount
	{
		[VariantTagProperty]
		public NeuronIdOrSubaccountTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public NeuronIdOrSubaccount(NeuronIdOrSubaccountTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected NeuronIdOrSubaccount()
		{
		}

		public static NeuronIdOrSubaccount Subaccount(List<byte> info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountTag.Subaccount, info);
		}

		public static NeuronIdOrSubaccount NeuronId(NeuronId info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountTag.NeuronId, info);
		}

		public List<byte> AsSubaccount()
		{
			this.ValidateTag(NeuronIdOrSubaccountTag.Subaccount);
			return (List<byte>)this.Value!;
		}

		public NeuronId AsNeuronId()
		{
			this.ValidateTag(NeuronIdOrSubaccountTag.NeuronId);
			return (NeuronId)this.Value!;
		}

		private void ValidateTag(NeuronIdOrSubaccountTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum NeuronIdOrSubaccountTag
	{
		Subaccount,
		NeuronId
	}
}