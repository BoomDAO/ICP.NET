using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class NeuronInFlightCommand
	{
		[CandidName("command")]
		public OptionalValue<Command_2> Command { get; set; }

		[CandidName("timestamp")]
		public ulong Timestamp { get; set; }

		public NeuronInFlightCommand(OptionalValue<Command_2> command, ulong timestamp)
		{
			this.Command = command;
			this.Timestamp = timestamp;
		}

		public NeuronInFlightCommand()
		{
		}
	}
}