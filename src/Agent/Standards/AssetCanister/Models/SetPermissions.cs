using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class SetPermissions
	{
		[CandidName("prepare")]
		public List<Principal> Prepare { get; set; }

		[CandidName("commit")]
		public List<Principal> Commit { get; set; }

		[CandidName("manage_permissions")]
		public List<Principal> ManagePermissions { get; set; }

		public SetPermissions(List<Principal> prepare, List<Principal> commit, List<Principal> managePermissions)
		{
			this.Prepare = prepare;
			this.Commit = commit;
			this.ManagePermissions = managePermissions;
		}

		public SetPermissions()
		{
		}
	}
}