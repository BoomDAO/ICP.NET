using Dahomey.Cbor.Serialization.Converters;
using Dahomey.Cbor.Serialization;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent.Cbor
{
	public class PathSegmentCborConverter : CborConverterBase<PathSegment?>
	{
		public override PathSegment? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			ReadOnlySpan<byte> value = reader.ReadByteString();
			return new PathSegment(value.ToArray());
		}

		public override void Write(ref CborWriter writer, PathSegment? value)
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
