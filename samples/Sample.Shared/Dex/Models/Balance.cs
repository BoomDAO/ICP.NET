using EdjCase.ICP.Candid.Models;
using Token = EdjCase.ICP.Candid.Models.Principal;

namespace Sample.Shared.Dex.Models
{
	public class Balance
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("amount")]
		public UnboundedUInt Amount { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("owner")]
		public EdjCase.ICP.Candid.Models.Principal Owner { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("token")]
		public Token Token { get; set; }
		
	}
}

