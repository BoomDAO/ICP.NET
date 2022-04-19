using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using ICP.Agent;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	internal class SignatureCborConverter : CborConverterBase<Signature>
	{
		public override Signature Read(ref CborReader reader)
		{
			ReadOnlySpan<byte> bytes = reader.ReadByteString();

			return new Signature(bytes.ToArray());
		}

		public override void Write(ref CborWriter writer, Signature value)
		{
			writer.WriteByteString(value?.Value);
		}
	}
}
