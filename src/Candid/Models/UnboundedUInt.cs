using EdjCase.ICP.Candid.Utilities;
using System;
using System.Numerics;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// An unsigned integer value with no bounds on how large it can get and variable byte size
	/// </summary>
	public class UnboundedUInt : IComparable<UnboundedUInt>, IEquatable<UnboundedUInt>
	{
		private BigInteger value;

		private UnboundedUInt(BigInteger value)
		{
			if (value < 0)
			{
				throw new ArgumentException("Value must be 0 or greater");
			}
			this.value = value;
		}

		/// <summary>
		/// Gets the raw bytes of the number
		/// </summary>
		/// <param name="isBigEndian">True if the byte order should be big endian (most significant bytes first),
		/// otherwise the order will be in little endian (least significant bytes first)</param>
		/// <returns>Byte array of the number</returns>
		public byte[] GetRawBytes(bool isBigEndian)
		{
			return this.value.ToByteArray(isUnsigned: true, isBigEndian: isBigEndian);
		}

		/// <summary>
		/// Tries to get the UInt64 representation of the value, will not if that value is too large to fit
		/// into a UInt64.
		/// </summary>
		/// <param name="value">Out parameter that is set ONLY if the return value is true</param>
		/// <returns>True if converted, otherwise false</returns>
		public bool TryToUInt64(out ulong value)
		{
			if (this.value <= ulong.MaxValue)
			{
				value = (ulong)this.value;
				return true;
			}
			value = 0;
			return false;
		}

		/// <summary>
		/// Converts a big integer to an unbounded uint
		/// </summary>
		/// <param name="value">Big integer to convert</param>
		/// <returns>An unbounded uint</returns>
		public static UnboundedUInt FromBigInteger(BigInteger value)
		{
			return new UnboundedUInt(value);
		}

		/// <summary>
		/// Converts a unbounded uint to a big integer value
		/// </summary>
		/// <returns>A big integer</returns>
		public BigInteger ToBigInteger()
		{
			return this.value;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.value.ToString();
		}

		/// <inheritdoc />
		public bool Equals(UnboundedUInt? uuint)
		{
			return this.CompareTo(uuint) == 0;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return this.Equals(obj as UnboundedUInt);
		}

		/// <inheritdoc />
		public int CompareTo(UnboundedUInt? other)
		{
			if (ReferenceEquals(other, null))
			{
				return 1;
			}
			return this.value.CompareTo(other.value);
		}

		/// <summary>
		/// A helper method to convert a UInt64 to a unbounded uint
		/// </summary>
		/// <param name="value">A UInt64 value</param>
		/// <returns>An unbounded uint</returns>
		public static UnboundedUInt FromUInt64(ulong value)
		{
			return new UnboundedUInt(new BigInteger(value));
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}


		/// <inheritdoc />
		public static bool operator ==(UnboundedUInt? v1, UnboundedUInt? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		/// <inheritdoc />
		public static bool operator !=(UnboundedUInt? v1, UnboundedUInt? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}

		/// <inheritdoc />
		public static UnboundedUInt operator +(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value + b.value);
		}

		/// <inheritdoc />
		public static UnboundedUInt operator -(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value - b.value);
		}

		/// <inheritdoc />
		public static UnboundedUInt operator *(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value * b.value);
		}

		/// <inheritdoc />
		public static UnboundedUInt operator /(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value / b.value);
		}

		/// <inheritdoc />
		public static bool operator <(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value < b.value;
		}

		/// <inheritdoc />
		public static bool operator >(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value > b.value;
		}

		/// <inheritdoc />
		public static bool operator <=(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value <= b.value;
		}

		/// <inheritdoc />
		public static bool operator >=(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value >= b.value;
		}

		/// <inheritdoc />
		public static UnboundedUInt operator ++(UnboundedUInt a)
		{
			return new UnboundedUInt(a.value + 1);
		}

		/// <inheritdoc />
		public static UnboundedUInt operator --(UnboundedUInt a)
		{
			return new UnboundedUInt(a.value - 1);
		}


		/// <summary>
		/// A helper method to implicitly convert a unbounded int to an unbounded uint
		/// </summary>
		/// <param name="value">An unbounded uint</param>
		public static explicit operator UnboundedUInt(UnboundedInt value)
		{
			if (value < 0)
			{
				throw new InvalidCastException("Value must be 0 or greater");
			}
			return FromBigInteger(value.ToBigInteger());
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt64 to an unbounded uint
		/// </summary>
		/// <param name="value">An UInt64 value</param>
		public static implicit operator UnboundedUInt(ulong value)
		{
			return FromUInt64(value);
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt32 to an unbounded uint
		/// </summary>
		/// <param name="value">An UInt32 value</param>
		public static implicit operator UnboundedUInt(uint value)
		{
			return FromUInt64(value);
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt16 to an unbounded uint
		/// </summary>
		/// <param name="value">An UInt16 value</param>
		public static implicit operator UnboundedUInt(ushort value)
		{
			return FromUInt64(value);
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt8 to an unbounded uint
		/// </summary>
		/// <param name="value">An UInt8 value</param>
		public static implicit operator UnboundedUInt(byte value)
		{
			return FromUInt64(value);
		}

		/// <summary>
		/// A helper method to explicitly convert a Int64 to an unbounded uint
		/// </summary>
		/// <param name="value">An Int64 value</param>
		public static explicit operator UnboundedUInt(long value)
		{
			return FromUInt64((ulong)value);
		}

		/// <summary>
		/// A helper method to explicitly convert a Int32 to an unbounded uint
		/// </summary>
		/// <param name="value">An Int32 value</param>
		public static explicit operator UnboundedUInt(int value)
		{
			return FromUInt64((uint)value);
		}

		/// <summary>
		/// A helper method to explicitly convert a Int16 to an unbounded uint
		/// </summary>
		/// <param name="value">An Int16 value</param>
		public static explicit operator UnboundedUInt(short value)
		{
			return FromUInt64((ushort)value);
		}

		/// <summary>
		/// A helper method to explicitly convert a Int8 to an unbounded uint
		/// </summary>
		/// <param name="value">An Int8 value</param>
		public static explicit operator UnboundedUInt(sbyte value)
		{
			return FromUInt64((byte)value);
		}


		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a UInt64
		/// </summary>
		/// <param name="value">A UInt64</param>
		public static explicit operator ulong(UnboundedUInt value)
		{
			ValidateMinMax(value, ulong.MinValue, ulong.MaxValue);
			return (ulong)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a UInt32
		/// </summary>
		/// <param name="value">A UInt32</param>
		public static explicit operator uint(UnboundedUInt value)
		{
			ValidateMinMax(value, uint.MinValue, uint.MaxValue);
			return (uint)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a UInt16
		/// </summary>
		/// <param name="value">A UInt16</param>
		public static explicit operator ushort(UnboundedUInt value)
		{
			ValidateMinMax(value, ushort.MinValue, ushort.MaxValue);
			return (ushort)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a UInt8
		/// </summary>
		/// <param name="value">A UInt8</param>
		public static explicit operator byte(UnboundedUInt value)
		{
			ValidateMinMax(value, byte.MinValue, byte.MaxValue);
			return (byte)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a Int64
		/// </summary>
		/// <param name="value">An Int64</param>
		public static explicit operator long(UnboundedUInt value)
		{
			ValidateMinMax(value, long.MinValue, long.MaxValue);
			return (long)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a Int32
		/// </summary>
		/// <param name="value">An Int32</param>
		public static explicit operator int(UnboundedUInt value)
		{
			ValidateMinMax(value, int.MinValue, int.MaxValue);
			return (int)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a Int16
		/// </summary>
		/// <param name="value">An Int16</param>
		public static explicit operator short(UnboundedUInt value)
		{
			ValidateMinMax(value, short.MinValue, short.MaxValue);
			return (short)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded uint to a Int8
		/// </summary>
		/// <param name="value">An Int8</param>
		public static explicit operator sbyte(UnboundedUInt value)
		{
			ValidateMinMax(value, sbyte.MinValue, sbyte.MaxValue);
			return (sbyte)value.value;
		}

		private static void ValidateMinMax(UnboundedUInt value, BigInteger minValue, BigInteger maxValue)
		{
			if (value.value > maxValue)
			{
				throw new InvalidCastException("Value is too large");
			}
			if (value.value < minValue)
			{
				throw new InvalidCastException("Value is too small");
			}
		}
	}

	/// <summary>
	/// Extensions methods around UnboundedUInt
	/// </summary>
	public static class UnboundedUIntExtensions
	{
		/// <summary>
		/// Converts a UInt64 to a unbounded uint
		/// </summary>
		/// <param name="value">Int64 value</param>
		/// <returns>An unbounded uint</returns>
		public static UnboundedUInt ToUnbounded(this ulong value)
		{
			return UnboundedUInt.FromUInt64(value);
		}
	}
}
