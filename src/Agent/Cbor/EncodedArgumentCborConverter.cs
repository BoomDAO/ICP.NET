using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using ICP.Agent;
using ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	internal class EncodedArgumentCborConverter : CborConverterBase<CandidArg?>
	{
		public override CandidArg? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			ReadOnlySpan<byte> bytes = reader.ReadByteString();

			return CandidArg.FromBytes(bytes.ToArray());
		}

		public override void Write(ref CborWriter writer, CandidArg? value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteByteString(value.Encode());
		}
	}
}
