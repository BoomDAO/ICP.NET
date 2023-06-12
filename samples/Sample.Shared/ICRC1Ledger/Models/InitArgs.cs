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
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class InitArgs
	{
		[CandidName("minting_account")]
		public Account MintingAccount { get; set; }

		[CandidName("fee_collector_account")]
		public OptionalValue<Account> FeeCollectorAccount { get; set; }

		[CandidName("transfer_fee")]
		public ulong TransferFee { get; set; }

		[CandidName("token_symbol")]
		public string TokenSymbol { get; set; }

		[CandidName("token_name")]
		public string TokenName { get; set; }

		[CandidName("metadata")]
		public List<InitArgs.MetadataItem> Metadata { get; set; }

		[CandidName("initial_balances")]
		public List<InitArgs.InitialBalancesItem> InitialBalances { get; set; }

		[CandidName("archive_options")]
		public InitArgs.ArchiveOptionsInfo ArchiveOptions { get; set; }

		public InitArgs(Account mintingAccount, OptionalValue<Account> feeCollectorAccount, ulong transferFee, string tokenSymbol, string tokenName, List<InitArgs.MetadataItem> metadata, List<InitArgs.InitialBalancesItem> initialBalances, InitArgs.ArchiveOptionsInfo archiveOptions)
		{
			this.MintingAccount = mintingAccount;
			this.FeeCollectorAccount = feeCollectorAccount;
			this.TransferFee = transferFee;
			this.TokenSymbol = tokenSymbol;
			this.TokenName = tokenName;
			this.Metadata = metadata;
			this.InitialBalances = initialBalances;
			this.ArchiveOptions = archiveOptions;
		}

		public InitArgs()
		{
		}

		public class MetadataItem
		{
			[CandidTag(0U)]
			public string F0 { get; set; }

			[CandidTag(1U)]
			public MetadataValue F1 { get; set; }

			public MetadataItem(string f0, MetadataValue f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public MetadataItem()
			{
			}
		}

		public class InitialBalancesItem
		{
			[CandidTag(0U)]
			public Account F0 { get; set; }

			[CandidTag(1U)]
			public ulong F1 { get; set; }

			public InitialBalancesItem(Account f0, ulong f1)
			{
				this.F0 = f0;
				this.F1 = f1;
			}

			public InitialBalancesItem()
			{
			}
		}

		public class ArchiveOptionsInfo
		{
			[CandidName("num_blocks_to_archive")]
			public ulong NumBlocksToArchive { get; set; }

			[CandidName("trigger_threshold")]
			public ulong TriggerThreshold { get; set; }

			[CandidName("max_message_size_bytes")]
			public OptionalValue<ulong> MaxMessageSizeBytes { get; set; }

			[CandidName("cycles_for_archive_creation")]
			public OptionalValue<ulong> CyclesForArchiveCreation { get; set; }

			[CandidName("node_max_memory_size_bytes")]
			public OptionalValue<ulong> NodeMaxMemorySizeBytes { get; set; }

			[CandidName("controller_id")]
			public Principal ControllerId { get; set; }

			public ArchiveOptionsInfo(ulong numBlocksToArchive, ulong triggerThreshold, OptionalValue<ulong> maxMessageSizeBytes, OptionalValue<ulong> cyclesForArchiveCreation, OptionalValue<ulong> nodeMaxMemorySizeBytes, Principal controllerId)
			{
				this.NumBlocksToArchive = numBlocksToArchive;
				this.TriggerThreshold = triggerThreshold;
				this.MaxMessageSizeBytes = maxMessageSizeBytes;
				this.CyclesForArchiveCreation = cyclesForArchiveCreation;
				this.NodeMaxMemorySizeBytes = nodeMaxMemorySizeBytes;
				this.ControllerId = controllerId;
			}

			public ArchiveOptionsInfo()
			{
			}
		}
	}
}