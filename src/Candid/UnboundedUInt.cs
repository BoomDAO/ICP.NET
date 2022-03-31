using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candid
{
	public class UnboundedUInt
	{
		private ulong uint64Count;
		private ulong offset;
		public UnboundedUInt(ulong uint64Count, ulong offset)
		{
			this.uint64Count = uint64Count;
			this.offset = offset;
		}

		public (ulong Count, ulong Offset) GetValue()
		{
			return (this.uint64Count, this.offset);
		}

		public static UnboundedUInt FromUInt64(ulong value)
		{
			return new UnboundedUInt(0, value);
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
