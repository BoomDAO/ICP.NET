using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class AccountIdentifier
	{
		[CandidName("hash")]
		public byte[] Hash { get; set; }

		public AccountIdentifier(byte[] hash)
		{
			this.Hash = hash;
		}

		public AccountIdentifier()
		{
		}
	}
}