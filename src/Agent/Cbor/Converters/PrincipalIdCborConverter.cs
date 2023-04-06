//using EdjCase.ICP.Candid.Encodings;
//using EdjCase.ICP.Candid.Models;
//using System;

//namespace EdjCase.ICP.Agent.Cbor.Converters
//{
//	internal class PrincipalCborConverter : CborConverterBase<Principal>
//	{
//		public override Principal Read(ref CborReader reader)
//		{
//			ReadOnlySpan<byte> raw = reader.ReadByteString();

//			if (raw[0] == 1) // first byte must be 1
//			{
//				int i = 1;
//				byte b;
//				do
//				{
//					b = raw[i];
//				}
//				while (b >= 0b1000_0000); // Last byte will not have a left most 1

//				byte[] bytes = raw.Slice(1, i).ToArray();
//				UnboundedUInt length = LEB128.DecodeUnsigned(bytes);
//				if (length.TryToUInt64(out ulong lengthLong)
//					&& lengthLong <= int.MaxValue
//					&& (int)lengthLong <= bytes.Length + i)
//				{
//					byte[] rawPrincipalId = raw
//						.Slice(i, (int)lengthLong)
//						.ToArray();
//					return Principal.FromBytes(rawPrincipalId);
//				}
//			}

//			throw new Dahomey.Cbor.CborException("Failed to deserialize PrincipalId, invalid bytes");
//		}

//		public override void Write(ref CborWriter writer, Principal value)
//		{
//		}
//	}
//}
