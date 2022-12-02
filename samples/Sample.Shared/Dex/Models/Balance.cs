namespace Sample.Shared.Dex.Models
{
	public class Balance
	{
		public EdjCase.ICP.Candid.UnboundedUInt amount { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal owner { get; set; }
		
		public Token token { get; set; }
		
	}
}
