//using EdjCase.ICP.Candid.Models;
//using System;
//using System.Collections.Generic;

//namespace EdjCase.ICP.Agent.Cbor.Converters
//{
//	internal class PathCborConverter : CborConverterBase<StatePath>
//	{
//		public override StatePath Read(ref CborReader reader)
//		{
//			List<StatePathSegment> value = CborUtil.ReadArray(ref reader, this.GetArrayValue);
//			return new StatePath(value);
//		}

//		private StatePathSegment GetArrayValue(ref CborReader reader)
//		{
//			return new StatePathSegment(reader.ReadByteString().ToArray());
//		}

//		public override void Write(ref CborWriter writer, StatePath value)
//		{
//			writer.WriteBeginArray(value.Segments.Count);
//			foreach (StatePathSegment segment in value.Segments)
//			{
//				writer.WriteByteString(segment.Value);
//			}
//		}
//	}
//	internal class PathSegmentCborConverter : CborConverterBase<StatePathSegment>
//	{
//		public override StatePathSegment Read(ref CborReader reader)
//		{
//			ReadOnlySpan<byte> value = reader.ReadByteString();
//			return new StatePathSegment(value.ToArray());
//		}

//		public override void Write(ref CborWriter writer, StatePathSegment value)
//		{
//			writer.WriteByteString(value.Value);
//		}
//	}
//}
