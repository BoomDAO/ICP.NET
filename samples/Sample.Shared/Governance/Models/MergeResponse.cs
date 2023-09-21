using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class MergeResponse
	{
		[CandidName("target_neuron")]
		public OptionalValue<Neuron> TargetNeuron { get; set; }

		[CandidName("source_neuron")]
		public OptionalValue<Neuron> SourceNeuron { get; set; }

		[CandidName("target_neuron_info")]
		public OptionalValue<NeuronInfo> TargetNeuronInfo { get; set; }

		[CandidName("source_neuron_info")]
		public OptionalValue<NeuronInfo> SourceNeuronInfo { get; set; }

		public MergeResponse(OptionalValue<Neuron> targetNeuron, OptionalValue<Neuron> sourceNeuron, OptionalValue<NeuronInfo> targetNeuronInfo, OptionalValue<NeuronInfo> sourceNeuronInfo)
		{
			this.TargetNeuron = targetNeuron;
			this.SourceNeuron = sourceNeuron;
			this.TargetNeuronInfo = targetNeuronInfo;
			this.SourceNeuronInfo = sourceNeuronInfo;
		}

		public MergeResponse()
		{
		}
	}
}