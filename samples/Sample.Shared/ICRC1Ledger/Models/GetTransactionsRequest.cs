using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class GetTransactionsRequest
	{
		[CandidName("start")]
		public TxIndex Start { get; set; }

		[CandidName("length")]
		public UnboundedUInt Length { get; set; }

		public GetTransactionsRequest(TxIndex start, UnboundedUInt length)
		{
			this.Start = start;
			this.Length = length;
		}

		public GetTransactionsRequest()
		{
		}
	}
}