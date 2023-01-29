using Agent.Cbor;
using Dahomey.Cbor;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;
using System;
using System.Numerics;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Agent.Cbor.Converters
{
	internal class ICTimestampCborConverter : CborConverterBase<ICTimestamp>
	{
		public override ICTimestamp Read(ref CborReader reader)
		{
			UnboundedUInt value;
			if (reader.TryReadSemanticTag(out ulong semanticTag))
			{
				if (semanticTag != 2)
				{
					throw new CborException("Unable to read value as Unbounded UInt");
				}
				ReadOnlySpan<byte> bytes = reader.ReadByteString();
				value = UnboundedUInt.FromBigInteger(new BigInteger(bytes.ToArray()));
				
			}
			else
			{
				ulong ulongValue = reader.ReadUInt64();
				value = UnboundedUInt.FromBigInteger(new BigInteger(ulongValue));
			}
			return new ICTimestamp(value);
		}

		public override void Write(ref CborWriter writer, ICTimestamp value)
		{
			if (value.NanoSeconds.TryToUInt64(out ulong longValue))
			{
				writer.WriteUInt64(longValue);
			}
			else
			{
				writer.WriteSemanticTag(2);
				byte[] raw = value.NanoSeconds.GetRawBytes(isBigEndian: true);
				byte[] lengthBytes = new BigInteger(raw.Length)
					.ToByteArray(isUnsigned: true, isBigEndian: true);
				writer.WriteByteString(lengthBytes);
				writer.WriteByteString(raw);
			}
		}
	}
}
