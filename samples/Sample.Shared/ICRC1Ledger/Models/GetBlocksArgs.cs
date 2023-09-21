using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<(System.String, Sample.Shared.ICRC1Ledger.Models.Value)>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

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