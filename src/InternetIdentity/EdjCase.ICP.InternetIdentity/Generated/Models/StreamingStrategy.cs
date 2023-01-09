using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using UserNumber = System.UInt64;
using PublicKey = System.Collections.Generic.List<System.Byte>;
using CredentialId = System.Collections.Generic.List<System.Byte>;
using DeviceKey = PublicKey;
using UserKey = PublicKey;
using SessionKey = PublicKey;
using FrontendHostname = System.String;
using Timestamp = System.UInt64;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(StreamingStrategyTag))]
	public class StreamingStrategy
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public StreamingStrategyTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
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
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("callback")]
			public EdjCase.ICP.Candid.Models.Values.CandidFunc Callback { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("token")]
			public Token Token { get; set; }
			
		}
		public static StreamingStrategy Callback(StreamingStrategy.O0 info)
		{
			return new StreamingStrategy(StreamingStrategyTag.Callback, info);
		}
		
		public StreamingStrategy.O0 AsCallback()
		{
			this.ValidateTag(StreamingStrategyTag.Callback);
			return (StreamingStrategy.O0)this.Value!;
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
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Callback")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(StreamingStrategy.O0))]
		Callback,
	}
}

