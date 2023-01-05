using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(DissolveStateTag))]
	public class DissolveState
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public DissolveStateTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private DissolveState(DissolveStateTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected DissolveState()
		{
		}
		
		public static DissolveState DissolveDelaySeconds(ulong info)
		{
			return new DissolveState(DissolveStateTag.DissolveDelaySeconds, info);
		}
		
		public ulong AsDissolveDelaySeconds()
		{
			this.ValidateTag(DissolveStateTag.DissolveDelaySeconds);
			return (ulong)this.Value!;
		}
		
		public static DissolveState WhenDissolvedTimestampSeconds(ulong info)
		{
			return new DissolveState(DissolveStateTag.WhenDissolvedTimestampSeconds, info);
		}
		
		public ulong AsWhenDissolvedTimestampSeconds()
		{
			this.ValidateTag(DissolveStateTag.WhenDissolvedTimestampSeconds);
			return (ulong)this.Value!;
		}
		
		private void ValidateTag(DissolveStateTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum DissolveStateTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("DissolveDelaySeconds")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ulong))]
		DissolveDelaySeconds,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("WhenDissolvedTimestampSeconds")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(ulong))]
		WhenDissolvedTimestampSeconds,
	}
}

