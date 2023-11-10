using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using System;
using Map = System.Collections.Generic.Dictionary<System.String, Sample.Shared.ICRC1Ledger.Models.Value>;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant]
	public class Value
	{
		[VariantTagProperty]
		public ValueTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value_ { get; set; }

		public Value(ValueTag tag, object? value)
		{
			this.Tag = tag;
			this.Value_ = value;
		}

		protected Value()
		{
		}

		public static Value Blob(byte[] info)
		{
			return new Value(ValueTag.Blob, info);
		}

		public static Value Text(string info)
		{
			return new Value(ValueTag.Text, info);
		}

		public static Value Nat(UnboundedUInt info)
		{
			return new Value(ValueTag.Nat, info);
		}

		public static Value Nat64(ulong info)
		{
			return new Value(ValueTag.Nat64, info);
		}

		public static Value Int(UnboundedInt info)
		{
			return new Value(ValueTag.Int, info);
		}

		public static Value Array(Value.ArrayInfo info)
		{
			return new Value(ValueTag.Array, info);
		}

		public static Value Map(Map info)
		{
			return new Value(ValueTag.Map, info);
		}

		public byte[] AsBlob()
		{
			this.ValidateTag(ValueTag.Blob);
			return (byte[])this.Value_!;
		}

		public string AsText()
		{
			this.ValidateTag(ValueTag.Text);
			return (string)this.Value_!;
		}

		public UnboundedUInt AsNat()
		{
			this.ValidateTag(ValueTag.Nat);
			return (UnboundedUInt)this.Value_!;
		}

		public ulong AsNat64()
		{
			this.ValidateTag(ValueTag.Nat64);
			return (ulong)this.Value_!;
		}

		public UnboundedInt AsInt()
		{
			this.ValidateTag(ValueTag.Int);
			return (UnboundedInt)this.Value_!;
		}

		public Value.ArrayInfo AsArray()
		{
			this.ValidateTag(ValueTag.Array);
			return (Value.ArrayInfo)this.Value_!;
		}

		public Map AsMap()
		{
			this.ValidateTag(ValueTag.Map);
			return (Map)this.Value_!;
		}

		private void ValidateTag(ValueTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}

		public class ArrayInfo : List<Value>
		{
			public ArrayInfo()
			{
			}
		}
	}

	public enum ValueTag
	{
		Blob,
		Text,
		Nat,
		Nat64,
		Int,
		Array,
		Map
	}
}