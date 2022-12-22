using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class SpawnResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("created_neuron_id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> CreatedNeuronId { get; set; }
		
	}
}

