using EdjCase.ICP.Candid.Mapping;


namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	public class ListPermitted
	{
		[CandidName("permission")]
		public Permission Permission { get; set; }

		public ListPermitted(Permission permission)
		{
			this.Permission = permission;
		}

		public ListPermitted()
		{
		}
	}
}