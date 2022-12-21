using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Dex.Models
{
	public class Balance
	{
		public EdjCase.ICP.Candid.UnboundedUInt amount { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal owner { get; set; }
		
		public Token token { get; set; }
		
	}
}

