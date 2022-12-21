using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class Spawn
	{
		public uint? percentage_to_spawn { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal? new_controller { get; set; }
		
		public ulong? nonce { get; set; }
		
	}
}

