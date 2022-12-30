using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class GovernanceError
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("error_message")]
		public string ErrorMessage { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("error_type")]
		public int ErrorType { get; set; }
		
	}
}

