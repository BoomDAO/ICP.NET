using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class Follow
	{
		[CandidName("topic")]
		public int Topic { get; set; }

		[CandidName("followees")]
		public List<NeuronId> Followees { get; set; }

		public Follow(int topic, List<NeuronId> followees)
		{
			this.Topic = topic;
			this.Followees = followees;
		}

		public Follow()
		{
		}
	}
}