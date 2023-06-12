using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class Countries
	{
		[CandidName("iso_codes")]
		public List<string> IsoCodes { get; set; }

		public Countries(List<string> isoCodes)
		{
			this.IsoCodes = isoCodes;
		}

		public Countries()
		{
		}
	}
}