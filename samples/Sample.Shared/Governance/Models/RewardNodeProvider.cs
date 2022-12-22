using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class RewardNodeProvider
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("node_provider")]
		public EdjCase.ICP.Candid.Models.OptionalValue<NodeProvider> NodeProvider { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("reward_mode")]
		public EdjCase.ICP.Candid.Models.OptionalValue<RewardMode> RewardMode { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("amount_e8s")]
		public ulong AmountE8s { get; set; }
		
	}
}

