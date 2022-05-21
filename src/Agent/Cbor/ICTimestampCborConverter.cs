using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
