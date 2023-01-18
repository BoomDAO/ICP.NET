namespace Sample.Shared.Governance.Models
{
	public class Followees
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("followees")]
		public System.Collections.Generic.List<NeuronId> Followees_ { get; set; }
		
	}
}

