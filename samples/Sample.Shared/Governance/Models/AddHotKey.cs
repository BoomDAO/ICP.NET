using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class AddHotKey
	{
		[CandidName("new_hot_key")]
		public OptionalValue<Principal> NewHotKey { get; set; }

		public AddHotKey(OptionalValue<Principal> newHotKey)
		{
			this.NewHotKey = newHotKey;
		}

		public AddHotKey()
		{
		}
	}
}