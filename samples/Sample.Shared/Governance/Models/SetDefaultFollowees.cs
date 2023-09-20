using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		[CandidName("default_followees")]
		public List<(int, Followees)> DefaultFollowees { get; set; }

		public SetDefaultFollowees(List<(int, Followees)> defaultFollowees)
		{
			this.DefaultFollowees = defaultFollowees;
		}

		public SetDefaultFollowees()
		{
		}
	}
}