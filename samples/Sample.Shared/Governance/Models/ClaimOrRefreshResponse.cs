using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("refreshed_neuron_id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> RefreshedNeuronId { get; set; }
		
	}
}

