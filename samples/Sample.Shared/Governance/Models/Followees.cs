using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class Followees
	{
		[CandidName("followees")]
		public List<NeuronId> Followees_ { get; set; }

		public Followees(List<NeuronId> followees)
		{
			this.Followees_ = followees;
		}

		public Followees()
		{
		}
	}
}