using Dahomey.Cbor;
using Dahomey.Cbor.Serialization;
using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Utilities;

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
				byte[] raw = value.GetRawBytes(isBigEndian: true);
				byte[] lengthBytes = new BigInteger(raw.Length)
					.ToByteArray(unsignedBits: true, bigEndian: true);
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
				throw new CborException("Unable to read value as Unbounded UInt");
			}
			else
			{
				ulong value = reader.ReadUInt64();
				return new UnboundedUInt(new BigInteger(value));
			}
		}
	}
}
