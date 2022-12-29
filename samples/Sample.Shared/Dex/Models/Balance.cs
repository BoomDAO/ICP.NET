using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	public class Balance
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("amount")]
		public EdjCase.ICP.Candid.UnboundedUInt Amount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("owner")]
		public EdjCase.ICP.Candid.Models.Principal Owner { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("token")]
		public Token Token { get; set; }
		
	}
}

