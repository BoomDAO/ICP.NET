using EdjCase.ICP.Candid.Utilities;
using System;
using System.Numerics;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// An integer value with no bounds on how large it can get and variable byte size
	/// </summary>
	public class UnboundedInt : IComparable<UnboundedInt>, IEquatable<UnboundedInt>
	{
		private BigInteger value;

		private UnboundedInt(BigInteger value)
		{
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
			return this.value.ToByteArray(isUnsigned: false, isBigEndian: isBigEndian);
		}

		/// <summary>
		/// Tries to get the Int64 representation of the value, will not if that value is too large to fit
		/// into a Int64.
		/// </summary>
		/// <param name="value">Out parameter that is set ONLY if the return value is true</param>
		/// <returns>True if converted, otherwise false</returns>
		public bool TryToInt64(out long value)
		{

			if (this.value <= long.MaxValue)
			{
				value = (long)this.value;
				return true;
			}
			value = 0;
			return false;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.value.ToString();
		}

		/// <summary>
		/// A helper method to convert a Int64 to a unbounded int
		/// </summary>
		/// <param name="value">A Int64 value</param>
		/// <returns>An unbounded int</returns>
		public static UnboundedInt FromInt64(long value)
		{
			return new UnboundedInt(new BigInteger(value));
		}

		/// <summary>
		/// Converts a big integer to an unbounded int
		/// </summary>
		/// <param name="value">Big integer to convert</param>
		/// <returns>An unbounded int</returns>
		public static UnboundedInt FromBigInteger(BigInteger value)
		{
			return new UnboundedInt(value);
		}

		/// <summary>
		/// Converts a unbounded int to a big integer value
		/// </summary>
		/// <returns>A big integer</returns>
		public BigInteger ToBigInteger()
		{
			return this.value;
		}

		/// <inheritdoc />
		public bool Equals(UnboundedInt? uuint)
		{
			return this.CompareTo(uuint) == 0;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return this.Equals(obj as UnboundedInt);
		}

		/// <inheritdoc />
		public int CompareTo(UnboundedInt? other)
		{
			if (ReferenceEquals(other, null))
			{
				return 1;
			}
			return this.value.CompareTo(other.value);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <inheritdoc />
		public static bool operator ==(UnboundedInt? v1, UnboundedInt? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		/// <inheritdoc />
		public static bool operator !=(UnboundedInt? v1, UnboundedInt? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}

		/// <inheritdoc />
		public static UnboundedInt operator +(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value + b.value);
		}

		/// <inheritdoc />
		public static UnboundedInt operator -(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value - b.value);
		}

		/// <inheritdoc />
		public static UnboundedInt operator *(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value * b.value);
		}

		/// <inheritdoc />
		public static UnboundedInt operator /(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value / b.value);
		}

		/// <inheritdoc />
		public static bool operator <(UnboundedInt a, UnboundedInt b)
		{
			return a.value < b.value;
		}

		/// <inheritdoc />
		public static bool operator >(UnboundedInt a, UnboundedInt b)
		{
			return a.value > b.value;
		}

		/// <inheritdoc />
		public static bool operator <=(UnboundedInt a, UnboundedInt b)
		{
			return a.value <= b.value;
		}

		/// <inheritdoc />
		public static bool operator >=(UnboundedInt a, UnboundedInt b)
		{
			return a.value >= b.value;
		}

		/// <inheritdoc />
		public static UnboundedInt operator ++(UnboundedInt a)
		{
			return new UnboundedInt(a.value + 1);
		}

		/// <inheritdoc />
		public static UnboundedInt operator --(UnboundedInt a)
		{
			return new UnboundedInt(a.value - 1);
		}

		/// <summary>
		/// A helper method to implicitly convert a unbounded uint to an unbounded int
		/// </summary>
		/// <param name="value">An unbounded uint</param>
		public static implicit operator UnboundedInt(UnboundedUInt value)
		{
			return FromBigInteger(value.ToBigInteger());
		}


		/// <summary>
		/// A helper method to implicitly convert a Int64 to an unbounded int
		/// </summary>
		/// <param name="value">An Int64 value</param>
		public static implicit operator UnboundedInt(long value)
		{
			return FromInt64(value);
		}

		/// <summary>
		/// A helper method to implicitly convert a Int32 to an unbounded int
		/// </summary>
		/// <param name="value">An Int32 value</param>
		public static implicit operator UnboundedInt(int value)
		{
			return FromInt64(value);
		}

		/// <summary>
		/// A helper method to implicitly convert a Int16 to an unbounded int
		/// </summary>
		/// <param name="value">An Int16 value</param>
		public static implicit operator UnboundedInt(short value)
		{
			return FromInt64(value);
		}

		/// <summary>
		/// A helper method to implicitly convert a Int8 to an unbounded int
		/// </summary>
		/// <param name="value">An Int8 value</param>
		public static implicit operator UnboundedInt(sbyte value)
		{
			return FromInt64(value);
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt64 to an unbounded int
		/// </summary>
		/// <param name="value">An UInt64 value</param>
		public static implicit operator UnboundedInt(ulong value)
		{
			return FromInt64((long)value);
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt32 to an unbounded int
		/// </summary>
		/// <param name="value">An UInt32 value</param>
		public static implicit operator UnboundedInt(uint value)
		{
			return FromInt64((int)value);
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt16 to an unbounded int
		/// </summary>
		/// <param name="value">An UInt16 value</param>
		public static implicit operator UnboundedInt(ushort value)
		{
			return FromInt64((short)value);
		}

		/// <summary>
		/// A helper method to implicitly convert a UInt8 to an unbounded int
		/// </summary>
		/// <param name="value">An UInt8 value</param>
		public static implicit operator UnboundedInt(byte value)
		{
			return FromInt64((sbyte)value);
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a UInt64
		/// </summary>
		/// <param name="value">An UInt64</param>
		public static explicit operator ulong(UnboundedInt value)
		{
			ValidateMinMax(value, ulong.MinValue, ulong.MaxValue);
			return (ulong)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a UInt32
		/// </summary>
		/// <param name="value">An UInt32</param>
		public static explicit operator uint(UnboundedInt value)
		{
			ValidateMinMax(value, uint.MinValue, uint.MaxValue);
			return (uint)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a UInt16
		/// </summary>
		/// <param name="value">An UInt16</param>
		public static explicit operator ushort(UnboundedInt value)
		{
			ValidateMinMax(value, ushort.MinValue, ushort.MaxValue);
			return (ushort)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a UInt8
		/// </summary>
		/// <param name="value">An UInt8</param>
		public static explicit operator byte(UnboundedInt value)
		{
			ValidateMinMax(value, byte.MinValue, byte.MaxValue);
			return (byte)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a Int64
		/// </summary>
		/// <param name="value">An Int64</param>
		public static explicit operator long(UnboundedInt value)
		{
			ValidateMinMax(value, long.MinValue, long.MaxValue);
			return (long)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a Int32
		/// </summary>
		/// <param name="value">An Int32</param>
		public static explicit operator int(UnboundedInt value)
		{
			ValidateMinMax(value, int.MinValue, int.MaxValue);
			return (int)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a Int16
		/// </summary>
		/// <param name="value">An Int16</param>
		public static explicit operator short(UnboundedInt value)
		{
			ValidateMinMax(value, short.MinValue, short.MaxValue);
			return (short)value.value;
		}

		/// <summary>
		/// A helper method to explicitly convert an unbounded int to a Int8
		/// </summary>
		/// <param name="value">An Int8</param>
		public static explicit operator sbyte(UnboundedInt value)
		{
			ValidateMinMax(value, sbyte.MinValue, sbyte.MaxValue);
			return (sbyte)value.value;
		}

		private static void ValidateMinMax(UnboundedInt value, BigInteger minValue, BigInteger maxValue)
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
	/// Extension methods related to unbounded ints
	/// </summary>
	public static class UnboundedIntExtensions
	{
		/// <summary>
		/// Converts a Int64 into an unbounded int
		/// </summary>
		/// <param name="value">A Int64 value</param>
		/// <returns>An unbounded int</returns>
		public static UnboundedInt ToUnbounded(this long value)
		{
			return UnboundedInt.FromInt64(value);
		}
	}
}
