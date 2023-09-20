using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class AccountId
	{
		[CandidName("hash")]
		public byte[] Hazh { get; set; }

		public AccountId(byte[] hazh)
		{
			this.Hazh = hazh;
		}

		public AccountId()
		{
		}
	}
}