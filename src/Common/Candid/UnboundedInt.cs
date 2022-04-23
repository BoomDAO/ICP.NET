using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Common.Candid
{
	public class UnboundedInt : IComparable<UnboundedInt>, IEquatable<UnboundedInt>
	{
		private BigInteger value;

		public UnboundedInt(BigInteger value)
		{
			this.value = value;
		}

		public byte[] GetRawBytes(bool isBigEndian)
		{
			return this.value.ToByteArray(isUnsigned: false, isBigEndian: isBigEndian);
		}

		public bool TryToUInt64(out long value)
		{
			if (this.value <= long.MaxValue)
			{
				value = (long)this.value;
				return true;
			}
			value = 0;
			return false;
		}

		public bool Equals(UnboundedInt? uuint)
		{
			return this.CompareTo(uuint) == 0;
		}

		public static UnboundedInt FromBigInteger(BigInteger value)
		{
			return new UnboundedInt(value);
		}

		public BigInteger ToBigInteger()
		{
			return this.value;
		}

		public override bool Equals(object? obj)
		{
			return this.value.Equals(obj);
		}


		public int CompareTo(UnboundedInt? other)
		{
			return this.value.CompareTo(other);
		}

		public static UnboundedInt FromInt64(long value)
		{
			return new UnboundedInt(new BigInteger(value));
		}

		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		public static UnboundedInt operator +(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value + b.value);
		}

		public static UnboundedInt operator -(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value - b.value);
		}

		public static UnboundedInt operator *(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value * b.value);
		}

		public static UnboundedInt operator /(UnboundedInt a, UnboundedInt b)
		{
			return new UnboundedInt(a.value / b.value);
		}

		public static bool operator <(UnboundedInt a, UnboundedInt b)
		{
			return a.value < b.value;
		}

		public static bool operator >(UnboundedInt a, UnboundedInt b)
		{
			return a.value > b.value;
		}

		public static bool operator <=(UnboundedInt a, UnboundedInt b)
		{
			return a.value <= b.value;
		}

		public static bool operator >=(UnboundedInt a, UnboundedInt b)
		{
			return a.value >= b.value;
		}

		public static UnboundedInt operator ++(UnboundedInt a)
		{
			return new UnboundedInt(a.value++);
		}

		public static UnboundedInt operator --(UnboundedInt a)
		{
			return new UnboundedInt(a.value--);
		}

		public static implicit operator UnboundedInt(long value)
		{
			return UnboundedInt.FromInt64(value);
		}

		public static implicit operator UnboundedInt(int value)
		{
			return UnboundedInt.FromInt64(value);
		}

		public static implicit operator UnboundedInt(short value)
		{
			return UnboundedInt.FromInt64(value);
		}

		public static implicit operator UnboundedInt(sbyte value)
		{
			return UnboundedInt.FromInt64(value);
		}
		public static implicit operator UnboundedInt(ulong value)
		{
			return UnboundedInt.FromInt64((long)value);
		}

		public static implicit operator UnboundedInt(uint value)
		{
			return UnboundedInt.FromInt64((int)value);
		}

		public static implicit operator UnboundedInt(ushort value)
		{
			return UnboundedInt.FromInt64((short)value);
		}

		public static implicit operator UnboundedInt(byte value)
		{
			return UnboundedInt.FromInt64((sbyte)value);
		}





		public static explicit operator ulong(UnboundedInt value)
		{
			ValidateMinMax(value, ulong.MinValue, ulong.MaxValue);
			return (ulong)value.value;
		}

		public static explicit operator uint(UnboundedInt value)
		{
			ValidateMinMax(value, uint.MinValue, uint.MaxValue);
			return (uint)value.value;
		}

		public static explicit operator ushort(UnboundedInt value)
		{
			ValidateMinMax(value, ushort.MinValue, ushort.MaxValue);
			return (ushort)value.value;
		}

		public static explicit operator byte(UnboundedInt value)
		{
			ValidateMinMax(value, byte.MinValue, byte.MaxValue);
			return (byte)value.value;
		}

		public static explicit operator long(UnboundedInt value)
		{
			ValidateMinMax(value, long.MinValue, long.MaxValue);
			return (long)value.value;
		}

		public static explicit operator int(UnboundedInt value)
		{
			ValidateMinMax(value, int.MinValue, int.MaxValue);
			return (int)value.value;
		}

		public static explicit operator short(UnboundedInt value)
		{
			ValidateMinMax(value, short.MinValue, short.MaxValue);
			return (short)value.value;
		}

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
			if (value.value < maxValue)
			{
				throw new InvalidCastException("Value is too small");
			}
		}
	}

	public static class UnboundedIntExtensions
	{
		public static UnboundedInt ToUnbounded(this long v)
		{
			return UnboundedInt.FromInt64(v);
		}
	}
}
