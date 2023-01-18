namespace Sample.Shared.Governance.Models
{
	public class NeuronId
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public ulong Id { get; set; }
		
	}
}

