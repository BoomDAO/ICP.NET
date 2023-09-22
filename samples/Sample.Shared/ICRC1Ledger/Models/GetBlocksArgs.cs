using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class GetBlocksArgs
	{
		[CandidName("start")]
		public BlockIndex Start { get; set; }

		[CandidName("length")]
		public UnboundedUInt Length { get; set; }

		public GetBlocksArgs(BlockIndex start, UnboundedUInt length)
		{
			this.Start = start;
			this.Length = length;
		}

		public GetBlocksArgs()
		{
		}
	}
}