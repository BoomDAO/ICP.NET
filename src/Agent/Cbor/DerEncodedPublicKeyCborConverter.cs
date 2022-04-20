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
	internal class DerEncodedPublicKeyCborConverter : CborConverterBase<DerEncodedPublicKey?>
	{
		public override DerEncodedPublicKey? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			ReadOnlySpan<byte> bytes = reader.ReadByteString();

			return new DerEncodedPublicKey(bytes.ToArray());
		}

		public override void Write(ref CborWriter writer, DerEncodedPublicKey? value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteByteString(value.Value);
		}
	}
}
