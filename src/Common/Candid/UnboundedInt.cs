using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Common.Candid
{
	public class UnboundedInt : IComparable<UnboundedInt>
	{
		private BigInteger value;

		public UnboundedInt(BigInteger value)
		{
			this.value = value;
		}

		public byte[] GetRawBytes()
		{
			return this.value.ToByteArray(isUnsigned: false, isBigEndian: true);
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

		public bool Equals(UnboundedInt uuint)
		{
			return this.value == uuint.value;
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
	}

	public static class UnboundedIntExtensions
	{
		public static UnboundedInt ToUnbounded(this long v)
		{
			return UnboundedInt.FromInt64(v);
		}
	}
}
