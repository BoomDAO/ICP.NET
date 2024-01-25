using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;


namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class RevokePermission
	{
		[CandidName("of_principal")]
		public Principal OfPrincipal { get; set; }

		[CandidName("permission")]
		public Permission Permission { get; set; }

		public RevokePermission(Principal ofPrincipal, Permission permission)
		{
			this.OfPrincipal = ofPrincipal;
			this.Permission = permission;
		}

		public RevokePermission()
		{
		}
	}
}