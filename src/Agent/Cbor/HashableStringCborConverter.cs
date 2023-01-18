using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Cbor
{
	internal class HashableStringCborConverter : CborConverterBase<HashableString?>
	{

		public override HashableString? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			string? value = reader.ReadString();
			return value == null ? null : new HashableString(value);
		}
		public override void Write(ref CborWriter writer, HashableString? value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteString(value.Value);
		}
	}
}
