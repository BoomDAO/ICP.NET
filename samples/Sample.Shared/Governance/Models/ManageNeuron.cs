namespace Sample.Shared.Governance.Models
{
	public class ManageNeuron
	{
		public NeuronId? Id { get; set; }
		
		public Command? Command { get; set; }
		
		public NeuronIdOrSubaccount? NeuronIdOrSubaccount { get; set; }
		
	}
}
