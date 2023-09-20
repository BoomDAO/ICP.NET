using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<Sample.Shared.ICRC1Ledger.Models.MapItem>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Sample.Shared.ICRC1Ledger.Models;

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
		public List<GetTransactionsResponse.ArchivedTransactionsItem> ArchivedTransactions { get; set; }

		public GetTransactionsResponse(UnboundedUInt logLength, List<Transaction> transactions, TxIndex firstIndex, List<GetTransactionsResponse.ArchivedTransactionsItem> archivedTransactions)
		{
			this.LogLength = logLength;
			this.Transactions = transactions;
			this.FirstIndex = firstIndex;
			this.ArchivedTransactions = archivedTransactions;
		}

		public GetTransactionsResponse()
		{
		}

		public class ArchivedTransactionsItem
		{
			[CandidName("start")]
			public TxIndex Start { get; set; }

			[CandidName("length")]
			public UnboundedUInt Length { get; set; }

			[CandidName("callback")]
			public QueryArchiveFn Callback { get; set; }

			public ArchivedTransactionsItem(TxIndex start, UnboundedUInt length, QueryArchiveFn callback)
			{
				this.Start = start;
				this.Length = length;
				this.Callback = callback;
			}

			public ArchivedTransactionsItem()
			{
			}
		}
	}
}