using EdjCase.ICP.Candid.Mapping;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class DeleteAssetArguments
	{
		[CandidName("key")]
		public Key Key { get; set; }

		public DeleteAssetArguments(Key key)
		{
			this.Key = key;
		}

		public DeleteAssetArguments()
		{
		}
	}
}