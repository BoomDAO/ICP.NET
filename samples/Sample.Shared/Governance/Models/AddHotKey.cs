using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class AddHotKey
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("new_hot_key")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> NewHotKey { get; set; }
		
	}
}

