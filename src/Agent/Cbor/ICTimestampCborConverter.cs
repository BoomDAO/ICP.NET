using Candid;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	internal class ICTimestampCborConverter : CborConverterBase<ICTimestamp>
	{
		public override ICTimestamp Read(ref CborReader reader)
		{
			UnboundedUInt value = CborConverterUtil.ReadUnboundedUInt(ref reader);
			return new ICTimestamp(value);
		}

		public override void Write(ref CborWriter writer, ICTimestamp value)
		{
			CborConverterUtil.Write(ref writer, value.NanoSeconds);
		}
	}
}
