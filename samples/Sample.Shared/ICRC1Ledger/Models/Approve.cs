using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.ICRC1Ledger.Models;
using System.Collections.Generic;
using Timestamp = System.UInt64;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class Approve
	{
		[CandidName("fee")]
		public OptionalValue<UnboundedUInt> Fee { get; set; }

		[CandidName("from")]
		public Account From { get; set; }

		[CandidName("memo")]
		public OptionalValue<byte[]> Memo { get; set; }

		[CandidName("created_at_time")]
		public Approve.CreatedAtTimeInfo CreatedAtTime { get; set; }

		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		[CandidName("expected_allowance")]
		public OptionalValue<UnboundedUInt> ExpectedAllowance { get; set; }

		[CandidName("expires_at")]
		public Approve.ExpiresAtInfo ExpiresAt { get; set; }

		[CandidName("spender")]
		public Account Spender { get; set; }

		public Approve(OptionalValue<UnboundedUInt> fee, Account from, OptionalValue<byte[]> memo, Approve.CreatedAtTimeInfo createdAtTime, UnboundedUInt amount, OptionalValue<UnboundedUInt> expectedAllowance, Approve.ExpiresAtInfo expiresAt, Account spender)
		{
			this.Fee = fee;
			this.From = from;
			this.Memo = memo;
			this.CreatedAtTime = createdAtTime;
			this.Amount = amount;
			this.ExpectedAllowance = expectedAllowance;
			this.ExpiresAt = expiresAt;
			this.Spender = spender;
		}

		public Approve()
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

		public class ExpiresAtInfo : OptionalValue<Timestamp>
		{
			public ExpiresAtInfo()
			{
			}

			public ExpiresAtInfo(Timestamp value) : base(value)
			{
			}
		}
	}
}