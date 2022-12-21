using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum NeuronIdOrSubaccountType
	{
		Subaccount,
		NeuronId,
	}
	public class NeuronIdOrSubaccount : EdjCase.ICP.Candid.CandidVariantValueBase<NeuronIdOrSubaccountType>
	{
		public NeuronIdOrSubaccount(NeuronIdOrSubaccountType type, object? value)  : base(type, value)
		{
		}
		
		protected NeuronIdOrSubaccount()
		{
		}
		
		public static NeuronIdOrSubaccount Subaccount(List<byte> info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountType.Subaccount, info);
		}
		
		public List<byte> AsSubaccount()
		{
			this.ValidateType(NeuronIdOrSubaccountType.Subaccount);
			return (List<byte>)this.value!;
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
}

