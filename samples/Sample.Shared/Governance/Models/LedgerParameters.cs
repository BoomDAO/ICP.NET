using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class LedgerParameters
	{
		[CandidName("transaction_fee")]
		public OptionalValue<Tokens> TransactionFee { get; set; }

		[CandidName("token_symbol")]
		public OptionalValue<string> TokenSymbol { get; set; }

		[CandidName("token_logo")]
		public OptionalValue<Image> TokenLogo { get; set; }

		[CandidName("token_name")]
		public OptionalValue<string> TokenName { get; set; }

		public LedgerParameters(OptionalValue<Tokens> transactionFee, OptionalValue<string> tokenSymbol, OptionalValue<Image> tokenLogo, OptionalValue<string> tokenName)
		{
			this.TransactionFee = transactionFee;
			this.TokenSymbol = tokenSymbol;
			this.TokenLogo = tokenLogo;
			this.TokenName = tokenName;
		}

		public LedgerParameters()
		{
		}
	}
}