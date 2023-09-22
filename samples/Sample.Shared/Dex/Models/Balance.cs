using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Token = EdjCase.ICP.Candid.Models.Principal;

namespace Sample.Shared.Dex.Models
{
	public class Balance
	{
		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		[CandidName("owner")]
		public Principal Owner { get; set; }

		[CandidName("token")]
		public Token Token { get; set; }

		public Balance(UnboundedUInt amount, Principal owner, Token token)
		{
			this.Amount = amount;
			this.Owner = owner;
			this.Token = token;
		}

		public Balance()
		{
		}
	}
}