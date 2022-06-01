namespace Sample.Shared.Governance.Models
{
	public class NeuronInFlightCommand
	{
		public Command2? Command { get; set; }
		
		public ulong Timestamp { get; set; }
		
	}
}
