using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class NeuronIdOrSubaccount : EdjCase.ICP.Candid.Models.CandidVariantValueBase<NeuronIdOrSubaccountType>
	{
		public NeuronIdOrSubaccount(NeuronIdOrSubaccountType type, System.Object? value)  : base(type, value)
		{
		}
		
		protected NeuronIdOrSubaccount()
		{
		}
		
		public static NeuronIdOrSubaccount Subaccount(System.Collections.Generic.List<byte> info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountType.Subaccount, info);
		}
		
		public System.Collections.Generic.List<byte> AsSubaccount()
		{
			this.ValidateType(NeuronIdOrSubaccountType.Subaccount);
			return (System.Collections.Generic.List<byte>)this.value!;
		}
		
		public static NeuronIdOrSubaccount NeuronId(NeuronId info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountType.NeuronId, info);
		}
		
		public NeuronId AsNeuronId()
		{
			this.ValidateType(NeuronIdOrSubaccountType.NeuronId);
			return (NeuronId)this.value!;
		}
		
	}
	public enum NeuronIdOrSubaccountType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Subaccount")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(System.Collections.Generic.List<byte>))]
		Subaccount,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NeuronId")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(NeuronId))]
		NeuronId,
	}
}

