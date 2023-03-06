using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;
using EdjCase.ICP.Agent.Responses;

namespace Sample.Shared.Governance
{
	public class GovernanceApiClient
	{
		public IAgent Agent { get; }

		public Principal CanisterId { get; }

		public GovernanceApiClient(IAgent agent, Principal canisterId)
		{
			this.Agent = agent;
			this.CanisterId = canisterId;
		}

		public async System.Threading.Tasks.Task<Result> ClaimGtcNeurons(Principal arg0, List<NeuronId> arg1)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0), CandidTypedValue.FromObject(arg1));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "claim_gtc_neurons", arg);
			return reply.ToObjects<Result>();
		}

		public async System.Threading.Tasks.Task<ClaimOrRefreshNeuronFromAccountResponse> ClaimOrRefreshNeuronFromAccount(ClaimOrRefreshNeuronFromAccount arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "claim_or_refresh_neuron_from_account", arg);
			return reply.ToObjects<ClaimOrRefreshNeuronFromAccountResponse>();
		}

		public async System.Threading.Tasks.Task<string> GetBuildMetadata()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_build_metadata", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<string>();
		}

		public async System.Threading.Tasks.Task<Result2> GetFullNeuron(ulong arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_full_neuron", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Result2>();
		}

		public async System.Threading.Tasks.Task<Result2> GetFullNeuronByIdOrSubaccount(NeuronIdOrSubaccount arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_full_neuron_by_id_or_subaccount", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Result2>();
		}

		public async System.Threading.Tasks.Task<Result3> GetMonthlyNodeProviderRewards()
		{
			CandidArg arg = CandidArg.FromCandid();
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "get_monthly_node_provider_rewards", arg);
			return reply.ToObjects<Result3>();
		}

		public async System.Threading.Tasks.Task<NetworkEconomics> GetNetworkEconomicsParameters()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_network_economics_parameters", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<NetworkEconomics>();
		}

		public async System.Threading.Tasks.Task<List<ulong>> GetNeuronIds()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_neuron_ids", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<ulong>>();
		}

		public async System.Threading.Tasks.Task<Result4> GetNeuronInfo(ulong arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_neuron_info", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Result4>();
		}

		public async System.Threading.Tasks.Task<Result4> GetNeuronInfoByIdOrSubaccount(NeuronIdOrSubaccount arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_neuron_info_by_id_or_subaccount", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Result4>();
		}

		public async System.Threading.Tasks.Task<Result5> GetNodeProviderByCaller(NullValue arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_node_provider_by_caller", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Result5>();
		}

		public async System.Threading.Tasks.Task<List<ProposalInfo>> GetPendingProposals()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_pending_proposals", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<ProposalInfo>>();
		}

		public async System.Threading.Tasks.Task<OptionalValue<ProposalInfo>> GetProposalInfo(ulong arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_proposal_info", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<OptionalValue<ProposalInfo>>();
		}

		public async System.Threading.Tasks.Task<ListKnownNeuronsResponse> ListKnownNeurons()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "list_known_neurons", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<ListKnownNeuronsResponse>();
		}

		public async System.Threading.Tasks.Task<ListNeuronsResponse> ListNeurons(ListNeurons arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "list_neurons", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<ListNeuronsResponse>();
		}

		public async System.Threading.Tasks.Task<ListNodeProvidersResponse> ListNodeProviders()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "list_node_providers", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<ListNodeProvidersResponse>();
		}

		public async System.Threading.Tasks.Task<ListProposalInfoResponse> ListProposals(ListProposalInfo arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "list_proposals", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<ListProposalInfoResponse>();
		}

		public async System.Threading.Tasks.Task<ManageNeuronResponse> ManageNeuron(ManageNeuron arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "manage_neuron", arg);
			return reply.ToObjects<ManageNeuronResponse>();
		}

		public async System.Threading.Tasks.Task<Result> TransferGtcNeuron(NeuronId arg0, NeuronId arg1)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0), CandidTypedValue.FromObject(arg1));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "transfer_gtc_neuron", arg);
			return reply.ToObjects<Result>();
		}

		public async System.Threading.Tasks.Task<Result> UpdateNodeProvider(UpdateNodeProvider arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "update_node_provider", arg);
			return reply.ToObjects<Result>();
		}
	}
}