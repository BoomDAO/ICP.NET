namespace Sample.Shared.Governance.Models
{
	public enum DissolveStateType
	{
		DissolveDelaySeconds,
		WhenDissolvedTimestampSeconds,
	}
	public class DissolveState
	{
		public DissolveStateType Type { get; }
		private readonly object? value;
		
		public DissolveState(DissolveStateType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static DissolveState DissolveDelaySeconds(ulong info)
		{
			return new DissolveState(DissolveStateType.DissolveDelaySeconds, info);
		}
		
		public ulong AsDissolveDelaySeconds()
		{
			this.ValidateType(DissolveStateType.DissolveDelaySeconds);
			return (ulong)this.value!;
		}
		
		public static DissolveState WhenDissolvedTimestampSeconds(ulong info)
		{
			return new DissolveState(DissolveStateType.WhenDissolvedTimestampSeconds, info);
		}
		
		public ulong AsWhenDissolvedTimestampSeconds()
		{
			this.ValidateType(DissolveStateType.WhenDissolvedTimestampSeconds);
			return (ulong)this.value!;
		}
		
		private void ValidateType(DissolveStateType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
