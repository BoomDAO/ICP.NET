using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Mapping;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class StoreRequest
	{
		[CandidName("key")]
		public Key Key { get; set; }

		[CandidName("content_type")]
		public string ContentType { get; set; }

		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		[CandidName("content")]
		public byte[] Content { get; set; }

		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		public StoreRequest(Key key, string contentType, string contentEncoding, byte[] content, OptionalValue<byte[]> sha256)
		{
			this.Key = key;
			this.ContentType = contentType;
			this.ContentEncoding = contentEncoding;
			this.Content = content;
			this.Sha256 = sha256;
		}

		public StoreRequest()
		{
		}
	}
}
