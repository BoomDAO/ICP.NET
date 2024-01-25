namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Enum representing the different types of permissions for interacting with an asset canister.
	/// </summary>
	public enum Permission
	{
		/// <summary>
		/// Permission allowing changes to the assets served by the asset canister.
		/// </summary>
		Commit,

		/// <summary>
		/// Permission allowing a principal to grant and revoke permissions to other principals.
		/// </summary>
		ManagePermissions,

		/// <summary>
		/// Permission allowing the upload of data to the canister to be committed later by a principal with the Commit permission.
		/// </summary>
		Prepare
	}
}
