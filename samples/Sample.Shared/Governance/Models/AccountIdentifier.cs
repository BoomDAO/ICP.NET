using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class AccountIdentifier
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("hash")]
		public System.Collections.Generic.List<byte> Hash { get; set; }
		
	}
}

