using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Linq;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.Candid.Models.Values
{
	/// <summary>
	/// All the candid primitive types
	/// </summary>
	public enum PrimitiveType
	{
		/// <summary>
		/// A text/string value
		/// </summary>
		Text,
		/// <summary>
		/// A unbounded unsigned integer (natural number)
		/// </summary>
		Nat,
		/// <summary>
		/// A 8-bit unsigned integer (natural number)
		/// </summary>
		Nat8,
		/// <summary>
		/// A 16-bit unsigned integer (natural number)
		/// </summary>
		Nat16,
		/// <summary>
		/// A 32-bit unsigned integer (natural number)
		/// </summary>
		Nat32,
		/// <summary>
		/// A 64-bit unsigned integer (natural number)
		/// </summary>
		Nat64,
		/// <summary>
		/// A unbounded integer
		/// </summary>
		Int,
		/// <summary>
		/// A 8-bit integer
		/// </summary>
		Int8,
		/// <summary>
		/// A 16-bit integer
		/// </summary>
		Int16,
		/// <summary>
		/// A 32-bit integer
		/// </summary>
		Int32,
		/// <summary>
		/// A 64-bit integer
		/// </summary>
		Int64,
		/// <summary>
		/// A 32-bit floating point number
		/// </summary>
		Float32,
		/// <summary>
		/// A 64-bit floating point number
		/// </summary>
		Float64,
		/// <summary>
		/// A boolean (true/false) value
		/// </summary>
		Bool,
		/// <summary>
		/// A candid principal value which works as an identifier for identities/canisters
		/// </summary>
		Principal,
		/// <summary>
		/// A 'any' type value that is a supertype of all types. It allows the removal of a type without breaking
		/// the type structure
		/// </summary>
		Reserved,
		/// <summary>
		/// A value with no data that is considered a subtype of all types. Practical use cases for the empty type are relatively rare.
		/// </summary>
		Empty,
		/// <summary>
		/// The null value that is a supertype of any `opt t` value
		/// </summary>
		Null
	}

	/// <summary>
	/// A model representing a candid primitive type
	/// </summary>
	public class CandidPrimitive : CandidValue
	{
		/// <inheritdoc />
		public override CandidValueType Type { get; } = CandidValueType.Primitive;

		/// <summary>
		/// The specific primitive type that is represented
		/// </summary>
		public PrimitiveType ValueType { get; }

		private readonly object? value;

		internal CandidPrimitive(PrimitiveType valueType, object? value)
		{
			this.ValueType = valueType;
			this.value = value;
		}

		/// <inheritdoc />
		public new string AsText()
		{
			this.ValidateType(PrimitiveType.Text);
			return (string)this.value!;
		}

		/// <inheritdoc />
		public new UnboundedUInt AsNat()
		{
			this.ValidateType(PrimitiveType.Nat);
			return (UnboundedUInt)this.value!;
		}

		/// <inheritdoc />
		public new byte AsNat8()
		{
			this.ValidateType(PrimitiveType.Nat8);
			return (byte)this.value!;
		}

		/// <inheritdoc />
		public new ushort AsNat16()
		{
			this.ValidateType(PrimitiveType.Nat16);
			return (ushort)this.value!;
		}

		/// <inheritdoc />
		public new uint AsNat32()
		{
			this.ValidateType(PrimitiveType.Nat32);
			return (uint)this.value!;
		}

		/// <inheritdoc />
		public new ulong AsNat64()
		{
			this.ValidateType(PrimitiveType.Nat64);
			return (ulong)this.value!;
		}

		/// <inheritdoc />
		public new UnboundedInt AsInt()
		{
			this.ValidateType(PrimitiveType.Int);
			return (UnboundedInt)this.value!;
		}

		/// <inheritdoc />
		public new sbyte AsInt8()
		{
			this.ValidateType(PrimitiveType.Int8);
			return (sbyte)this.value!;
		}

		/// <inheritdoc />
		public new short AsInt16()
		{
			this.ValidateType(PrimitiveType.Int16);
			return (short)this.value!;
		}

		/// <inheritdoc />
		public new int AsInt32()
		{
			this.ValidateType(PrimitiveType.Int32);
			return (int)this.value!;
		}

		/// <inheritdoc />
		public new long AsInt64()
		{
			this.ValidateType(PrimitiveType.Int64);
			return (long)this.value!;
		}

		/// <inheritdoc />
		public new float AsFloat32()
		{
			this.ValidateType(PrimitiveType.Float32);
			return (float)this.value!;
		}

		/// <inheritdoc />
		public new double AsFloat64()
		{
			this.ValidateType(PrimitiveType.Float64);
			return (double)this.value!;
		}

		/// <inheritdoc />
		public new bool AsBool()
		{
			this.ValidateType(PrimitiveType.Bool);
			return (bool)this.value!;
		}

		/// <inheritdoc />
		public new Principal AsPrincipal()
		{
			this.ValidateType(PrimitiveType.Principal);
			return (Principal)this.value!;
		}





		/// <inheritdoc />
		internal override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			return this.ValueType switch
			{
				PrimitiveType.Text => this.EncodeText(),
				PrimitiveType.Nat => this.EncodeNat(),
				PrimitiveType.Nat8 => this.EncodeNat8(),
				PrimitiveType.Nat16 => this.EncodeNat16(),
				PrimitiveType.Nat32 => this.EncodeNat32(),
				PrimitiveType.Nat64 => this.EncodeNat64(),
				PrimitiveType.Int => this.EncodeInt(),
				PrimitiveType.Int8 => this.EncodeInt8(),
				PrimitiveType.Int16 => this.EncodeInt16(),
				PrimitiveType.Int32 => this.EncodeInt32(),
				PrimitiveType.Int64 => this.EncodeInt64(),
				PrimitiveType.Float32 => this.EncodeFloat32(),
				PrimitiveType.Float64 => this.EncodeFloat64(),
				PrimitiveType.Principal => this.EncodePrincipal(),
				PrimitiveType.Bool => this.EncodeBool(),
				PrimitiveType.Null => this.EncodeNull(),
				PrimitiveType.Reserved => this.EncodeReserved(),
				// exclude empty, will never be encoded
				_ => throw new NotImplementedException()
			};
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.ValueType switch
			{
				PrimitiveType.Text => $"'{this.AsText()}'",
				PrimitiveType.Nat => this.AsNat().ToString(),
				PrimitiveType.Nat8 => this.AsNat8().ToString(),
				PrimitiveType.Nat16 => this.AsNat16().ToString(),
				PrimitiveType.Nat32 => this.AsNat32().ToString(),
				PrimitiveType.Nat64 => this.AsNat64().ToString(),
				PrimitiveType.Int => this.AsInt().ToString(),
				PrimitiveType.Int8 => this.AsInt8().ToString(),
				PrimitiveType.Int16 => this.AsInt16().ToString(),
				PrimitiveType.Int32 => this.AsInt32().ToString(),
				PrimitiveType.Int64 => this.AsInt64().ToString(),
				PrimitiveType.Float32 => this.AsFloat32().ToString(),
				PrimitiveType.Float64 => this.AsFloat64().ToString(),
				PrimitiveType.Principal => this.AsPrincipal()?.ToString() ?? "(Opaque Reference)",
				PrimitiveType.Bool => this.AsBool() ? "true" : "false",
				PrimitiveType.Null => "null",
				PrimitiveType.Reserved => "(reserved)",
				PrimitiveType.Empty => "(empty)",
				_ => throw new NotImplementedException()
			};
		}

		private byte[] EncodeText()
		{
			string value = this.AsText();
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			// bytes = Length (LEB128) + text (UTF8)
			return LEB128.EncodeSigned(bytes.Length)
					   .Concat(bytes)
					   .ToArray();
		}

		private byte[] EncodeNat()
		{
			UnboundedUInt value = this.AsNat();
			return LEB128.EncodeUnsigned(value);
		}

		private byte[] EncodeNat8()
		{
			byte value = this.AsNat8();
			return new byte[] { value };
		}

		private byte[] EncodeNat16()
		{
			ushort value = this.AsNat16();

			byte[] bytes = new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
			return CandidPrimitive.PadBytes(bytes, 2, isPositive: true);
		}

		private byte[] EncodeNat32()
		{
			uint value = this.AsNat32();
			byte[] bytes = new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
			return CandidPrimitive.PadBytes(bytes, 4, isPositive: true);
		}

		private byte[] EncodeNat64()
		{
			ulong value = this.AsNat64();
			byte[] bytes = new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
			return CandidPrimitive.PadBytes(bytes, 8, isPositive: true);
		}

		private byte[] EncodeInt()
		{
			UnboundedInt value = this.AsInt();
			return LEB128.EncodeSigned(value);
		}

		private byte[] EncodeInt8()
		{
			sbyte value = this.AsInt8();
			return new byte[] { (byte)value };
		}

		private byte[] EncodeInt16()
		{
			short value = this.AsInt16();
			byte[] bytes = new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);

			return CandidPrimitive.PadBytes(bytes, 2, isPositive: value >= 0);
		}

		private byte[] EncodeInt32()
		{
			int value = this.AsInt32();
			byte[] bytes = new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
			return CandidPrimitive.PadBytes(bytes, 4, isPositive: value >= 0);
		}

		private byte[] EncodeInt64()
		{
			long value = this.AsInt64();
			byte[] bytes = new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
			return CandidPrimitive.PadBytes(bytes, 8, isPositive: value >= 0);
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

		private byte[] EncodePrincipal()
		{
			// TODO how to do opaque?
			Principal principalId = this.AsPrincipal();
			byte[] value = principalId.Raw;
			byte[] encodedValueLength = LEB128.EncodeSigned(value.Length);
			return new byte[] { 1 }
				.Concat(encodedValueLength)
				.Concat(value)
				.ToArray();
		}

		private byte[] EncodeBool()
		{
			bool value = this.AsBool();
			return BitConverter.GetBytes(value);
		}

		private byte[] EncodeNull()
		{
			return new byte[0];
		}

		private byte[] EncodeReserved()
		{
			return new byte[0];
		}

		private static byte[] PadBytes(byte[] bytes, int byteSize, bool isPositive)
		{
			if (bytes.Length > byteSize)
			{
				throw new ArgumentException("Bytes is already too large");
			}
			if (bytes.Length == byteSize)
			{
				return bytes;
			}
			int paddingSize = byteSize - bytes.Length;
			byte paddingByte = isPositive ? (byte)0 : (byte)0b1111_1111; // Pad with 0s when posiive. 1's when negative
			return bytes
				.Concat(Enumerable.Range(0, paddingSize).Select(x => paddingByte))
				.ToArray();
		}


		private void ValidateType(PrimitiveType type)
		{
			if (this.ValueType != type)
			{
				throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
			}
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.value, this.ValueType);
		}

		/// <inheritdoc />
		public override bool Equals(CandidValue? other)
		{
			if (other is CandidPrimitive p)
			{
				if (this.ValueType == p.ValueType)
				{
					if (this.value == null)
					{
						return p.value == null;
					}
					return this.value.Equals(p.value);
				}
			}
			return false;
		}
	}
}
