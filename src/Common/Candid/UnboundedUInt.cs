using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Common.Candid
{
	public class UnboundedUInt : IComparable<UnboundedUInt>
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
			return this.value.ToByteArray(isUnsigned: true, isBigEndian: isBigEndian);
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

		public bool Equals(UnboundedUInt uuint)
		{
			return this.value == uuint.value;
		}

		public override bool Equals(object? obj)
		{
			return this.value.Equals(obj);
		}


		public int CompareTo(UnboundedUInt? other)
		{
			return this.value.CompareTo(other);
		}

		public static UnboundedUInt FromUInt64(ulong value)
		{
			return new UnboundedUInt(new BigInteger(value));
		}

		public override int GetHashCode()
		{
			return this.value.GetHashCode();
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
