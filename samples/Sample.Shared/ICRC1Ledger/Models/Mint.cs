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
		public OptionalValue<byte[]> Memo { get; set; }

		[CandidName("created_at_time")]
		public Mint.CreatedAtTimeInfo CreatedAtTime { get; set; }

		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		public Mint(Account to, OptionalValue<byte[]> memo, Mint.CreatedAtTimeInfo createdAtTime, UnboundedUInt amount)
		{
			this.To = to;
			this.Memo = memo;
			this.CreatedAtTime = createdAtTime;
			this.Amount = amount;
		}

		public Mint()
		{
		}

		public class CreatedAtTimeInfo : OptionalValue<Timestamp>
		{
			public CreatedAtTimeInfo()
			{
			}

			public CreatedAtTimeInfo(Timestamp value) : base(value)
			{
			}
		}
	}
}