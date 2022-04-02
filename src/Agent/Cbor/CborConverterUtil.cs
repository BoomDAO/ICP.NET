using Candid;
using Dahomey.Cbor.Serialization;
using ICP.Common.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	public static class CborConverterUtil
	{
		public static void Write(ref CborWriter writer, UnboundedUInt value)
		{
			if (value.TryToUInt64(out ulong longValue))
			{
				writer.WriteUInt64(longValue);
			}
			else
			{
				writer.WriteSemanticTag(2);
				writer.WriteByteString(leb.Raw.Length);
				writer.WriteByteString(leb.Raw);
			}
		}

		public static UnboundedUInt ReadUnboundedUInt(ref CborReader reader)
		{
			if (reader.TryReadSemanticTag(out ulong semanticTag))
			{
				if (semanticTag != 2)
				{

				}
			}
		}
	}
}
