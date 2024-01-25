using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to grant a permission to a principal in the asset canister.
	/// </summary>
	public class GrantPermission
	{
		/// <summary>
		/// The principal to which the permission will be granted.
		/// </summary>
		[CandidName("to_principal")]
		public Principal ToPrincipal { get; set; }

		/// <summary>
		/// The permission to be granted.
		/// </summary>
		[CandidName("permission")]
		public Permission Permission { get; set; }

		/// <summary>
		/// Initializes a new instance of the GrantPermission class with specified principal and permission.
		/// </summary>
		/// <param name="toPrincipal">The principal to grant permission to.</param>
		/// <param name="permission">The permission to be granted.</param>
		public GrantPermission(Principal toPrincipal, Permission permission)
		{
			this.ToPrincipal = toPrincipal;
			this.Permission = permission;
		}

		/// <summary>
		/// Initializes a new instance of the GrantPermission class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public GrantPermission()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
