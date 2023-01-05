using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid
{
	public class UnboundedUInt : IComparable<UnboundedUInt>, IEquatable<UnboundedUInt>
	{
		private BigInteger value;

		public UnboundedUInt(BigInteger value)
		{
			if (value < 0)
			{
				throw new ArgumentException("Value must be 0 or greater");
			}
			this.value = value;
		}

		public byte[] GetRawBytes(bool isBigEndian)
		{
			return NumericUtil.ToByteArray(this.value, unsignedBits: true, bigEndian: isBigEndian);
		}

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

		public static UnboundedUInt FromBigInteger(BigInteger value)
		{
			return new UnboundedUInt(value);
		}

		public BigInteger ToBigInteger()
		{
			return this.value;
		}

		public override string ToString()
		{
			return this.value.ToString();
		}

		public bool Equals(UnboundedUInt? uuint)
		{
			return this.CompareTo(uuint) == 0;
		}

		public override bool Equals(object? obj)
		{
			return this.Equals(obj as UnboundedUInt);
		}


		public int CompareTo(UnboundedUInt? other)
		{
			if (object.ReferenceEquals(other, null))
			{
				return 1;
			}
			return this.value.CompareTo(other.value);
		}

		public static UnboundedUInt FromUInt64(ulong value)
		{
			return new UnboundedUInt(new BigInteger(value));
		}

		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}


		public static bool operator ==(UnboundedUInt? v1, UnboundedUInt? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		public static bool operator !=(UnboundedUInt? v1, UnboundedUInt? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}

		public static UnboundedUInt operator +(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value + b.value);
		}

		public static UnboundedUInt operator -(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value - b.value);
		}

		public static UnboundedUInt operator *(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value * b.value);
		}

		public static UnboundedUInt operator /(UnboundedUInt a, UnboundedUInt b)
		{
			return new UnboundedUInt(a.value / b.value);
		}

		public static bool operator <(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value < b.value;
		}

		public static bool operator >(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value > b.value;
		}

		public static bool operator <=(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value <= b.value;
		}

		public static bool operator >=(UnboundedUInt a, UnboundedUInt b)
		{
			return a.value >= b.value;
		}

		public static UnboundedUInt operator ++(UnboundedUInt a)
		{
			return new UnboundedUInt(a.value + 1);
		}

		public static UnboundedUInt operator --(UnboundedUInt a)
		{
			return new UnboundedUInt(a.value - 1);
		}


		public static implicit operator UnboundedUInt(UnboundedInt value)
		{
			if (value < 0)
			{
				throw new InvalidCastException("Value must be 0 or greater");
			}
			return UnboundedUInt.FromBigInteger(value.ToBigInteger());
		}



		public static implicit operator UnboundedUInt(ulong value)
		{
			return UnboundedUInt.FromUInt64(value);
		}

		public static implicit operator UnboundedUInt(uint value)
		{
			return UnboundedUInt.FromUInt64(value);
		}

		public static implicit operator UnboundedUInt(ushort value)
		{
			return UnboundedUInt.FromUInt64(value);
		}

		public static implicit operator UnboundedUInt(byte value)
		{
			return UnboundedUInt.FromUInt64(value);
		}

		public static explicit operator UnboundedUInt(long value)
		{
			return UnboundedUInt.FromUInt64((ulong)value);
		}

		public static explicit operator UnboundedUInt(int value)
		{
			return UnboundedUInt.FromUInt64((uint)value);
		}

		public static explicit operator UnboundedUInt(short value)
		{
			return UnboundedUInt.FromUInt64((ushort)value);
		}

		public static explicit operator UnboundedUInt(sbyte value)
		{
			return UnboundedUInt.FromUInt64((byte)value);
		}



		public static explicit operator ulong(UnboundedUInt value)
		{
			ValidateMinMax(value, ulong.MinValue, ulong.MaxValue);
			return (ulong)value.value;
		}

		public static explicit operator uint(UnboundedUInt value)
		{
			ValidateMinMax(value, uint.MinValue, uint.MaxValue);
			return (uint)value.value;
		}

		public static explicit operator ushort(UnboundedUInt value)
		{
			ValidateMinMax(value, ushort.MinValue, ushort.MaxValue);
			return (ushort)value.value;
		}

		public static explicit operator byte(UnboundedUInt value)
		{
			ValidateMinMax(value, byte.MinValue, byte.MaxValue);
			return (byte)value.value;
		}

		public static explicit operator long(UnboundedUInt value)
		{
			ValidateMinMax(value, long.MinValue, long.MaxValue);
			return (long)value.value;
		}

		public static explicit operator int(UnboundedUInt value)
		{
			ValidateMinMax(value, int.MinValue, int.MaxValue);
			return (int)value.value;
		}

		public static explicit operator short(UnboundedUInt value)
		{
			ValidateMinMax(value, short.MinValue, short.MaxValue);
			return (short)value.value;
		}

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

	public static class UnboundedUIntExtensions
	{
		public static UnboundedUInt ToUnbounded(this ulong v)
		{
			return UnboundedUInt.FromUInt64(v);
		}
	}
}
