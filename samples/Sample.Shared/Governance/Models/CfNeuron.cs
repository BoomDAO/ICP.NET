using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class CfNeuron
	{
		[CandidName("nns_neuron_id")]
		public ulong NnsNeuronId { get; set; }

		[CandidName("amount_icp_e8s")]
		public ulong AmountIcpE8s { get; set; }

		public CfNeuron(ulong nnsNeuronId, ulong amountIcpE8s)
		{
			this.NnsNeuronId = nnsNeuronId;
			this.AmountIcpE8s = amountIcpE8s;
		}

		public CfNeuron()
		{
		}
	}
}