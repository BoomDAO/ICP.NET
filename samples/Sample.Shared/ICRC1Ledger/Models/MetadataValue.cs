using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant]
	public class MetadataValue
	{
		[VariantTagProperty]
		public MetadataValueTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public MetadataValue(MetadataValueTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected MetadataValue()
		{
		}

		public static MetadataValue Nat(UnboundedUInt info)
		{
			return new MetadataValue(MetadataValueTag.Nat, info);
		}

		public static MetadataValue Int(UnboundedInt info)
		{
			return new MetadataValue(MetadataValueTag.Int, info);
		}

		public static MetadataValue Text(string info)
		{
			return new MetadataValue(MetadataValueTag.Text, info);
		}

		public static MetadataValue Blob(byte[] info)
		{
			return new MetadataValue(MetadataValueTag.Blob, info);
		}

		public UnboundedUInt AsNat()
		{
			this.ValidateTag(MetadataValueTag.Nat);
			return (UnboundedUInt)this.Value!;
		}

		public UnboundedInt AsInt()
		{
			this.ValidateTag(MetadataValueTag.Int);
			return (UnboundedInt)this.Value!;
		}

		public string AsText()
		{
			this.ValidateTag(MetadataValueTag.Text);
			return (string)this.Value!;
		}

		public byte[] AsBlob()
		{
			this.ValidateTag(MetadataValueTag.Blob);
			return (byte[])this.Value!;
		}

		private void ValidateTag(MetadataValueTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
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