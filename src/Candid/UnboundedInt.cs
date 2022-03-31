using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candid
{
	public class UnboundedInt
	{
		private long int64Count;
		private long offset;
		public UnboundedInt(long int64Count, long offset)
		{
			this.int64Count = int64Count;
			this.offset = offset;
		}

		public (long Count, long Offset) GetValue()
		{
			return (this.int64Count, this.offset);
		}

		public static UnboundedInt FromInt64(long value)
		{
			return new UnboundedInt(0, value);
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
