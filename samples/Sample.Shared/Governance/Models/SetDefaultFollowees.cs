using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		[CandidName("default_followees")]
		public List<ValueTuple<int, Followees>> DefaultFollowees { get; set; }

		public SetDefaultFollowees(List<ValueTuple<int, Followees>> defaultFollowees)
		{
			this.DefaultFollowees = defaultFollowees;
		}

		public SetDefaultFollowees()
		{
		}
	}
}