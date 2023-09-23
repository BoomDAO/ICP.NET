using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class AccountId
	{
		[CandidName("hash")]
		public byte[] Hash { get; set; }

		public AccountId(byte[] hash)
		{
			this.Hash = hash;
		}

		public AccountId()
		{
		}
	}
}