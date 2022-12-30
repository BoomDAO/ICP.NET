using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class RemoveHotKey
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("hot_key_to_remove")]
		public EdjCase.ICP.Candid.Models.OptionalValue<EdjCase.ICP.Candid.Models.Principal> HotKeyToRemove { get; set; }
		
	}
}

