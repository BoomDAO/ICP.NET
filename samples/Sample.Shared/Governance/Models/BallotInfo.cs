using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class BallotInfo
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("vote")]
		public int Vote { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("proposal_id")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NeuronId> ProposalId { get; set; }
		
	}
}

