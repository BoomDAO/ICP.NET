using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a request to list principals with a specified permission in the asset canister.
	/// </summary>
	public class ListPermitted
	{
		/// <summary>
		/// The specific permission to query for.
		/// </summary>
		[CandidName("permission")]
		public Permission Permission { get; set; }

		/// <summary>
		/// Initializes a new instance of the ListPermitted class with a specified permission.
		/// </summary>
		/// <param name="permission">The permission to query for.</param>
		public ListPermitted(Permission permission)
		{
			this.Permission = permission;
		}

		/// <summary>
		/// Initializes a new instance of the ListPermitted class.
		/// </summary>
		public ListPermitted()
		{
		}
	}
}
