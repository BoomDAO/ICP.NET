using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance
{
	public class GovernanceApiClient
	{
		public IAgent Agent { get; }
		public Principal CanisterId { get; }
		public GovernanceApiClient(IAgent agent, Principal canisterId)
		{
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
		}
		public async Task<Result> ClaimGtcNeuronsAsync(EdjCase.ICP.Candid.Models.Principal arg0, List<NeuronId> arg1, IIdentity? identityOverride = null)
		{
			string method = "claim_gtc_neurons";
			CandidValueWithType p0 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.Models.Principal>(arg0);
			CandidValueWithType p1 = CandidValueWithType.FromObject<List<NeuronId>>(arg1);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result r0 = reply.Arg.Values[0].ToObject<Result>();
			return (r0);
		}
		public async Task<ClaimOrRefreshNeuronFromAccountResponse> ClaimOrRefreshNeuronFromAccountAsync(ClaimOrRefreshNeuronFromAccount arg0, IIdentity? identityOverride = null)
		{
			string method = "claim_or_refresh_neuron_from_account";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ClaimOrRefreshNeuronFromAccount>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			ClaimOrRefreshNeuronFromAccountResponse r0 = reply.Arg.Values[0].ToObject<ClaimOrRefreshNeuronFromAccountResponse>();
			return (r0);
		}
		public async Task<string> GetBuildMetadataAsync(IIdentity? identityOverride = null)
		{
			string method = "get_build_metadata";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			string r0 = reply.Arg.Values[0].ToObject<string>();
			return (r0);
		}
		public async Task<Result2> GetFullNeuronAsync(ulong arg0, IIdentity? identityOverride = null)
		{
			string method = "get_full_neuron";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ulong>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result2 r0 = reply.Arg.Values[0].ToObject<Result2>();
			return (r0);
		}
		public async Task<Result2> GetFullNeuronByIdOrSubaccountAsync(NeuronIdOrSubaccount arg0, IIdentity? identityOverride = null)
		{
			string method = "get_full_neuron_by_id_or_subaccount";
			CandidValueWithType p0 = CandidValueWithType.FromObject<NeuronIdOrSubaccount>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result2 r0 = reply.Arg.Values[0].ToObject<Result2>();
			return (r0);
		}
		public async Task<Result3> GetMonthlyNodeProviderRewardsAsync(IIdentity? identityOverride = null)
		{
			string method = "get_monthly_node_provider_rewards";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result3 r0 = reply.Arg.Values[0].ToObject<Result3>();
			return (r0);
		}
		public async Task<NetworkEconomics> GetNetworkEconomicsParametersAsync(IIdentity? identityOverride = null)
		{
			string method = "get_network_economics_parameters";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			NetworkEconomics r0 = reply.Arg.Values[0].ToObject<NetworkEconomics>();
			return (r0);
		}
		public async Task<List<ulong>> GetNeuronIdsAsync(IIdentity? identityOverride = null)
		{
			string method = "get_neuron_ids";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			List<ulong> r0 = reply.Arg.Values[0].ToObject<List<ulong>>();
			return (r0);
		}
		public async Task<Result4> GetNeuronInfoAsync(ulong arg0, IIdentity? identityOverride = null)
		{
			string method = "get_neuron_info";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ulong>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result4 r0 = reply.Arg.Values[0].ToObject<Result4>();
			return (r0);
		}
		public async Task<Result4> GetNeuronInfoByIdOrSubaccountAsync(NeuronIdOrSubaccount arg0, IIdentity? identityOverride = null)
		{
			string method = "get_neuron_info_by_id_or_subaccount";
			CandidValueWithType p0 = CandidValueWithType.FromObject<NeuronIdOrSubaccount>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result4 r0 = reply.Arg.Values[0].ToObject<Result4>();
			return (r0);
		}
		public async Task<Result5> GetNodeProviderByCallerAsync(IIdentity? identityOverride = null)
		{
			string method = "get_node_provider_by_caller";
			CandidValueWithType p0 = CandidValueWithType.Null();
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result5 r0 = reply.Arg.Values[0].ToObject<Result5>();
			return (r0);
		}
		public async Task<List<ProposalInfo>> GetPendingProposalsAsync(IIdentity? identityOverride = null)
		{
			string method = "get_pending_proposals";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			List<ProposalInfo> r0 = reply.Arg.Values[0].ToObject<List<ProposalInfo>>();
			return (r0);
		}
		public async Task<ProposalInfo?> GetProposalInfoAsync(ulong arg0, IIdentity? identityOverride = null)
		{
			string method = "get_proposal_info";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ulong>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			ProposalInfo? r0 = reply.Arg.Values[0].ToObject<ProposalInfo?>();
			return (r0);
		}
		public async Task<ListKnownNeuronsResponse> ListKnownNeuronsAsync(IIdentity? identityOverride = null)
		{
			string method = "list_known_neurons";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			ListKnownNeuronsResponse r0 = reply.Arg.Values[0].ToObject<ListKnownNeuronsResponse>();
			return (r0);
		}
		public async Task<ListNeuronsResponse> ListNeuronsAsync(ListNeurons arg0, IIdentity? identityOverride = null)
		{
			string method = "list_neurons";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ListNeurons>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			ListNeuronsResponse r0 = reply.Arg.Values[0].ToObject<ListNeuronsResponse>();
			return (r0);
		}
		public async Task<ListNodeProvidersResponse> ListNodeProvidersAsync(IIdentity? identityOverride = null)
		{
			string method = "list_node_providers";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			ListNodeProvidersResponse r0 = reply.Arg.Values[0].ToObject<ListNodeProvidersResponse>();
			return (r0);
		}
		public async Task<ListProposalInfoResponse> ListProposalsAsync(ListProposalInfo arg0, IIdentity? identityOverride = null)
		{
			string method = "list_proposals";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ListProposalInfo>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			ListProposalInfoResponse r0 = reply.Arg.Values[0].ToObject<ListProposalInfoResponse>();
			return (r0);
		}
		public async Task<ManageNeuronResponse> ManageNeuronAsync(ManageNeuron arg0, IIdentity? identityOverride = null)
		{
			string method = "manage_neuron";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ManageNeuron>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			ManageNeuronResponse r0 = reply.Arg.Values[0].ToObject<ManageNeuronResponse>();
			return (r0);
		}
		public async Task<Result> TransferGtcNeuronAsync(NeuronId arg0, NeuronId arg1, IIdentity? identityOverride = null)
		{
			string method = "transfer_gtc_neuron";
			CandidValueWithType p0 = CandidValueWithType.FromObject<NeuronId>(arg0);
			CandidValueWithType p1 = CandidValueWithType.FromObject<NeuronId>(arg1);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result r0 = reply.Arg.Values[0].ToObject<Result>();
			return (r0);
		}
		public async Task<Result> UpdateNodeProviderAsync(UpdateNodeProvider arg0, IIdentity? identityOverride = null)
		{
			string method = "update_node_provider";
			CandidValueWithType p0 = CandidValueWithType.FromObject<UpdateNodeProvider>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Result r0 = reply.Arg.Values[0].ToObject<Result>();
			return (r0);
		}
	}
}
