using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;


namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class GrantPermission
	{
		[CandidName("to_principal")]
		public Principal ToPrincipal { get; set; }

		[CandidName("permission")]
		public Permission Permission { get; set; }

		public GrantPermission(Principal toPrincipal, Permission permission)
		{
			this.ToPrincipal = toPrincipal;
			this.Permission = permission;
		}

		public GrantPermission()
		{
		}
	}
}