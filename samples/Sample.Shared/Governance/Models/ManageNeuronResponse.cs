namespace Sample.Shared.Governance.Models
{
	public class ManageNeuronResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("command")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Command1> Command { get; set; }
		
	}
}

