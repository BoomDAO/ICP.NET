using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class NeuronInFlightCommand
	{
		[CandidName("command")]
		public OptionalValue<Command2> Command { get; set; }

		[CandidName("timestamp")]
		public ulong Timestamp { get; set; }

		public NeuronInFlightCommand(OptionalValue<Command2> command, ulong timestamp)
		{
			this.Command = command;
			this.Timestamp = timestamp;
		}

		public NeuronInFlightCommand()
		{
		}
	}
}