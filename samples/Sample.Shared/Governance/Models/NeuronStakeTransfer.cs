using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class NeuronStakeTransfer
	{
		[CandidName("to_subaccount")]
		public List<byte> ToSubaccount { get; set; }

		[CandidName("neuron_stake_e8s")]
		public ulong NeuronStakeE8s { get; set; }

		[CandidName("from")]
		public OptionalValue<Principal> From { get; set; }

		[CandidName("memo")]
		public ulong Memo { get; set; }

		[CandidName("from_subaccount")]
		public List<byte> FromSubaccount { get; set; }

		[CandidName("transfer_timestamp")]
		public ulong TransferTimestamp { get; set; }

		[CandidName("block_height")]
		public ulong BlockHeight { get; set; }

		public NeuronStakeTransfer(List<byte> toSubaccount, ulong neuronStakeE8s, OptionalValue<Principal> from, ulong memo, List<byte> fromSubaccount, ulong transferTimestamp, ulong blockHeight)
		{
			this.ToSubaccount = toSubaccount;
			this.NeuronStakeE8s = neuronStakeE8s;
			this.From = from;
			this.Memo = memo;
			this.FromSubaccount = fromSubaccount;
			this.TransferTimestamp = transferTimestamp;
			this.BlockHeight = blockHeight;
		}

		public NeuronStakeTransfer()
		{
		}
	}
}