using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccountResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("result")]
		public EdjCase.ICP.Candid.Models.OptionalValue<Result_1> Result { get; set; }
		
	}
}

