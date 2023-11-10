using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	/// <summary>
	/// This class represents an ICRC1 account
	/// </summary>
	public class Account
	{
		/// <summary>
		/// The owner of the account, represented as a Principal object
		/// </summary>
		[CandidName("owner")]
		public Principal Owner { get; set; }

		/// <summary>
		/// The subaccount of the account, represented as an OptionalValue object
		/// </summary>
		[CandidName("subaccount")]
		public OptionalValue<byte[]> Subaccount { get; set; }

		/// <summary>
		/// Primary constructor for the Account class
		/// </summary>
		/// <param name="owner">The owner of the account, represented as a Principal object</param>
		/// <param name="subaccount">The subaccount of the account, represented as an OptionalValue object</param>
		public Account(Principal owner, OptionalValue<byte[]> subaccount)
		{
			this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
			this.Subaccount = subaccount ?? throw new ArgumentNullException(nameof(subaccount));
		}
	}
}