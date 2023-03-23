using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		[CandidName("default_followees")]
		public List<SetDefaultFollowees.DefaultFolloweesItemRecord> DefaultFollowees { get; set; }

		public SetDefaultFollowees(List<SetDefaultFollowees.DefaultFolloweesItemRecord> defaultFollowees)
		{
			this.DefaultFollowees = defaultFollowees;
		}

		public SetDefaultFollowees()
		{
		}

		public class DefaultFolloweesItemRecord
		{
			[CandidTag(0U)]
			public int F0 { get; set; }

			[CandidTag(1U)]
			public Followees F1 { get; set; }

			public DefaultFolloweesItemRecord(int f0, Followees f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public DefaultFolloweesItemRecord()
			{
			}
		}
	}
}