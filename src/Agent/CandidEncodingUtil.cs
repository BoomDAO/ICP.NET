using Candid;
using Candid.Constants;
using ICP.Common.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent
{
	public static class CandidEncodingUtil
	{
		public static byte[] EncodeType(CandidToken token)
		{
			return token.Type switch
			{
				CandidTokenType.Primitive => EncodePrimitiveType(token.AsPrimitive()),
				CandidTokenType.Vector => EncodeVectorType(token.AsVector()),
				CandidTokenType.Record => EncodeRecordType(token.AsRecord()),
				CandidTokenType.Variant => throw new NotImplementedException(),
				CandidTokenType.Func => throw new NotImplementedException(),
				CandidTokenType.Service => throw new NotImplementedException(),
				CandidTokenType.Reserved => throw new NotImplementedException(),
				CandidTokenType.Empty => throw new NotImplementedException(),
				CandidTokenType.Null => throw new NotImplementedException(),
				CandidTokenType.Optional => throw new NotImplementedException(),
				_ => throw new NotImplementedException(),
			};
		}

		private static object EncodeRecordType(CandidRecord candidRecord)
		{
			throw new NotImplementedException();
		}

		private static byte[] EncodeVectorType(CandidVector vector)
		{
			byte[] bytes = SLEB128.FromInt64((long)IDLType.Vector).Raw;
			return bytes
				.Concat(EncodeType(vector.Values));
		}



		public static byte[] EncodeValue(CandidToken token)
		{
			return token.Type switch
			{
				CandidTokenType.Primitive => SerializePrimitive(token.AsPrimitive()),
				CandidTokenType.Vector => SerializeVector(token.AsVector()),
				CandidTokenType.Record => SerializeRecord(token.AsRecord()),
				CandidTokenType.Variant => SerializeVariant(token.AsVariant()),
				CandidTokenType.Func => SerializeFunc(token.AsFunc()),
				CandidTokenType.Service => SerializeService(token.AsService()),
				CandidTokenType.Reserved => throw new NotImplementedException(),
				CandidTokenType.Empty => throw new NotImplementedException(),
				CandidTokenType.Null => throw new NotImplementedException(),
				CandidTokenType.Optional => SerializeOptional(token.AsOptional()),
				_ => throw new NotImplementedException(),
			};
		}

		private static byte[] SerializeOptional(CandidOptional candidOptional)
		{
			if (candidOptional.Value == null)
			{
				return LEB128.FromUInt64(0).Raw;
			}
			byte[] valueBytes = Serialize(candidOptional.Value);
			return LEB128.FromUInt64(1).Raw
				.Concat(valueBytes)
				.ToArray();
		}

		private static byte[] SerializeService(CandidService service)
		{
			return SerializePrincipal(service.PrincipalId);
		}

		private static byte[] SerializeFunc(CandidFunc func)
		{
			return new byte[] { 1 }
				.Concat(UnboundedBytes(func.CanisterId))
				.Concat(SerializeText(func.Name))
				.ToArray();

		}

		private static byte[] SerializeVariant(CandidVariant variant)
		{
			return LEB128.FromUInt64(variant.Tag).Raw
				.Concat(Serialize(variant.Value))
				.ToArray();
		}

		private static byte[] SerializeRecord(CandidRecord record)
		{
			return record.GetFields()
				   .Select(o =>
				   {
					   // TODO which hash for key, crc32 or IHashFunction?
					   byte[] keyBytes = BitConverter.GetBytes(o.Key);
					   byte[] valueBytes = Serialize(o.Value);

					   return (Key: keyBytes, Value: valueBytes);
				   }) // Hash key and value bytes
				   .Where(o => o.Value == null) // Remove empty/null ones
				   .OrderBy(o => o.Key) // Keys in order
				   .SelectMany(o => o.Key.Concat(o.Value))
				   .ToArray(); // Create single byte[] by concatinating them all together
		}

		private static byte[] SerializeVector(CandidVector vector)
		{
			return vector.Values
				.SelectMany(Serialize)
				.ToArray();
		}

		private static byte[] SerializePrimitive(CandidPrimitive prim)
		{
			return prim.ValueType switch
			{
				CandidPrimitiveType.Text => SerializeText(prim.AsText()),
				CandidPrimitiveType.Blob => UnboundedBytes(prim.AsBlob()),
				CandidPrimitiveType.Nat => LEB128.FromNat(prim.AsNat()).Raw,
				CandidPrimitiveType.Nat8 => new byte[] { prim.AsInt8() },
				// TODO how to enforce little endian?
				CandidPrimitiveType.Nat16 => BitConverter.GetBytes(prim.AsInt16()),
				CandidPrimitiveType.Nat32 => BitConverter.GetBytes(prim.AsInt32()),
				CandidPrimitiveType.Nat64 => BitConverter.GetBytes(prim.AsInt64()),
				CandidPrimitiveType.Int => SLEB128.FromInt(prim.AsInt()).Raw,
				CandidPrimitiveType.Int8 => BitConverter.GetBytes(prim.AsInt8()),
				CandidPrimitiveType.Int16 => BitConverter.GetBytes(prim.AsInt16()),
				CandidPrimitiveType.Int32 => BitConverter.GetBytes(prim.AsInt32()),
				CandidPrimitiveType.Int64 => BitConverter.GetBytes(prim.AsInt64()),
				CandidPrimitiveType.Float32 => BitConverter.GetBytes(prim.AsFloat32()),
				CandidPrimitiveType.Float64 => BitConverter.GetBytes(prim.AsFloat64()),
				CandidPrimitiveType.Bool => new byte[] { (byte)(prim.AsBool() ? 1 : 0) },
				CandidPrimitiveType.Principal => SerializePrincipal(prim.AsPrincipal()),
				_ => throw new NotImplementedException()
			};
		}

		private static byte[] SerializeText(string value)
		{
			return UnboundedBytes(Encoding.UTF8.GetBytes(value));
		}

		private static byte[] SerializePrincipal(byte[] value)
		{
			return new byte[] { 1 }
				.Concat(UnboundedBytes(value))
				.ToArray();
		}

		private static byte[] UnboundedBytes(byte[] value)
		{
			var length = LEB128.FromUInt64((ulong)value.Length);
			return length.Raw
				.Concat(value)
				.ToArray();
		}
	}
}
