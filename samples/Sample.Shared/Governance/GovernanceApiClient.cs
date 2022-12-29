using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
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
		public GovernanceApiClient(EdjCase.ICP.Agent.Agents.IAgent agent, EdjCase.ICP.Candid.Models.Principal canisterId)
		{
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
		}
		public async Task<Result> ClaimGtcNeurons(EdjCase.ICP.Candid.Models.Principal arg0, System.Collections.Generic.List<NeuronId> arg1, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "claim_gtc_neurons";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			Result r0 = responseArg.Values[0].ToObject<Result>();
			return (r0);
		}
		public async Task<ClaimOrRefreshNeuronFromAccountResponse> ClaimOrRefreshNeuronFromAccount(ClaimOrRefreshNeuronFromAccount arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "claim_or_refresh_neuron_from_account";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			ClaimOrRefreshNeuronFromAccountResponse r0 = responseArg.Values[0].ToObject<ClaimOrRefreshNeuronFromAccountResponse>();
			return (r0);
		}
		public async Task<string> GetBuildMetadata(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_build_metadata";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			string r0 = reply.Arg.Values[0].ToObject<string>();
			return (r0);
		}
		public async Task<Result2> GetFullNeuron(ulong arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_full_neuron";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			Result2 r0 = reply.Arg.Values[0].ToObject<Result2>();
			return (r0);
		}
		public async Task<Result2> GetFullNeuronByIdOrSubaccount(NeuronIdOrSubaccount arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_full_neuron_by_id_or_subaccount";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			Result2 r0 = reply.Arg.Values[0].ToObject<Result2>();
			return (r0);
		}
		public async Task<Result3> GetMonthlyNodeProviderRewards(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_monthly_node_provider_rewards";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			Result3 r0 = responseArg.Values[0].ToObject<Result3>();
			return (r0);
		}
		public async Task<NetworkEconomics> GetNetworkEconomicsParameters(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_network_economics_parameters";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			NetworkEconomics r0 = reply.Arg.Values[0].ToObject<NetworkEconomics>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<ulong>> GetNeuronIds(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_neuron_ids";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<ulong> r0 = reply.Arg.Values[0].ToObject<System.Collections.Generic.List<ulong>>();
			return (r0);
		}
		public async Task<Result4> GetNeuronInfo(ulong arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_neuron_info";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			Result4 r0 = reply.Arg.Values[0].ToObject<Result4>();
			return (r0);
		}
		public async Task<Result4> GetNeuronInfoByIdOrSubaccount(NeuronIdOrSubaccount arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_neuron_info_by_id_or_subaccount";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			Result4 r0 = reply.Arg.Values[0].ToObject<Result4>();
			return (r0);
		}
		public async Task<Result5> GetNodeProviderByCaller(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_node_provider_by_caller";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			Result5 r0 = reply.Arg.Values[0].ToObject<Result5>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<ProposalInfo>> GetPendingProposals(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_pending_proposals";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<ProposalInfo> r0 = reply.Arg.Values[0].ToObject<System.Collections.Generic.List<ProposalInfo>>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.Models.OptionalValue<ProposalInfo>> GetProposalInfo(ulong arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "get_proposal_info";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.Models.OptionalValue<ProposalInfo> r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.Models.OptionalValue<ProposalInfo>>();
			return (r0);
		}
		public async Task<ListKnownNeuronsResponse> ListKnownNeurons(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "list_known_neurons";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			ListKnownNeuronsResponse r0 = reply.Arg.Values[0].ToObject<ListKnownNeuronsResponse>();
			return (r0);
		}
		public async Task<ListNeuronsResponse> ListNeurons(ListNeurons arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "list_neurons";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			ListNeuronsResponse r0 = reply.Arg.Values[0].ToObject<ListNeuronsResponse>();
			return (r0);
		}
		public async Task<ListNodeProvidersResponse> ListNodeProviders(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "list_node_providers";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			ListNodeProvidersResponse r0 = reply.Arg.Values[0].ToObject<ListNodeProvidersResponse>();
			return (r0);
		}
		public async Task<ListProposalInfoResponse> ListProposals(ListProposalInfo arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "list_proposals";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			ListProposalInfoResponse r0 = reply.Arg.Values[0].ToObject<ListProposalInfoResponse>();
			return (r0);
		}
		public async Task<ManageNeuronResponse> ManageNeuron(ManageNeuron arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "manage_neuron";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			ManageNeuronResponse r0 = responseArg.Values[0].ToObject<ManageNeuronResponse>();
			return (r0);
		}
		public async Task<Result> TransferGtcNeuron(NeuronId arg0, NeuronId arg1, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "transfer_gtc_neuron";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			Result r0 = responseArg.Values[0].ToObject<Result>();
			return (r0);
		}
		public async Task<Result> UpdateNodeProvider(UpdateNodeProvider arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "update_node_provider";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			Result r0 = responseArg.Values[0].ToObject<Result>();
			return (r0);
		}
	}
}

