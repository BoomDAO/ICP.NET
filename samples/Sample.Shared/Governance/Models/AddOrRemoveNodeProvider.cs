using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class AddOrRemoveNodeProvider
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("change")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Change> Change { get; set; }
		
	}
}

