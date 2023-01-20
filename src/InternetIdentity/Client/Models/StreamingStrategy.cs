using System;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(StreamingStrategyTag))]
	public class StreamingStrategy
	{
		[VariantTagProperty]
		public StreamingStrategyTag Tag { get; set; }
		[VariantValueProperty]
		public object? Value { get; set; }
		private StreamingStrategy(StreamingStrategyTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected StreamingStrategy()
		{
		}
		
		public class O0
		{
			[CandidName("callback")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public Candid.Models.Values.CandidFunc Callback { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			
			[CandidName("token")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public Token Token { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			
		}
		public static StreamingStrategy Callback(O0 info)
		{
			return new StreamingStrategy(StreamingStrategyTag.Callback, info);
		}
		
		public O0 AsCallback()
		{
			this.ValidateTag(StreamingStrategyTag.Callback);
			return (O0)this.Value!;
		}
		
		private void ValidateTag(StreamingStrategyTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum StreamingStrategyTag
	{
		[CandidName("Callback")]
		[VariantOptionType(typeof(StreamingStrategy.O0))]
		Callback,
	}
}

