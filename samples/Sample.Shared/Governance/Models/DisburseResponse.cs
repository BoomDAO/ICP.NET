using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class DisburseResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("transfer_block_height")]
		public ulong TransferBlockHeight { get; set; }
		
	}
}

