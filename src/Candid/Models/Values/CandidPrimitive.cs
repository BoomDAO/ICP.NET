using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Buffers;
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
		public new string? AsText()
		{
			return this.As<string>(PrimitiveType.Text, isNullable: true);
		}

		/// <inheritdoc />
		public new UnboundedUInt AsNat()
		{
			return this.As<UnboundedUInt>(PrimitiveType.Nat, isNullable: false)!;
		}

		/// <inheritdoc />
		public new byte AsNat8()
		{
			return this.As<byte>(PrimitiveType.Nat8, isNullable: false);
		}

		/// <inheritdoc />
		public new ushort AsNat16()
		{
			return this.As<ushort>(PrimitiveType.Nat16, isNullable: false);
		}

		/// <inheritdoc />
		public new uint AsNat32()
		{
			return this.As<uint>(PrimitiveType.Nat32, isNullable: false);
		}

		/// <inheritdoc />
		public new ulong AsNat64()
		{
			return this.As<ulong>(PrimitiveType.Nat64, isNullable: false);
		}

		/// <inheritdoc />
		public new UnboundedInt AsInt()
		{
			return this.As<UnboundedInt>(PrimitiveType.Int, isNullable: false)!;
		}

		/// <inheritdoc />
		public new sbyte AsInt8()
		{
			return this.As<sbyte>(PrimitiveType.Int8, isNullable: false);
		}

		/// <inheritdoc />
		public new short AsInt16()
		{
			return this.As<short>(PrimitiveType.Int16, isNullable: false);
		}

		/// <inheritdoc />
		public new int AsInt32()
		{
			return this.As<int>(PrimitiveType.Int32, isNullable: false);
		}

		/// <inheritdoc />
		public new long AsInt64()
		{
			return this.As<long>(PrimitiveType.Int64, isNullable: false);
		}

		/// <inheritdoc />
		public new float AsFloat32()
		{
			return this.As<float>(PrimitiveType.Float32, isNullable: false);
		}

		/// <inheritdoc />
		public new double AsFloat64()
		{
			return this.As<double>(PrimitiveType.Float64, isNullable: false);
		}

		/// <inheritdoc />
		public new bool AsBool()
		{
			return this.As<bool>(PrimitiveType.Bool, isNullable: false);
		}

		/// <inheritdoc />
		public new Principal? AsPrincipal()
		{
			return this.As<Principal>(PrimitiveType.Principal, isNullable: true);
		}





		/// <inheritdoc />
		internal override void EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType, IBufferWriter<byte> destination)
		{
			Action<IBufferWriter<byte>> encode = this.ValueType switch
			{
				PrimitiveType.Text => this.EncodeText,
				PrimitiveType.Nat => this.EncodeNat,
				PrimitiveType.Nat8 => this.EncodeNat8,
				PrimitiveType.Nat16 => this.EncodeNat16,
				PrimitiveType.Nat32 => this.EncodeNat32,
				PrimitiveType.Nat64 => this.EncodeNat64,
				PrimitiveType.Int => this.EncodeInt,
				PrimitiveType.Int8 => this.EncodeInt8,
				PrimitiveType.Int16 => this.EncodeInt16,
				PrimitiveType.Int32 => this.EncodeInt32,
				PrimitiveType.Int64 => this.EncodeInt64,
				PrimitiveType.Float32 => this.EncodeFloat32,
				PrimitiveType.Float64 => this.EncodeFloat64,
				PrimitiveType.Principal => this.EncodePrincipal,
				PrimitiveType.Bool => this.EncodeBool,
				PrimitiveType.Null => this.EncodeNull,
				PrimitiveType.Reserved => this.EncodeReserved,
				// exclude empty, will never be encoded
				_ => throw new NotImplementedException()
			};
			encode(destination);
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

		private void EncodeText(IBufferWriter<byte> destination)
		{
			string? value = this.AsText();
			// bytes = Length (LEB128) + text (UTF8)
			destination.WriteUtf8LebAndValue(value ?? string.Empty);
		}

		private void EncodeNat(IBufferWriter<byte> destination)
		{
			UnboundedUInt value = this.AsNat();
			LEB128.EncodeUnsigned(value, destination);
		}

		private void EncodeNat8(IBufferWriter<byte> destination)
		{
			byte value = this.AsNat8();
			destination.WriteOne(value);
		}

		private void EncodeNat16(IBufferWriter<byte> destination)
		{
			ushort value = this.AsNat16();
			this.EncodeBigInteger(value, destination, isUnsigned: true, byteSize: 2, isPositive: true);
		}

		private void EncodeNat32(IBufferWriter<byte> destination)
		{
			uint value = this.AsNat32();
			this.EncodeBigInteger(value, destination, isUnsigned: true, byteSize: 4, isPositive: true);
		}

		private void EncodeNat64(IBufferWriter<byte> destination)
		{
			ulong value = this.AsNat64();
			this.EncodeBigInteger(value, destination, isUnsigned: true, byteSize: 8, isPositive: true);
		}

		private void EncodeInt(IBufferWriter<byte> destination)
		{
			UnboundedInt value = this.AsInt();
			LEB128.EncodeSigned(value, destination);
		}

		private void EncodeInt8(IBufferWriter<byte> destination)
		{
			sbyte value = this.AsInt8();
			destination.WriteOne((byte)value);
		}

		private void EncodeInt16(IBufferWriter<byte> destination)
		{
			short value = this.AsInt16();
			this.EncodeBigInteger(value, destination, isUnsigned: false, byteSize: 2, isPositive: value >= 0);
		}

		private void EncodeInt32(IBufferWriter<byte> destination)
		{
			int value = this.AsInt32();
			this.EncodeBigInteger(value, destination, isUnsigned: false, byteSize: 4, isPositive: value >= 0);
		}

		private void EncodeInt64(IBufferWriter<byte> destination)
		{
			long value = this.AsInt64();
			this.EncodeBigInteger(value, destination, isUnsigned: false, byteSize: 8, isPositive: value >= 0);
		}

		private void EncodeFloat32(IBufferWriter<byte> destination)
		{
			float value = this.AsFloat32();
			Span<byte> span = destination.GetSpan(4); // 32 bits
			BitConverter.TryWriteBytes(span, value); // Encode value
			destination.Advance(4);
		}

		private void EncodeFloat64(IBufferWriter<byte> destination)
		{
			double value = this.AsFloat64();
			Span<byte> span = destination.GetSpan(8); // 64 bits
			BitConverter.TryWriteBytes(span, value); // Encode value
			destination.Advance(8);
		}

		private void EncodePrincipal(IBufferWriter<byte> destination)
		{
			Principal? principalId = this.AsPrincipal();
			if (principalId == null)
			{
				// Opaque
				destination.WriteOne<byte>(0);
			}
			else
			{
				destination.WriteOne<byte>(1); // Encode indication that it is not an opaque reference
				LEB128.EncodeSigned(principalId.Raw.Length, destination); // Encode byte length
				destination.Write(principalId.Raw); // Encode bytes
			}
		}

		private void EncodeBool(IBufferWriter<byte> destination)
		{
			bool value = this.AsBool();
			Span<byte> span = destination.GetSpan(1); // 1 byte
			BitConverter.TryWriteBytes(span, value); // Encode value
			destination.Advance(1);
		}

		private void EncodeNull(IBufferWriter<byte> destination)
		{
			// No encoding
		}

		private void EncodeReserved(IBufferWriter<byte> destination)
		{
			// No encoding
		}

		private void EncodeBigInteger(BigInteger value, IBufferWriter<byte> destination, bool isUnsigned, int byteSize, bool isPositive)
		{
			Span<byte> span = destination.GetSpan(value.GetByteCount(isUnsigned));
			value.TryWriteBytes(span, out int bytesWritten, isUnsigned, isBigEndian: false); // write big int
			destination.Advance(bytesWritten);
			if (bytesWritten > byteSize)
			{
				throw new InvalidOperationException("Bytes is already too large");
			}
			if (bytesWritten == byteSize)
			{
				return; // No padding required
			}
			int paddingSize = byteSize - bytesWritten;
			byte paddingByte = isPositive ? (byte)0 : (byte)0b1111_1111; // Pad with 0s when posiive. 1's when negative

			Span<byte> paddingBytes = destination.GetSpan(paddingSize);
			for (int i = 0; i < paddingSize; i++)
			{
				paddingBytes[i] = paddingByte; // Pad bytes
			}
			destination.Advance(paddingSize);
		}



		private T? As<T>(PrimitiveType type, bool isNullable)
		{
			if (this.ValueType != type)
			{
				if (isNullable && this.ValueType == PrimitiveType.Null)
				{
					return default;
				}
				throw new InvalidOperationException($"Cannot convert candid primitive type '{this.ValueType}' to candid type '{type}'");
			}
			return (T)this.value!;
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
