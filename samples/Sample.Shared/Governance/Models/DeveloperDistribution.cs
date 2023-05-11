using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class DeveloperDistribution
	{
		[CandidName("developer_neurons")]
		public List<NeuronDistribution> DeveloperNeurons { get; set; }

		public DeveloperDistribution(List<NeuronDistribution> developerNeurons)
		{
			this.DeveloperNeurons = developerNeurons;
		}

		public DeveloperDistribution()
		{
		}
	}
}