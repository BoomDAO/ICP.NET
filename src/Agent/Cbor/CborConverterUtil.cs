using Candid;
using Dahomey.Cbor;
using Dahomey.Cbor.Serialization;
using ICP.Common.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
				byte[] raw = value.GetRawBytes();
				byte[] lengthBytes = new BigInteger(raw.Length).ToByteArray(isUnsigned: true, isBigEndian: true);
				writer.WriteByteString(lengthBytes);
				writer.WriteByteString(raw);
			}
		}

		public static UnboundedUInt ReadUnboundedUInt(ref CborReader reader)
		{
			if (reader.TryReadSemanticTag(out ulong semanticTag))
			{
				if (semanticTag == 2)
				{
					ReadOnlySpan<byte> bytes = reader.ReadByteString();
					return new UnboundedUInt(new BigInteger(bytes.ToArray()));
				}
			}
			throw new CborException("Unable to read value as Unbounded UInt");
		}
	}
}
