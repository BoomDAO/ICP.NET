using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents an asset in the asset canister.
	/// </summary>
	public class Asset
	{
		/// <summary>
		/// The key of the asset.
		/// </summary>
		[CandidName("key")]
		public string Key { get; set; }

		/// <summary>
		/// The content type of the asset.
		/// </summary>
		[CandidName("content_type")]
		public string ContentType { get; set; }

		/// <summary>
		/// A list of encodings for the asset.
		/// </summary>
		[CandidName("encodings")]
		public List<Encoding> Encodings { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Asset"/> class.
		/// </summary>
		/// <param name="key">The key of the asset.</param>
		/// <param name="contentType">The content type of the asset.</param>
		/// <param name="encodings">A list of encodings for the asset.</param>
		public Asset(string key, string contentType, List<Encoding> encodings)
		{
			this.Key = key;
			this.ContentType = contentType;
			this.Encodings = encodings;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Asset"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Asset()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}

	/// <summary>
	/// Represents an encoding of an asset.
	/// </summary>
	public class Encoding
	{
		/// <summary>
		/// The content encoding of the asset.
		/// </summary>
		[CandidName("content_encoding")]
		public string ContentEncoding { get; set; }

		/// <summary>
		/// The SHA256 hash of the asset content.
		/// </summary>
		[CandidName("sha256")]
		public OptionalValue<byte[]> Sha256 { get; set; }

		/// <summary>
		/// The length of the asset content.
		/// </summary>
		[CandidName("length")]
		public UnboundedUInt Length { get; set; }

		/// <summary>
		/// The last modified timestamp of the asset content.
		/// </summary>
		[CandidName("modified")]
		public UnboundedInt Modified { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Encoding"/> class.
		/// </summary>
		/// <param name="contentEncoding">The content encoding of the asset.</param>
		/// <param name="sha256">The SHA256 hash of the asset content.</param>
		/// <param name="length">The length of the asset content.</param>
		/// <param name="modified">The last modified timestamp of the asset content.</param>
		public Encoding(string contentEncoding, OptionalValue<byte[]> sha256, UnboundedUInt length, UnboundedInt modified)
		{
			this.ContentEncoding = contentEncoding;
			this.Sha256 = sha256;
			this.Length = length;
			this.Modified = modified;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Encoding"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Encoding()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
