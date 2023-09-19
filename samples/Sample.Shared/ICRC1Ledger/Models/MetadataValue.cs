using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<Sample.Shared.ICRC1Ledger.Models.MapItem>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant(typeof(MetadataValueTag))]
	public class MetadataValue
	{
		[VariantTagProperty()]
		public MetadataValueTag Tag { get; set; }

		[VariantValueProperty()]
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

		public static MetadataValue Blob(List<byte> info)
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

		public List<byte> AsBlob()
		{
			this.ValidateTag(MetadataValueTag.Blob);
			return (List<byte>)this.Value!;
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
		[VariantOptionType(typeof(UnboundedUInt))]
		Nat,
		[VariantOptionType(typeof(UnboundedInt))]
		Int,
		[VariantOptionType(typeof(string))]
		Text,
		[VariantOptionType(typeof(List<byte>))]
		Blob
	}
}