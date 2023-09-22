using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	public class Order
	{
		[CandidName("from")]
		public Token From { get; set; }

		[CandidName("fromAmount")]
		public UnboundedUInt FromAmount { get; set; }

		[CandidName("id")]
		public OrderId Id { get; set; }

		[CandidName("owner")]
		public Principal Owner { get; set; }

		[CandidName("to")]
		public Token To { get; set; }

		[CandidName("toAmount")]
		public UnboundedUInt ToAmount { get; set; }

		public Order(Token from, UnboundedUInt fromAmount, OrderId id, Principal owner, Token to, UnboundedUInt toAmount)
		{
			this.From = from;
			this.FromAmount = fromAmount;
			this.Id = id;
			this.Owner = owner;
			this.To = to;
			this.ToAmount = toAmount;
		}

		public Order()
		{
		}
	}
}