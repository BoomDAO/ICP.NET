using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class StandardRecord
	{
		[CandidName("url")]
		public string Url { get; set; }

		[CandidName("name")]
		public string Name { get; set; }

		public StandardRecord(string url, string name)
		{
			this.Url = url;
			this.Name = name;
		}

		public StandardRecord()
		{
		}
	}
}