using EdjCase.ICP.Candid.Mapping;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class UnsetAssetContentArguments
	{
		[CandidName("key")]
		public Key Key { get; set; }

		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		public UnsetAssetContentArguments(Key key, string contentEncoding)
		{
			this.Key = key;
			this.ContentEncoding = contentEncoding;
		}

		public UnsetAssetContentArguments()
		{
		}
	}
}