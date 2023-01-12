using Newtonsoft.Json;
using System;
using System.Numerics;
using EdjCase.ICP.Candid;

namespace EdjCase.ICP.Serialization.Converters
{
	public class UnboundedUIntJsonConverter : JsonConverter<UnboundedUInt>
	{
		public UnboundedUIntJsonConverter()
		{
		}

		public override void WriteJson(JsonWriter writer, UnboundedUInt? value, JsonSerializer serializer)
		{
			writer.WriteValue(value.ToBigInteger().ToByteArray());
		}

		static byte[] JsonValueToBytes(object v)
		{
			switch (v)
			{
				case byte[] bytes:
					return bytes;

				case string str:
					return Convert.FromBase64String(str);

				default:
					throw new InvalidOperationException($"Type {v.GetType()} is not convertible to bytes");
			}
		}

		public override UnboundedUInt? ReadJson(JsonReader reader, Type objectType, UnboundedUInt? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			return new UnboundedUInt(new BigInteger(JsonValueToBytes(reader.Value)));
		}
	}
}
