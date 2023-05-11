using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class Tokens
	{
		[CandidName("e8s")]
		public OptionalValue<ulong> E8s { get; set; }

		public Tokens(OptionalValue<ulong> e8s)
		{
			this.E8s = e8s;
		}

		public Tokens()
		{
		}
	}
}