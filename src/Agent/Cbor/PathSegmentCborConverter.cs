using Dahomey.Cbor.Serialization.Converters;
using Dahomey.Cbor.Serialization;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Cbor
{
	internal class PathCborConverter : CborConverterBase<StatePath?>
	{
		public override StatePath? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			List<StatePathSegment> value = CborReaderUtil.ReadArray(ref reader, this.GetArrayValue);
			return new StatePath(value);
		}

		private StatePathSegment GetArrayValue(ref CborReader reader)
		{
			return new StatePathSegment(reader.ReadByteString().ToArray());
		}

		public override void Write(ref CborWriter writer, StatePath? value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteBeginArray(value.Segments.Count);
			foreach (StatePathSegment segment in value.Segments)
			{
				writer.WriteByteString(segment.Value);
			}
		}
	}
	internal class PathSegmentCborConverter : CborConverterBase<StatePathSegment?>
	{
		public override StatePathSegment? Read(ref CborReader reader)
		{
			if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
			{
				return null;
			}
			ReadOnlySpan<byte> value = reader.ReadByteString();
			return new StatePathSegment(value.ToArray());
		}

		public override void Write(ref CborWriter writer, StatePathSegment? value)
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
