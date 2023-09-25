using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class DissolveState
	{
		[VariantTagProperty()]
		public DissolveStateTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public ulong? DissolveDelaySeconds { get => this.Tag == DissolveStateTag.DissolveDelaySeconds ? (ulong)this.Value : default; set => (this.Tag, this.Value) = (DissolveStateTag.DissolveDelaySeconds, value); }

		public ulong? WhenDissolvedTimestampSeconds { get => this.Tag == DissolveStateTag.WhenDissolvedTimestampSeconds ? (ulong)this.Value : default; set => (this.Tag, this.Value) = (DissolveStateTag.WhenDissolvedTimestampSeconds, value); }

		public DissolveState(DissolveStateTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected DissolveState()
		{
		}
	}

	public enum DissolveStateTag
	{
		DissolveDelaySeconds,
		WhenDissolvedTimestampSeconds
	}
}