using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class Split
	{
		[CandidName("amount_e8s")]
		public ulong AmountE8s { get; set; }

		public Split(ulong amountE8s)
		{
			this.AmountE8s = amountE8s;
		}

		public Split()
		{
		}
	}
}