using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System.Collections.Generic;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class BlockRange
	{
		[CandidName("blocks")]
		public BlockRange.BlocksInfo Blocks { get; set; }

		public BlockRange(BlockRange.BlocksInfo blocks)
		{
			this.Blocks = blocks;
		}

		public BlockRange()
		{
		}

		public class BlocksInfo : List<Block>
		{
			public BlocksInfo()
			{
			}
		}
	}
}