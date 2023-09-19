using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class AccountId
	{
		[CandidName("hash")]
		public List<byte> Hash { get; set; }

		public AccountId(List<byte> hash)
		{
			this.Hash = hash;
		}

		public AccountId()
		{
		}
	}
}