using EdjCase.ICP.Candid.Mapping;

using System;
using EdjCase.ICP.Candid.Models.Values;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	[Variant]
	public class StreamingStrategy
	{
		[VariantTagProperty]
		public StreamingStrategyTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public StreamingStrategy(StreamingStrategyTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected StreamingStrategy()
		{
		}

		public static StreamingStrategy Callback(StreamingStrategy.CallbackInfo info)
		{
			return new StreamingStrategy(StreamingStrategyTag.Callback, info);
		}

		public StreamingStrategy.CallbackInfo AsCallback()
		{
			this.ValidateTag(StreamingStrategyTag.Callback);
			return (StreamingStrategy.CallbackInfo)this.Value!;
		}

		private void ValidateTag(StreamingStrategyTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}

		public class CallbackInfo
		{
			[CandidName("callback")]
			public CandidFunc Callback { get; set; }

			[CandidName("token")]
			public StreamingCallbackToken Token { get; set; }

			public CallbackInfo(CandidFunc callback, StreamingCallbackToken token)
			{
				this.Callback = callback;
				this.Token = token;
			}

			public CallbackInfo()
			{
			}
		}
	}

	public enum StreamingStrategyTag
	{
		Callback
	}
}