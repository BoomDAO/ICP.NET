using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		[CandidName("default_followees")]
		public List<SetDefaultFollowees.DefaultFolloweesItem> DefaultFollowees { get; set; }

		public SetDefaultFollowees(List<SetDefaultFollowees.DefaultFolloweesItem> defaultFollowees)
		{
			this.DefaultFollowees = defaultFollowees;
		}

		public SetDefaultFollowees()
		{
		}

		public class DefaultFolloweesItem
		{
			[CandidTag(0U)]
			public int F0 { get; set; }

			[CandidTag(1U)]
			public Followees F1 { get; set; }

			public DefaultFolloweesItem(int f0, Followees f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public DefaultFolloweesItem()
			{
			}
		}
	}
}