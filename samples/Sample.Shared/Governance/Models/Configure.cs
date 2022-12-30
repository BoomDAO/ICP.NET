using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Configure
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("operation")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Operation> Operation { get; set; }
		
	}
}

