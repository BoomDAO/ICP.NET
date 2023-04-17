
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace System.Formats.Cbor
{
	internal static class CborWriterExtensions
	{
		public static void WriteArray<T>(this CborWriter writer, List<T> items, Action<CborWriter, T> write)
		{
			writer.WriteStartArray(items.Count);
			foreach (T item in items)
			{
				write(writer, item);
			}
			writer.WriteEndArray();
		}

		public static void WriteHashableValue(this CborWriter writer, IHashable value)
		{
			if (value == null)
			{
				writer.WriteNull();
			}
			else if (value is HashableArray a)
			{
				writer.WriteArray(a.Items, WriteHashableValue);
			}
			else if (value is HashableBytes b)
			{
				writer.WriteByteString(b.Value);
			}
			else if (value is HashableObject obj)
			{
				writer.WriteStartMap(obj.Properties.Count);
				foreach ((string key, IHashable item) in obj.Properties)
				{
					writer.WriteTextString(key);
					writer.WriteHashableValue(item);
				}
				writer.WriteEndMap();
			}
			else if (value is HashableString s)
			{
				writer.WriteTextString(s.Value);
			}
			else if (value is Delegation d)
			{
				writer.WriteDelegation(d);
			}
			else if (value is SignedDelegation sd)
			{
				writer.WriteStartMap(2);

				writer.WriteTextString(SignedDelegation.Properties.DELEGATION);
				writer.WriteDelegation(sd.Delegation);

				writer.WriteTextString(SignedDelegation.Properties.SIGNATURE);
				writer.WriteByteString(sd.Signature);

				writer.WriteEndMap();
			}
			else if (value is ICTimestamp t)
			{
				writer.WriteTimestamp(t);
			}
			else if (value is CandidArg arg)
			{
				writer.WriteByteString(arg.Encode());
			}
			else if (value is Principal p)
			{
				writer.WritePrincipal(p);
			}
			else if (value is RequestId ri)
			{
				writer.WriteByteString(ri.RawValue);
			}
			else if (value is StatePath sp)
			{
				writer.WriteStatePath(sp);
			}
			else if (value is StatePathSegment sps)
			{
				writer.WriteByteString(sps.Value);
			}
			else
			{
				throw new NotImplementedException($"Cbor serializer for IHashable type {value.GetType()} is not implemented");
			}
		}

		public static void WriteStatePath(this CborWriter writer, StatePath statePath)
		{
			writer.WriteStartArray(statePath.Segments.Count);
			foreach (StatePathSegment segment in statePath.Segments)
			{
				writer.WriteByteString(segment.Value);
			}
			writer.WriteEndArray();
		}

		public static void WriteDelegation(this CborWriter writer, Delegation d)
		{
			writer.WriteStartMap(null);

			writer.WriteTextString(Delegation.Properties.PUBLIC_KEY);
			writer.WriteByteString(d.PublicKey.ToDerEncoding());

			writer.WriteTextString(Delegation.Properties.EXPIRATION);
			writer.WriteHashableValue(d.Expiration);

			if (d.Senders?.Any() == true)
			{
				writer.WriteTextString(Delegation.Properties.SENDERS);
				writer.WriteArray(d.Senders, WritePrincipal);
			}

			if (d.Targets?.Any() == true)
			{
				writer.WriteTextString(Delegation.Properties.TARGETS);
				writer.WriteArray(d.Targets, WritePrincipal);
			}

			writer.WriteEndMap();
		}

		public static void WritePrincipal(this CborWriter writer, Principal value)
		{
			writer.WriteByteString(value.Raw);
		}


		public static void WriteTimestamp(this CborWriter writer, ICTimestamp value)
		{
			if (value.NanoSeconds.TryToUInt64(out ulong longValue))
			{
				writer.WriteUInt64(longValue);
			}
			else
			{
				writer.WriteTag(CborTag.UnsignedBigNum);
				byte[] raw = value.NanoSeconds.GetRawBytes(isBigEndian: true);
				byte[] lengthBytes = new BigInteger(raw.Length)
					.ToByteArray(isUnsigned: true, isBigEndian: true);
				writer.WriteByteString(lengthBytes);
				writer.WriteByteString(raw);
			}
		}
	}
}
