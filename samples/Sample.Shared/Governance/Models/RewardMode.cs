namespace Sample.Shared.Governance.Models
{
	public enum RewardModeType
	{
		RewardToNeuron,
		RewardToAccount,
	}
	public class RewardMode
	{
		public RewardModeType Type { get; }
		private readonly object? value;
		
		public RewardMode(RewardModeType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static RewardMode RewardToNeuron(RewardToNeuron info)
		{
			return new RewardMode(RewardModeType.RewardToNeuron, info);
		}
		
		public RewardToNeuron AsRewardToNeuron()
		{
			this.ValidateType(RewardModeType.RewardToNeuron);
			return (RewardToNeuron)this.value!;
		}
		
		public static RewardMode RewardToAccount(RewardToAccount info)
		{
			return new RewardMode(RewardModeType.RewardToAccount, info);
		}
		
		public RewardToAccount AsRewardToAccount()
		{
			this.ValidateType(RewardModeType.RewardToAccount);
			return (RewardToAccount)this.value!;
		}
		
		private void ValidateType(RewardModeType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
