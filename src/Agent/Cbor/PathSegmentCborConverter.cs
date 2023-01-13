using Dahomey.Cbor.Serialization.Converters;
using Dahomey.Cbor.Serialization;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent.Cbor
{
	internal class PathCborConverter : CborConverterBase<Path?>
	{
		public override Path? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			List<PathSegment> value = CborReaderUtil.ReadArray(ref reader, this.GetArrayValue);
			return new Path(value);
		}

		private PathSegment GetArrayValue(ref CborReader reader)
		{
			return new PathSegment(reader.ReadByteString().ToArray());
		}

		public override void Write(ref CborWriter writer, Path? value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteBeginArray(value.Segments.Count);
			foreach (PathSegment segment in value.Segments)
			{
				writer.WriteByteString(segment.Value);
			}
		}
	}
	internal class PathSegmentCborConverter : CborConverterBase<PathSegment?>
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
