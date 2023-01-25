using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;
using System;

namespace Agent.Cbor
{
	internal class CandidArgCborConverter : CborConverterBase<CandidArg>
	{
		public override CandidArg Read(ref CborReader reader)
		{
			ReadOnlySpan<byte> bytes = reader.ReadByteString();

			return CandidArg.FromBytes(bytes.ToArray());
		}

		public override void Write(ref CborWriter writer, CandidArg value)
		{
			writer.WriteByteString(value.Encode());
		}
	}
}
