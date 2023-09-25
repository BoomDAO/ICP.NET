using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class MetadataValue
	{
		[VariantTagProperty()]
		public MetadataValueTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public UnboundedUInt? Nat { get => this.Tag == MetadataValueTag.Nat ? (UnboundedUInt)this.Value : default; set => (this.Tag, this.Value) = (MetadataValueTag.Nat, value); }

		public UnboundedInt? Int { get => this.Tag == MetadataValueTag.Int ? (UnboundedInt)this.Value : default; set => (this.Tag, this.Value) = (MetadataValueTag.Int, value); }

		public string? Text { get => this.Tag == MetadataValueTag.Text ? (string)this.Value : default; set => (this.Tag, this.Value) = (MetadataValueTag.Text, value); }

		public List<byte>? Blob { get => this.Tag == MetadataValueTag.Blob ? (List<byte>)this.Value : default; set => (this.Tag, this.Value) = (MetadataValueTag.Blob, value); }

		public MetadataValue(MetadataValueTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected MetadataValue()
		{
		}
	}

	public enum MetadataValueTag
	{
		Nat,
		Int,
		Text,
		Blob
	}
}