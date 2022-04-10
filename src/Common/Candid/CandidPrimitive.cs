using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Common.Candid
{
	public enum CandidPrimitiveType
	{
		Text,
		Nat,
		Nat8,
		Nat16,
		Nat32,
		Nat64,
		Int,
		Int8,
		Int16,
		Int32,
		Int64,
		Float32,
		Float64,
		Bool,
		Principal
	}

	public class CandidPrimitive : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Primitive;
		public CandidPrimitiveType ValueType { get; }
		private readonly object value;

		private CandidPrimitive(CandidPrimitiveType valueType, object value)
		{
			this.ValueType = valueType;
			this.value = value;
		}

		public string AsText()
		{
			this.ValidateType(CandidPrimitiveType.Text);
			return (string)this.value;
		}

		public UnboundedUInt AsNat()
		{
			this.ValidateType(CandidPrimitiveType.Nat);
			return (UnboundedUInt)this.value;
		}

		public byte AsNat8()
		{
			this.ValidateType(CandidPrimitiveType.Nat8);
			return (byte)this.value;
		}

		public ushort AsNat16()
		{
			this.ValidateType(CandidPrimitiveType.Nat16);
			return (ushort)this.value;
		}

		public uint AsNat32()
		{
			this.ValidateType(CandidPrimitiveType.Nat32);
			return (uint)this.value;
		}

		public ulong AsNat64()
		{
			this.ValidateType(CandidPrimitiveType.Nat64);
			return (uint)this.value;
		}

		public UnboundedInt AsInt()
		{
			this.ValidateType(CandidPrimitiveType.Int);
			return (UnboundedInt)this.value;
		}

		public byte AsInt8()
		{
			this.ValidateType(CandidPrimitiveType.Int8);
			return (byte)this.value;
		}

		public short AsInt16()
		{
			this.ValidateType(CandidPrimitiveType.Int16);
			return (short)this.value;
		}

		public int AsInt32()
		{
			this.ValidateType(CandidPrimitiveType.Int32);
			return (int)this.value;
		}

		public long AsInt64()
		{
			this.ValidateType(CandidPrimitiveType.Int64);
			return (long)this.value;
		}

		public float AsFloat32()
		{
			this.ValidateType(CandidPrimitiveType.Float32);
			return (float)this.value;
		}

		public double AsFloat64()
		{
			this.ValidateType(CandidPrimitiveType.Float64);
			return (double)this.value;
		}

		public bool AsBool()
		{
			this.ValidateType(CandidPrimitiveType.Bool);
			return (bool)this.value;
		}

		public byte[] AsPrincipal()
		{
			this.ValidateType(CandidPrimitiveType.Principal);
			return (byte[])this.value;
		}





		public override byte[] EncodeValue()
		{
			return this.ValueType switch
			{
				CandidPrimitiveType.Text => this.EncodeText(),
				CandidPrimitiveType.Nat => this.EncodeNat(),
				CandidPrimitiveType.Nat8 => this.EncodeNat8(),
				CandidPrimitiveType.Nat16 => this.EncodeNat16(),
				CandidPrimitiveType.Nat32 => this.EncodeNat32(),
				CandidPrimitiveType.Nat64 => this.EncodeNat64(),
				CandidPrimitiveType.Int => this.EncodeInt(),
				CandidPrimitiveType.Int8 => this.EncodeInt8(),
				CandidPrimitiveType.Int16 => this.EncodeInt16(),
				CandidPrimitiveType.Int32 => this.EncodeInt32(),
				CandidPrimitiveType.Int64 => this.EncodeInt64(),
				CandidPrimitiveType.Float32 => this.EncodeFloat32(),
				CandidPrimitiveType.Float64 => this.EncodeFloat64(),
				CandidPrimitiveType.Bool => this.EncodeBool(),
				CandidPrimitiveType.Principal => this.EncodePrincipal(),
				_ => throw new NotImplementedException()
			};
		}

		private byte[] EncodeText()
		{
			string value = this.AsText();
			// bytes = Length (LEB128) + text (UTF8)
			return LEB128.FromUInt64((ulong)value.Length).Raw
					   .Concat(Encoding.UTF8.GetBytes(value))
					   .ToArray();
		}

		private byte[] EncodeNat()
		{
			UnboundedUInt value = this.AsNat();
			return LEB128.FromNat(value).Raw;
		}

		private byte[] EncodeNat8()
		{
			byte value = this.AsNat8();
			return new byte[] { value };
		}

		private byte[] EncodeNat16()
		{
			ushort value = this.AsNat16();
			return new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
		}

		private byte[] EncodeNat32()
		{
			uint value = this.AsNat32();
			return new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
		}

		private byte[] EncodeNat64()
		{
			ulong value = this.AsNat64();
			return new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
		}

		private byte[] EncodeInt()
		{
			UnboundedInt value = this.AsInt();
			return SLEB128.FromInt(value).Raw;
		}

		private byte[] EncodeInt8()
		{
			byte value = this.AsInt8();
			return new byte[] { value };
		}

		private byte[] EncodeInt16()
		{
			short value = this.AsInt16();
			return new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
		}

		private byte[] EncodeInt32()
		{
			int value = this.AsInt32();
			return new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
		}

		private byte[] EncodeInt64()
		{
			long value = this.AsInt64();
			return new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
		}

		private byte[] EncodeFloat32()
		{
			float value = this.AsFloat32();
			return BitConverter.GetBytes(value);
		}

		private byte[] EncodeFloat64()
		{
			double value = this.AsFloat64();
			return BitConverter.GetBytes(value);
		}

		private byte[] EncodeBool()
		{
			bool value = this.AsBool();
			return BitConverter.GetBytes(value);
		}

		private byte[] EncodePrincipal()
		{
			byte[] value = this.AsPrincipal();
			byte[] encodedValueLength = LEB128.FromUInt64((ulong)value.Length).Raw;
			return new byte[] { 1 }
				.Concat(encodedValueLength)
				.Concat(value)
				.ToArray();
		}








		public static CandidValue Blob(byte[] value)
		{
			return new CandidPrimitive(CandidPrimitiveType.Blob, value);
		}

		public static CandidPrimitive Nat(UnboundedUInt value)
		{
			return new CandidPrimitive(CandidPrimitiveType.Nat, value);
		}

		public static CandidValue Text(string value)
		{
			return new CandidPrimitive(CandidPrimitiveType.Text, value);
		}





		protected void ValidateType(CandidPrimitiveType type)
		{
			if (this.ValueType != type)
			{
				throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
			}
		}
	}
}
