using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;

namespace Agent.Cbor
{
	internal class ICTimestampCborConverter : CborConverterBase<ICTimestamp?>
	{
		public override ICTimestamp? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			UnboundedUInt value = CborConverterUtil.ReadUnboundedUInt(ref reader);
			return new ICTimestamp(value);
		}

		public override void Write(ref CborWriter writer, ICTimestamp? value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			CborConverterUtil.Write(ref writer, value.NanoSeconds);
		}
	}
}
