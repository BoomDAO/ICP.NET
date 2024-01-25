using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;


namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class UpgradeArgs
	{
		[CandidName("set_permissions")]
		public OptionalValue<SetPermissions> SetPermissions { get; set; }

		public UpgradeArgs(OptionalValue<SetPermissions> setPermissions)
		{
			this.SetPermissions = setPermissions;
		}

		public UpgradeArgs()
		{
		}
	}
}