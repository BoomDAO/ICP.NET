using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class CreateServiceNervousSystem
	{
		[CandidName("url")]
		public OptionalValue<string> Url { get; set; }

		[CandidName("governance_parameters")]
		public OptionalValue<GovernanceParameters> GovernanceParameters { get; set; }

		[CandidName("fallback_controller_principal_ids")]
		public List<Principal> FallbackControllerPrincipalIds { get; set; }

		[CandidName("logo")]
		public OptionalValue<Image> Logo { get; set; }

		[CandidName("name")]
		public OptionalValue<string> Name { get; set; }

		[CandidName("ledger_parameters")]
		public OptionalValue<LedgerParameters> LedgerParameters { get; set; }

		[CandidName("description")]
		public OptionalValue<string> Description { get; set; }

		[CandidName("dapp_canisters")]
		public List<Canister> DappCanisters { get; set; }

		[CandidName("swap_parameters")]
		public OptionalValue<SwapParameters> SwapParameters { get; set; }

		[CandidName("initial_token_distribution")]
		public OptionalValue<InitialTokenDistribution> InitialTokenDistribution { get; set; }

		public CreateServiceNervousSystem(OptionalValue<string> url, OptionalValue<GovernanceParameters> governanceParameters, List<Principal> fallbackControllerPrincipalIds, OptionalValue<Image> logo, OptionalValue<string> name, OptionalValue<LedgerParameters> ledgerParameters, OptionalValue<string> description, List<Canister> dappCanisters, OptionalValue<SwapParameters> swapParameters, OptionalValue<InitialTokenDistribution> initialTokenDistribution)
		{
			this.Url = url;
			this.GovernanceParameters = governanceParameters;
			this.FallbackControllerPrincipalIds = fallbackControllerPrincipalIds;
			this.Logo = logo;
			this.Name = name;
			this.LedgerParameters = ledgerParameters;
			this.Description = description;
			this.DappCanisters = dappCanisters;
			this.SwapParameters = swapParameters;
			this.InitialTokenDistribution = initialTokenDistribution;
		}

		public CreateServiceNervousSystem()
		{
		}
	}
}