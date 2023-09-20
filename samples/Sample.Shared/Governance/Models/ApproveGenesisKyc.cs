using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class ApproveGenesisKyc
	{
		[CandidName("principals")]
		public List<Principal> Principals { get; set; }

		public ApproveGenesisKyc(List<Principal> principals)
		{
			this.Principals = principals;
		}

		public ApproveGenesisKyc()
		{
		}
	}
}