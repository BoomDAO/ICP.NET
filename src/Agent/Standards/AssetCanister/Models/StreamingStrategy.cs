using EdjCase.ICP.Candid.Mapping;

using System;
using EdjCase.ICP.Candid.Models.Values;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a strategy for streaming from an asset canister
	/// </summary>
	[Variant]
	public class StreamingStrategy
	{
		/// <summary>
		/// The tag for the streaming strategy variant
		/// </summary>
		[VariantTagProperty]
		public StreamingStrategyTag Tag { get; set; }

		/// <summary>
		/// The value for the streaming strategy variant
		/// </summary>
		[VariantValueProperty]
		public object? Value { get; set; }


		/// <summary>
		/// Represents a strategy for streaming from an asset canister
		/// </summary>
		/// <param name="tag">The tag for the streaming strategy variant</param>
		/// <param name="value">The value for the streaming strategy variant</param>
		public StreamingStrategy(StreamingStrategyTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StreamingStrategy"/> class.
		/// </summary>
		protected StreamingStrategy()
		{
		}

		/// <summary>
		/// Creates a new streaming strategy with a callback
		/// </summary>
		/// <param name="info">The callback info</param>
		/// <returns>The streaming strategy variant</returns>
		public static StreamingStrategy Callback(StreamingStrategy.CallbackInfo info)
		{
			return new StreamingStrategy(StreamingStrategyTag.Callback, info);
		}

		/// <summary>
		/// Casts the streaming strategy to a callback
		/// </summary>
		/// <returns>The Callback info</returns>
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

		/// <summary>
		/// Represents information about a callback function and its associated token.
		/// </summary>
		public class CallbackInfo
		{
			/// <summary>
			/// The callback function
			/// </summary>
			[CandidName("callback")]
			public CandidFunc Callback { get; set; }

			/// <summary>
			/// The streaming callback token
			/// </summary>
			[CandidName("token")]
			public StreamingCallbackToken Token { get; set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="CallbackInfo"/> class.
			/// </summary>
			/// <param name="callback">The callback function.</param>
			/// <param name="token">The streaming callback token.</param>
			public CallbackInfo(CandidFunc callback, StreamingCallbackToken token)
			{
				this.Callback = callback;
				this.Token = token;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="CallbackInfo"/> class.
			/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public CallbackInfo()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			{
			}
		}
	}

	/// <summary>
	/// Represents all the options for the streaming strategy variant
	/// </summary>
	public enum StreamingStrategyTag
	{
		/// <summary>
		/// For callback streaming
		/// </summary>
		Callback
	}
}
