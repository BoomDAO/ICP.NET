using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class BlockRange
	{
		[CandidName("blocks")]
		public List<Block> Blocks { get; set; }

		public BlockRange(List<Block> blocks)
		{
			this.Blocks = blocks;
		}

		public BlockRange()
		{
		}
	}
}