using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Timestamp = System.UInt64;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class Mint
	{
		[CandidName("to")]
		public Account To { get; set; }

		[CandidName("memo")]
		public OptionalValue<List<byte>> Memo { get; set; }

		[CandidName("created_at_time")]
		public OptionalValue<Timestamp> CreatedAtTime { get; set; }

		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		public Mint(Account to, OptionalValue<List<byte>> memo, OptionalValue<Timestamp> createdAtTime, UnboundedUInt amount)
		{
			this.To = to;
			this.Memo = memo;
			this.CreatedAtTime = createdAtTime;
			this.Amount = amount;
		}

		public Mint()
		{
		}
	}
}