using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class Tally
	{
		public ulong no { get; set; }
		
		public ulong yes { get; set; }
		
		public ulong total { get; set; }
		
		public ulong timestamp_seconds { get; set; }
		
	}
}

