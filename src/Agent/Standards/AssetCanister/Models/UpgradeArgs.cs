using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;


namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request for upgrading the asset canister with options
	/// </summary>
	public class UpgradeArgs
	{
		/// <summary>
		/// Optional. The permissions to set for the asset canister
		/// </summary>
		[CandidName("set_permissions")]
		public OptionalValue<SetPermissions> SetPermissions { get; set; }


		/// <summary>
		/// Creates a new instance of the <see cref="UpgradeArgs"/> class.
		/// </summary>
		/// <param name="setPermissions">Optional value for setting permissions.</param>
		public UpgradeArgs(OptionalValue<SetPermissions> setPermissions)
		{
			this.SetPermissions = setPermissions;
		}


	}
}
