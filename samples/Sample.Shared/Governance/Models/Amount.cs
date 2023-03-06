using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class Amount
	{
		[CandidName("e8s")]
		public ulong E8s { get; set; }

		public Amount(ulong e8s)
		{
			this.E8s = e8s;
		}

		public Amount()
		{
		}
	}
}