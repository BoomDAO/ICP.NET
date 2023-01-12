using EdjCase.ICP.Serialization.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using EdjCase.ICP.Candid.Utilities;
using System;

namespace EdjCase.ICP.Serialization
{
	public enum SerializedFormat
	{
		Json, Bson, BsonHex, BsonB64
	}


	public static class SerializationUtil
	{
		// This is the best we have with a lack of result types/discriminated unions and exceptions
		// being used to simulate those.
		public static T FirstOf<T>(IEnumerable<System.Func<T>> producers, string? rootMessage)
		{
			var exceptions = new List<System.Exception>();

			var it = producers.GetEnumerator();
			bool more = true;
			while (more)
			{
				try
				{
					if (more = it.MoveNext())
					{
						return it.Current();
					}
				}
				catch (System.Exception e)
				{
					exceptions.Add(e);
				}
			}

			throw rootMessage != null ? new System.AggregateException(rootMessage, exceptions) : new System.AggregateException(exceptions);
		}

		public static JsonSerializer MakeJsonSerializer(JsonSerializer? serializer = null)
		{
			serializer = serializer ?? new JsonSerializer();
			serializer.TypeNameHandling = TypeNameHandling.All;
			serializer.Converters.Add(new UnboundedUIntJsonConverter());
			return serializer;
		}

		public static JsonSerializer DefaultSerializer => MakeJsonSerializer();


		public static T? DeserializeJson<T>(JsonSerializer jsonSerializer, string? raw) where T : class
		{
			if (raw == null) return null;
			var tr = new StringReader(raw);
			using (var reader = new JsonTextReader(tr))
			{
				return jsonSerializer.Deserialize<T>(reader);
			}
		}

		public static T? DeserializeBson<T>(JsonSerializer jsonSerializer, byte[]? raw) where T : class
		{
			if (raw == null) return null;
			var ms = new System.IO.MemoryStream(raw);
			using (var reader = new BsonDataReader(ms))
			{
				return jsonSerializer.Deserialize<T>(reader);
			}
		}

		public static T? DeserializeBsonHex<T>(JsonSerializer jsonSerializer, string? raw) where T : class
		{
			if (raw == null) return null;
			return DeserializeBson<T>(jsonSerializer, ByteUtil.FromHexString(raw));
		}

		public static T? DeserializeBsonB64<T>(JsonSerializer jsonSerializer, string? raw) where T : class
		{
			if (raw == null) return null;
			return DeserializeBson<T>(jsonSerializer, Convert.FromBase64String(raw));
		}

		public static byte[] SerializeBson<T>(JsonSerializer jsonSerializer, T obj) where T : class
		{
			var ms = new System.IO.MemoryStream();
			using (var writer = new BsonDataWriter(ms))
			{
				jsonSerializer.Serialize(writer, obj);
			}
			return ms.ToArray();
		}

		public static string SerializeJson<T>(JsonSerializer jsonSerializer, T obj) where T : class
		{
			var sb = new System.Text.StringBuilder();
			var sw = new StringWriter(sb);
			using (var writer = new JsonTextWriter(sw))
			{
				jsonSerializer.Serialize(writer, obj);
			}
			return sb.ToString();
		}

		public static byte[] Serialize<T>(JsonSerializer jsonSerializer, T obj, SerializedFormat fmt) where T : class
		{
			switch (fmt)
			{
				case SerializedFormat.Bson:
					return SerializeBson(jsonSerializer, obj);

				case SerializedFormat.BsonHex:
					return System.Text.Encoding.UTF8.GetBytes(ByteUtil.ToHexString(SerializeBson(jsonSerializer, obj)));

				case SerializedFormat.BsonB64:
					return System.Text.Encoding.UTF8.GetBytes(Convert.ToBase64String(SerializeBson(jsonSerializer, obj)));

				case SerializedFormat.Json:
				default:
					return System.Text.Encoding.UTF8.GetBytes(SerializeJson(jsonSerializer, obj));
			}
		}

		public static T? Deserialize<T>(JsonSerializer jsonSerializer, byte[]? rawBytes) where T : class
		{
			if (rawBytes == null) return null;

			IEnumerable<System.Func<T?>> Cands()
			{
				yield return () => DeserializeBson<T>(jsonSerializer, rawBytes);

				string? rawText = null;
				try { rawText = System.Text.Encoding.UTF8.GetString(rawBytes); } catch { };
				if (rawText != null)
				{
					yield return () => DeserializeJson<T>(jsonSerializer, rawText);
					yield return () => DeserializeBsonHex<T>(jsonSerializer, rawText);
					yield return () => DeserializeBsonB64<T>(jsonSerializer, rawText);
				}
			}

			return FirstOf(Cands(), null);
		}
	}
}
