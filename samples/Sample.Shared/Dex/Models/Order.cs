namespace Sample.Shared.Dex.Models
{
	public class Order
	{
		public Token from { get; set; }
		
		public EdjCase.ICP.Candid.UnboundedUInt fromAmount { get; set; }
		
		public OrderId id { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal owner { get; set; }
		
		public Token to { get; set; }
		
		public EdjCase.ICP.Candid.UnboundedUInt toAmount { get; set; }
		
	}
}
