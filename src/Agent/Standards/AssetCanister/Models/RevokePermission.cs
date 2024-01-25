using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to revoke a permission from a principal in the asset canister.
	/// </summary>
	public class RevokePermission
	{
		/// <summary>
		/// The principal from whom the permission will be revoked.
		/// </summary>
		[CandidName("of_principal")]
		public Principal OfPrincipal { get; set; }

		/// <summary>
		/// The permission to be revoked.
		/// </summary>
		[CandidName("permission")]
		public Permission Permission { get; set; }

		/// <summary>
		/// Initializes a new instance of the RevokePermission class with specified principal and permission.
		/// </summary>
		/// <param name="ofPrincipal">The principal from whom the permission will be revoked.</param>
		/// <param name="permission">The permission to be revoked.</param>
		public RevokePermission(Principal ofPrincipal, Permission permission)
		{
			this.OfPrincipal = ofPrincipal;
			this.Permission = permission;
		}

		/// <summary>
		/// Initializes a new instance of the RevokePermission class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public RevokePermission()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
