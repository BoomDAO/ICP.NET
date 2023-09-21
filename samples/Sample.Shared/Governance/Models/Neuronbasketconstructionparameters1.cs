using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class Neuronbasketconstructionparameters1
	{
		[CandidName("dissolve_delay_interval_seconds")]
		public ulong DissolveDelayIntervalSeconds { get; set; }

		[CandidName("count")]
		public ulong Count { get; set; }

		public Neuronbasketconstructionparameters1(ulong dissolveDelayIntervalSeconds, ulong count)
		{
			this.DissolveDelayIntervalSeconds = dissolveDelayIntervalSeconds;
			this.Count = count;
		}

		public Neuronbasketconstructionparameters1()
		{
		}
	}
}