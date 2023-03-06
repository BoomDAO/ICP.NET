using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class AccountIdentifier
	{
		[CandidName("hash")]
		public List<byte> Hash { get; set; }

		public AccountIdentifier(List<byte> hash)
		{
			this.Hash = hash;
		}

		public AccountIdentifier()
		{
		}
	}
}