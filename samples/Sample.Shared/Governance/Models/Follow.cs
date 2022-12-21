using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class Follow
	{
		public int topic { get; set; }
		
		public List<NeuronId> followees { get; set; }
		
	}
}

