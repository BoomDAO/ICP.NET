using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Dex.Models
{
	public class Order
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("from")]
		public Token From { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("fromAmount")]
		public UnboundedUInt FromAmount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("id")]
		public OrderId Id { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("owner")]
		public EdjCase.ICP.Candid.Models.Principal Owner { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("to")]
		public Token To { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("toAmount")]
		public UnboundedUInt ToAmount { get; set; }
		
	}
}

