namespace Sample.Shared.Governance.Models
{
	public class Spawn
	{
		public uint? PercentageToSpawn { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? NewController { get; set; }
		
		public ulong? Nonce { get; set; }
		
	}
}
