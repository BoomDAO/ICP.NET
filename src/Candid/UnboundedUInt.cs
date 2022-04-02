using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candid
{
	public class UnboundedUInt : IComparable<UnboundedUInt>
	{
		/// <summary>
		/// How many overflows of `offset` or the ulong.MaxValue count to add to the whole
		/// </summary>
		private ulong offsetOverflowCount;

		/// <summary>
		/// The raw value to add to the whole
		/// </summary>
		private ulong offset;
		public UnboundedUInt(ulong uint64Count, ulong offset)
		{
			this.offsetOverflowCount = uint64Count;
			this.offset = offset;
		}

		public (ulong Count, ulong Offset) GetValue()
		{
			return (this.offsetOverflowCount, this.offset);
		}

		public bool TryToUInt64(out ulong value)
		{
			if (this.offsetOverflowCount == 0)
			{
				value = offset;
				return true;
			}
			value = 0;
			return false;
		}

		public bool Equals(UnboundedUInt uuint)
		{
			return this.CompareTo(uuint) == 0;
		}

		public override bool Equals(object? obj)
		{
			if (obj is UnboundedUInt uuint)
			{
				return this.Equals(uuint);
			}
			return false;
		}


		public int CompareTo(UnboundedUInt? other)
		{
			if (other == null)
			{
				return 1;
			}
			if (this.offsetOverflowCount > other.offsetOverflowCount)
			{
				return 1;
			}
			if (this.offsetOverflowCount == other.offsetOverflowCount)
			{
				return this.offset.CompareTo(other.offset);
			}
			return -1;
		}

		public static UnboundedUInt FromUInt64(ulong value)
		{
			return new UnboundedUInt(0, value);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(offsetOverflowCount, offset);
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
