using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<Sample.Shared.ICRC1Ledger.Models.MapItem>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class UpgradeArgs
	{
		[CandidName("metadata")]
		public OptionalValue<List<UpgradeArgs.MetadataItemItem>> Metadata { get; set; }

		[CandidName("token_symbol")]
		public OptionalValue<string> TokenSymbol { get; set; }

		[CandidName("token_name")]
		public OptionalValue<string> TokenName { get; set; }

		[CandidName("transfer_fee")]
		public OptionalValue<ulong> TransferFee { get; set; }

		[CandidName("change_fee_collector")]
		public OptionalValue<ChangeFeeCollector> ChangeFeeCollector { get; set; }

		[CandidName("max_memo_length")]
		public OptionalValue<ushort> MaxMemoLength { get; set; }

		public UpgradeArgs(OptionalValue<List<UpgradeArgs.MetadataItemItem>> metadata, OptionalValue<string> tokenSymbol, OptionalValue<string> tokenName, OptionalValue<ulong> transferFee, OptionalValue<ChangeFeeCollector> changeFeeCollector, OptionalValue<ushort> maxMemoLength)
		{
			this.Metadata = metadata;
			this.TokenSymbol = tokenSymbol;
			this.TokenName = tokenName;
			this.TransferFee = transferFee;
			this.ChangeFeeCollector = changeFeeCollector;
			this.MaxMemoLength = maxMemoLength;
		}

		public UpgradeArgs()
		{
		}

		public class MetadataItemItem
		{
			[CandidTag(0U)]
			public string F0 { get; set; }

			[CandidTag(1U)]
			public MetadataValue F1 { get; set; }

			public MetadataItemItem(string f0, MetadataValue f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public MetadataItemItem()
			{
			}
		}
	}
}