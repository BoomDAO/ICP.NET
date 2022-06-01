namespace Sample.Shared.Dex.Models
{
	public class Balance
	{
		public EdjCase.ICP.Candid.UnboundedUInt Amount { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal Owner { get; set; }
		
		public Token Token { get; set; }
		
	}
}
