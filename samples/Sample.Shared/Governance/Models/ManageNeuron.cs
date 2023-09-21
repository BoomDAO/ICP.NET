using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class ManageNeuron
	{
		[CandidName("id")]
		public OptionalValue<NeuronId> Id { get; set; }

		[CandidName("command")]
		public OptionalValue<Command> Command { get; set; }

		[CandidName("neuron_id_or_subaccount")]
		public OptionalValue<NeuronIdOrSubaccount> NeuronIdOrSubaccount { get; set; }

		public ManageNeuron(OptionalValue<NeuronId> id, OptionalValue<Command> command, OptionalValue<NeuronIdOrSubaccount> neuronIdOrSubaccount)
		{
			this.Id = id;
			this.Command = command;
			this.NeuronIdOrSubaccount = neuronIdOrSubaccount;
		}

		public ManageNeuron()
		{
		}
	}
}