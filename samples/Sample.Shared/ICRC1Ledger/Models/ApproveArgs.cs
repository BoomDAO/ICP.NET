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
		public OptionalValue<List<byte>> Memo { get; set; }

		[CandidName("from_subaccount")]
		public OptionalValue<List<byte>> FromSubaccount { get; set; }

		[CandidName("created_at_time")]
		public OptionalValue<Timestamp> CreatedAtTime { get; set; }

		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		[CandidName("expected_allowance")]
		public OptionalValue<UnboundedUInt> ExpectedAllowance { get; set; }

		[CandidName("expires_at")]
		public OptionalValue<Timestamp> ExpiresAt { get; set; }

		[CandidName("spender")]
		public Account Spender { get; set; }

		public ApproveArgs(OptionalValue<UnboundedUInt> fee, OptionalValue<List<byte>> memo, OptionalValue<List<byte>> fromSubaccount, OptionalValue<Timestamp> createdAtTime, UnboundedUInt amount, OptionalValue<UnboundedUInt> expectedAllowance, OptionalValue<Timestamp> expiresAt, Account spender)
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
	}
}