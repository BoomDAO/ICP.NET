using Timestamp = System.UInt64;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using System;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	/// <summary>
	/// This class represents the arguments for transferring an ICRC1 token
	/// </summary>
	public class TransferArgs
	{
		/// <summary>
		/// The subaccount from which the transfer is made, represented as an OptionalValue object
		/// </summary>
		[CandidName("from_subaccount")]
		public OptionalValue<byte[]> FromSubaccount { get; set; }

		/// <summary>
		/// The account to which the transfer is made, represented as an Account object
		/// </summary>
		[CandidName("to")]
		public Account To { get; set; }

		/// <summary>
		/// The amount of the token being transferred, represented as an UnboundedUInt object
		/// </summary>
		[CandidName("amount")]
		public UnboundedUInt Amount { get; set; }

		/// <summary>
		/// The fee for the transfer, represented as an OptionalValue object
		/// </summary>
		[CandidName("fee")]
		public OptionalValue<UnboundedUInt> Fee { get; set; }

		/// <summary>
		/// The memo for the transfer, represented as an OptionalValue object
		/// </summary>
		[CandidName("memo")]
		public OptionalValue<byte[]> Memo { get; set; }

		/// <summary>
		/// The time at which the transfer is created, represented as an OptionalValue object
		/// </summary>
		[CandidName("created_at_time")]
		public OptionalValue<Timestamp> CreatedAtTime { get; set; }

		/// <summary>
		/// Primary constructor for the TransferArgs class
		/// </summary>
		/// <param name="fromSubaccount">The subaccount from which the transfer is made, represented as an OptionalValue object</param>
		/// <param name="to">The account to which the transfer is made, represented as an Account object</param>
		/// <param name="amount">The amount of the token being transferred, represented as an UnboundedUInt object</param>
		/// <param name="fee">The fee for the transfer, represented as an OptionalValue object</param>
		/// <param name="memo">The memo for the transfer, represented as an OptionalValue object</param>
		/// <param name="createdAtTime">The time at which the transfer is created, represented as an OptionalValue object</param>
		public TransferArgs(OptionalValue<byte[]> fromSubaccount, Account to, UnboundedUInt amount, OptionalValue<UnboundedUInt> fee, OptionalValue<byte[]> memo, OptionalValue<Timestamp> createdAtTime)
		{
			this.FromSubaccount = fromSubaccount ?? throw new ArgumentNullException(nameof(fromSubaccount));
			this.To = to ?? throw new ArgumentNullException(nameof(to));
			this.Amount = amount ?? throw new ArgumentNullException(nameof(amount));
			this.Fee = fee;
			this.Memo = memo;
			this.CreatedAtTime = createdAtTime;
		}
	}
}