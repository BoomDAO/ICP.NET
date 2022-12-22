using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Motion
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("motion_text")]
		public string MotionText { get; set; }
		
	}
}

