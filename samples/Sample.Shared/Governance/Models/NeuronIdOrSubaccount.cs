using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(NeuronIdOrSubaccountTag))]
	public class NeuronIdOrSubaccount
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public NeuronIdOrSubaccountTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private NeuronIdOrSubaccount(NeuronIdOrSubaccountTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected NeuronIdOrSubaccount()
		{
		}
		
		public static NeuronIdOrSubaccount Subaccount(System.Collections.Generic.List<byte> info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountTag.Subaccount, info);
		}
		
		public System.Collections.Generic.List<byte> AsSubaccount()
		{
			this.ValidateTag(NeuronIdOrSubaccountTag.Subaccount);
			return (System.Collections.Generic.List<byte>)this.Value!;
		}
		
		public static NeuronIdOrSubaccount NeuronId(NeuronId info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountTag.NeuronId, info);
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
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Subaccount")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(System.Collections.Generic.List<byte>))]
		Subaccount,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronId")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(NeuronId))]
		NeuronId,
	}
}

