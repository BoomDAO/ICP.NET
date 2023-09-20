using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class AddOrRemoveNodeProvider
	{
		[CandidName("change")]
		public OptionalValue<Change> Change { get; set; }

		public AddOrRemoveNodeProvider(OptionalValue<Change> change)
		{
			this.Change = change;
		}

		public AddOrRemoveNodeProvider()
		{
		}
	}
}