using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class ChangeAutoStakeMaturity
	{
		[CandidName("requested_setting_for_auto_stake_maturity")]
		public bool RequestedSettingForAutoStakeMaturity { get; set; }

		public ChangeAutoStakeMaturity(bool requestedSettingForAutoStakeMaturity)
		{
			this.RequestedSettingForAutoStakeMaturity = requestedSettingForAutoStakeMaturity;
		}

		public ChangeAutoStakeMaturity()
		{
		}
	}
}