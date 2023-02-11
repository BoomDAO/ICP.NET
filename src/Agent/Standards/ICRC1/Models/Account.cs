using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Subaccount = System.Collections.Generic.List<byte>;
using EdjCase.ICP.Agent.Agents;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	public class Account
	{
		[Candid.Mapping.CandidName("owner")]
		public Candid.Models.Principal Owner { get; set; }

		[Candid.Mapping.CandidName("subaccount")]
		public Candid.Models.OptionalValue<Subaccount> Subaccount { get; set; }

		public Account(Candid.Models.Principal owner, Candid.Models.OptionalValue<Subaccount> subaccount)
		{
			this.Owner = owner;
			this.Subaccount = subaccount;
		}
	}
}