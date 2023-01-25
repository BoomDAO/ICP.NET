using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Cbor.Converters
{
	internal class HashableStringCborConverter : CborConverterBase<HashableString>
	{
		public override HashableString Read(ref CborReader reader)
		{
			string? value = reader.ReadString();
			return new HashableString(value ?? "");
		}
		public override void Write(ref CborWriter writer, HashableString value)
		{
			writer.WriteString(value.Value);
		}
	}
}
