namespace Sample.Shared.Governance.Models
{
	public class Disburse
	{
		public AccountIdentifier? to_account { get; set; }
		
		public Amount? amount { get; set; }
		
	}
}
