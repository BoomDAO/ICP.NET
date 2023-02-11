using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Subaccount = System.Collections.Generic.List<byte>;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	[Variant(typeof(ValueTag))]
	public class MetaDataValue
	{
		[VariantTagProperty]
		public ValueTag Tag { get; set; }

		[VariantValueProperty]
		private object? Value { get; set; }

		public MetaDataValue(ValueTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected MetaDataValue()
		{
		}

		public static MetaDataValue Nat(UnboundedUInt info)
		{
			return new MetaDataValue(ValueTag.Nat, info);
		}

		public static MetaDataValue Int(UnboundedInt info)
		{
			return new MetaDataValue(ValueTag.Int, info);
		}

		public static MetaDataValue Text(string info)
		{
			return new MetaDataValue(ValueTag.Text, info);
		}

		public static MetaDataValue Blob(Subaccount info)
		{
			return new MetaDataValue(ValueTag.Blob, info);
		}

		public UnboundedUInt AsNat()
		{
			this.ValidateTag(ValueTag.Nat);
			return (UnboundedUInt)this.Value!;
		}

		public UnboundedInt AsInt()
		{
			this.ValidateTag(ValueTag.Int);
			return (UnboundedInt)this.Value!;
		}

		public string AsText()
		{
			this.ValidateTag(ValueTag.Text);
			return (string)this.Value!;
		}

		public Subaccount AsBlob()
		{
			this.ValidateTag(ValueTag.Blob);
			return (Subaccount)this.Value!;
		}

		private void ValidateTag(ValueTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new System.InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum ValueTag
	{
		[CandidName("Nat")]
		[VariantOptionType(typeof(UnboundedUInt))]
		Nat,
		[CandidName("Int")]
		[VariantOptionType(typeof(UnboundedInt))]
		Int,
		[CandidName("Text")]
		[VariantOptionType(typeof(string))]
		Text,
		[CandidName("Blob")]
		[VariantOptionType(typeof(Subaccount))]
		Blob
	}
}