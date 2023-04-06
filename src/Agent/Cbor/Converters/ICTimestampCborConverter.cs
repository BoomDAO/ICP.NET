
//using EdjCase.ICP.Candid.Models;
//using System;
//using System.Numerics;

//namespace EdjCase.ICP.Agent.Cbor.Converters
//{
//	internal class ICTimestampCborConverter : CborConverterBase<ICTimestamp>
//	{
//		public override ICTimestamp Read(ref CborReader reader)
//		{
//			UnboundedUInt value;
//			if (reader.TryReadSemanticTag(out ulong semanticTag))
//			{
//				if (semanticTag != 2)
//				{
//					throw new CborException("Unable to read value as Unbounded UInt");
//				}
//				ReadOnlySpan<byte> bytes = reader.ReadByteString();
//				value = UnboundedUInt.FromBigInteger(new BigInteger(bytes.ToArray()));
				
//			}
//			else
//			{
//				ulong ulongValue = reader.ReadUInt64();
//				value = UnboundedUInt.FromBigInteger(new BigInteger(ulongValue));
//			}
//			return new ICTimestamp(value);
//		}

//		public override void Write(ref CborWriter writer, ICTimestamp value)
//		{
			
//		}
//	}
//}
