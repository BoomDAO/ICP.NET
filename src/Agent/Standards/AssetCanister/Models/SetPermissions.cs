using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to set permessions for identities in the asset canister.
	/// </summary>
	public class SetPermissions
	{
		/// <summary>
		/// A list of identities that can prepare an asset
		/// </summary>
		[CandidName("prepare")]
		public List<Principal> Prepare { get; set; }

		/// <summary>
		/// A list of identities that can commit an asset
		/// </summary>
		[CandidName("commit")]
		public List<Principal> Commit { get; set; }

		/// <summary>
		/// A list of identities that can manage permissions
		/// </summary>
		[CandidName("manage_permissions")]
		public List<Principal> ManagePermissions { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SetPermissions"/> class.
		/// </summary>
		/// <param name="prepare">The list of principals with prepare permissions.</param>
		/// <param name="commit">The list of principals with commit permissions.</param>
		/// <param name="managePermissions">The list of principals with manage permissions.</param>
		public SetPermissions(List<Principal> prepare, List<Principal> commit, List<Principal> managePermissions)
		{
			this.Prepare = prepare;
			this.Commit = commit;
			this.ManagePermissions = managePermissions;
		}
	}
}
