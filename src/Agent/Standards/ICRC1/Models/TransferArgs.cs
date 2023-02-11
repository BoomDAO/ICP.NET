using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Subaccount = System.Collections.Generic.List<byte>;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	public class TransferArgs
	{
		[CandidName("from_subaccount")]
		public OptionalValue<Subaccount> FromSubaccount { get; set; }

		[CandidName("to")]
		public Account To { get; set; }

		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		[CandidName("fee")]
		public OptionalValue<UnboundedUInt> Fee { get; set; }

		[CandidName("memo")]
		public OptionalValue<Subaccount> Memo { get; set; }

		[CandidName("created_at_time")]
		public OptionalValue<Timestamp> CreatedAtTime { get; set; }

		public TransferArgs(OptionalValue<Subaccount> fromSubaccount, Account to, UnboundedUInt amount, OptionalValue<UnboundedUInt> fee, OptionalValue<Subaccount> memo, OptionalValue<Timestamp> createdAtTime)
		{
			this.FromSubaccount = fromSubaccount;
			this.To = to;
			this.Amount = amount;
			this.Fee = fee;
			this.Memo = memo;
			this.CreatedAtTime = createdAtTime;
		}
	}
}