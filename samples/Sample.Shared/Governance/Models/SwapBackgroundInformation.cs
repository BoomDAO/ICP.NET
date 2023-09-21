using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class SwapBackgroundInformation
	{
		[CandidName("ledger_index_canister_summary")]
		public OptionalValue<CanisterSummary> LedgerIndexCanisterSummary { get; set; }

		[CandidName("fallback_controller_principal_ids")]
		public List<Principal> FallbackControllerPrincipalIds { get; set; }

		[CandidName("ledger_archive_canister_summaries")]
		public List<CanisterSummary> LedgerArchiveCanisterSummaries { get; set; }

		[CandidName("ledger_canister_summary")]
		public OptionalValue<CanisterSummary> LedgerCanisterSummary { get; set; }

		[CandidName("swap_canister_summary")]
		public OptionalValue<CanisterSummary> SwapCanisterSummary { get; set; }

		[CandidName("governance_canister_summary")]
		public OptionalValue<CanisterSummary> GovernanceCanisterSummary { get; set; }

		[CandidName("root_canister_summary")]
		public OptionalValue<CanisterSummary> RootCanisterSummary { get; set; }

		[CandidName("dapp_canister_summaries")]
		public List<CanisterSummary> DappCanisterSummaries { get; set; }

		public SwapBackgroundInformation(OptionalValue<CanisterSummary> ledgerIndexCanisterSummary, List<Principal> fallbackControllerPrincipalIds, List<CanisterSummary> ledgerArchiveCanisterSummaries, OptionalValue<CanisterSummary> ledgerCanisterSummary, OptionalValue<CanisterSummary> swapCanisterSummary, OptionalValue<CanisterSummary> governanceCanisterSummary, OptionalValue<CanisterSummary> rootCanisterSummary, List<CanisterSummary> dappCanisterSummaries)
		{
			this.LedgerIndexCanisterSummary = ledgerIndexCanisterSummary;
			this.FallbackControllerPrincipalIds = fallbackControllerPrincipalIds;
			this.LedgerArchiveCanisterSummaries = ledgerArchiveCanisterSummaries;
			this.LedgerCanisterSummary = ledgerCanisterSummary;
			this.SwapCanisterSummary = swapCanisterSummary;
			this.GovernanceCanisterSummary = governanceCanisterSummary;
			this.RootCanisterSummary = rootCanisterSummary;
			this.DappCanisterSummaries = dappCanisterSummaries;
		}

		public SwapBackgroundInformation()
		{
		}
	}
}