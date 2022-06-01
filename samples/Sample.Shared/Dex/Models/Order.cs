namespace Sample.Shared.Dex.Models
{
	public class Order
	{
		public Token From { get; set; }
		
		public EdjCase.ICP.Candid.UnboundedUInt FromAmount { get; set; }
		
		public OrderId Id { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal Owner { get; set; }
		
		public Token To { get; set; }
		
		public EdjCase.ICP.Candid.UnboundedUInt ToAmount { get; set; }
		
	}
}
