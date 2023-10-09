using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Sample.Shared.ICRC1Ledger.Models;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class GetTransactionsResponse
	{
		[CandidName("log_length")]
		public UnboundedUInt LogLength { get; set; }

		[CandidName("transactions")]
		public List<Transaction> Transactions { get; set; }

		[CandidName("first_index")]
		public TxIndex FirstIndex { get; set; }

		[CandidName("archived_transactions")]
		public GetTransactionsResponse.ArchivedTransactionsInfo ArchivedTransactions { get; set; }

		public GetTransactionsResponse(UnboundedUInt logLength, List<Transaction> transactions, TxIndex firstIndex, GetTransactionsResponse.ArchivedTransactionsInfo archivedTransactions)
		{
			this.LogLength = logLength;
			this.Transactions = transactions;
			this.FirstIndex = firstIndex;
			this.ArchivedTransactions = archivedTransactions;
		}

		public GetTransactionsResponse()
		{
		}

		public class ArchivedTransactionsInfo : List<GetTransactionsResponse.ArchivedTransactionsInfo.ArchivedTransactionsInfoElement>
		{
			public ArchivedTransactionsInfo()
			{
			}

			public class ArchivedTransactionsInfoElement
			{
				[CandidName("start")]
				public TxIndex Start { get; set; }

				[CandidName("length")]
				public UnboundedUInt Length { get; set; }

				[CandidName("callback")]
				public QueryArchiveFn Callback { get; set; }

				public ArchivedTransactionsInfoElement(TxIndex start, UnboundedUInt length, QueryArchiveFn callback)
				{
					this.Start = start;
					this.Length = length;
					this.Callback = callback;
				}

				public ArchivedTransactionsInfoElement()
				{
				}
			}
		}
	}
}