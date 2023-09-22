using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.ICRC1Ledger.Models;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class TransactionRange
	{
		[CandidName("transactions")]
		public List<Transaction> Transactions { get; set; }

		public TransactionRange(List<Transaction> transactions)
		{
			this.Transactions = transactions;
		}

		public TransactionRange()
		{
		}
	}
}