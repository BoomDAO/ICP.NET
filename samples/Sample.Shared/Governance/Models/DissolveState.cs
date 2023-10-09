using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class DissolveState
	{
		[VariantTagProperty]
		public DissolveStateTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public DissolveState(DissolveStateTag tag, object? value)
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

		public static DissolveState WhenDissolvedTimestampSeconds(ulong info)
		{
			return new DissolveState(DissolveStateTag.WhenDissolvedTimestampSeconds, info);
		}

		public ulong AsDissolveDelaySeconds()
		{
			this.ValidateTag(DissolveStateTag.DissolveDelaySeconds);
			return (ulong)this.Value!;
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
		DissolveDelaySeconds,
		WhenDissolvedTimestampSeconds
	}
}