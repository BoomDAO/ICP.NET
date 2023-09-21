using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class ManageNeuronResponse
	{
		[CandidName("command")]
		public OptionalValue<Command1> Command { get; set; }

		public ManageNeuronResponse(OptionalValue<Command1> command)
		{
			this.Command = command;
		}

		public ManageNeuronResponse()
		{
		}
	}
}