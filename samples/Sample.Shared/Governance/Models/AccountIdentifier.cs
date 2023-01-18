namespace Sample.Shared.Governance.Models
{
	public class AccountIdentifier
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("hash")]
		public System.Collections.Generic.List<byte> Hash { get; set; }
		
	}
}

