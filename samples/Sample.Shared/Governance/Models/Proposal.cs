using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class Proposal
	{
		[CandidName("url")]
		public string Url { get; set; }

		[CandidName("title")]
		public OptionalValue<string> Title { get; set; }

		[CandidName("action")]
		public OptionalValue<Action> Action { get; set; }

		[CandidName("summary")]
		public string Summary { get; set; }

		public Proposal(string url, OptionalValue<string> title, OptionalValue<Action> action, string summary)
		{
			this.Url = url;
			this.Title = title;
			this.Action = action;
			this.Summary = summary;
		}

		public Proposal()
		{
		}
	}
}