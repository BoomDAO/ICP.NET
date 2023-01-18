namespace Sample.Shared.Governance.Models
{
	public class NeuronInFlightCommand
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("command")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Command2> Command { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("timestamp")]
		public ulong Timestamp { get; set; }
		
	}
}

