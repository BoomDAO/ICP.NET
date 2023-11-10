using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Sample.Shared.ICRC1Ledger.Models;
using Timestamp = System.UInt64;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class ApproveArgs
	{
		[CandidName("fee")]
		public OptionalValue<UnboundedUInt> Fee { get; set; }

		[CandidName("memo")]
		public OptionalValue<byte[]> Memo { get; set; }

		[CandidName("from_subaccount")]
		public OptionalValue<byte[]> FromSubaccount { get; set; }

		[CandidName("created_at_time")]
		public ApproveArgs.CreatedAtTimeInfo CreatedAtTime { get; set; }

		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		[CandidName("expected_allowance")]
		public OptionalValue<UnboundedUInt> ExpectedAllowance { get; set; }

		[CandidName("expires_at")]
		public ApproveArgs.ExpiresAtInfo ExpiresAt { get; set; }

		[CandidName("spender")]
		public Account Spender { get; set; }

		public ApproveArgs(OptionalValue<UnboundedUInt> fee, OptionalValue<byte[]> memo, OptionalValue<byte[]> fromSubaccount, ApproveArgs.CreatedAtTimeInfo createdAtTime, UnboundedUInt amount, OptionalValue<UnboundedUInt> expectedAllowance, ApproveArgs.ExpiresAtInfo expiresAt, Account spender)
		{
			this.Fee = fee;
			this.Memo = memo;
			this.FromSubaccount = fromSubaccount;
			this.CreatedAtTime = createdAtTime;
			this.Amount = amount;
			this.ExpectedAllowance = expectedAllowance;
			this.ExpiresAt = expiresAt;
			this.Spender = spender;
		}

		public ApproveArgs()
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