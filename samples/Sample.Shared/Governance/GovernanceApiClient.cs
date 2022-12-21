using System;
using System.Threading.Tasks;
using System.Collections.Generic;
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
		public async Task<Result> claim_gtc_neurons(EdjCase.ICP.Candid.Models.Principal arg0, List<NeuronId> arg1, IIdentity? identityOverride = null)
		{
			string method = "claim_gtc_neurons";
			CandidValueWithType p0 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.Models.Principal>(arg0, false);
			CandidValueWithType p1 = CandidValueWithType.FromObject<List<NeuronId>>(arg1, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			Result r0 = responseArg.Values[0].ToObject<Result>();
			return (r0);
		}
		public async Task<ClaimOrRefreshNeuronFromAccountResponse> claim_or_refresh_neuron_from_account(ClaimOrRefreshNeuronFromAccount arg0, IIdentity? identityOverride = null)
		{
			string method = "claim_or_refresh_neuron_from_account";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ClaimOrRefreshNeuronFromAccount>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			ClaimOrRefreshNeuronFromAccountResponse r0 = responseArg.Values[0].ToObject<ClaimOrRefreshNeuronFromAccountResponse>();
			return (r0);
		}
		public async Task<string> get_build_metadata(IIdentity? identityOverride = null)
		{
			string method = "get_build_metadata";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			string r0 = reply.Arg.Values[0].ToObject<string>();
			return (r0);
		}
		public async Task<Result_2> get_full_neuron(ulong arg0, IIdentity? identityOverride = null)
		{
			string method = "get_full_neuron";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ulong>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			Result_2 r0 = reply.Arg.Values[0].ToObject<Result_2>();
			return (r0);
		}
		public async Task<Result_2> get_full_neuron_by_id_or_subaccount(NeuronIdOrSubaccount arg0, IIdentity? identityOverride = null)
		{
			string method = "get_full_neuron_by_id_or_subaccount";
			CandidValueWithType p0 = CandidValueWithType.FromObject<NeuronIdOrSubaccount>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			Result_2 r0 = reply.Arg.Values[0].ToObject<Result_2>();
			return (r0);
		}
		public async Task<Result_3> get_monthly_node_provider_rewards(IIdentity? identityOverride = null)
		{
			string method = "get_monthly_node_provider_rewards";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			Result_3 r0 = responseArg.Values[0].ToObject<Result_3>();
			return (r0);
		}
		public async Task<NetworkEconomics> get_network_economics_parameters(IIdentity? identityOverride = null)
		{
			string method = "get_network_economics_parameters";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			NetworkEconomics r0 = reply.Arg.Values[0].ToObject<NetworkEconomics>();
			return (r0);
		}
		public async Task<List<ulong>> get_neuron_ids(IIdentity? identityOverride = null)
		{
			string method = "get_neuron_ids";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			List<ulong> r0 = reply.Arg.Values[0].ToObject<List<ulong>>();
			return (r0);
		}
		public async Task<Result_4> get_neuron_info(ulong arg0, IIdentity? identityOverride = null)
		{
			string method = "get_neuron_info";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ulong>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			Result_4 r0 = reply.Arg.Values[0].ToObject<Result_4>();
			return (r0);
		}
		public async Task<Result_4> get_neuron_info_by_id_or_subaccount(NeuronIdOrSubaccount arg0, IIdentity? identityOverride = null)
		{
			string method = "get_neuron_info_by_id_or_subaccount";
			CandidValueWithType p0 = CandidValueWithType.FromObject<NeuronIdOrSubaccount>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			Result_4 r0 = reply.Arg.Values[0].ToObject<Result_4>();
			return (r0);
		}
		public async Task<Result_5> get_node_provider_by_caller(IIdentity? identityOverride = null)
		{
			string method = "get_node_provider_by_caller";
			CandidValueWithType p0 = CandidValueWithType.Null();
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			Result_5 r0 = reply.Arg.Values[0].ToObject<Result_5>();
			return (r0);
		}
		public async Task<List<ProposalInfo>> get_pending_proposals(IIdentity? identityOverride = null)
		{
			string method = "get_pending_proposals";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			List<ProposalInfo> r0 = reply.Arg.Values[0].ToObject<List<ProposalInfo>>();
			return (r0);
		}
		public async Task<ProposalInfo?> get_proposal_info(ulong arg0, IIdentity? identityOverride = null)
		{
			string method = "get_proposal_info";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ulong>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			ProposalInfo? r0 = reply.Arg.Values[0].ToObjectOrDefault<ProposalInfo?>();
			return (r0);
		}
		public async Task<ListKnownNeuronsResponse> list_known_neurons(IIdentity? identityOverride = null)
		{
			string method = "list_known_neurons";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			ListKnownNeuronsResponse r0 = reply.Arg.Values[0].ToObject<ListKnownNeuronsResponse>();
			return (r0);
		}
		public async Task<ListNeuronsResponse> list_neurons(ListNeurons arg0, IIdentity? identityOverride = null)
		{
			string method = "list_neurons";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ListNeurons>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			ListNeuronsResponse r0 = reply.Arg.Values[0].ToObject<ListNeuronsResponse>();
			return (r0);
		}
		public async Task<ListNodeProvidersResponse> list_node_providers(IIdentity? identityOverride = null)
		{
			string method = "list_node_providers";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			ListNodeProvidersResponse r0 = reply.Arg.Values[0].ToObject<ListNodeProvidersResponse>();
			return (r0);
		}
		public async Task<ListProposalInfoResponse> list_proposals(ListProposalInfo arg0, IIdentity? identityOverride = null)
		{
			string method = "list_proposals";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ListProposalInfo>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			ListProposalInfoResponse r0 = reply.Arg.Values[0].ToObject<ListProposalInfoResponse>();
			return (r0);
		}
		public async Task<ManageNeuronResponse> manage_neuron(ManageNeuron arg0, IIdentity? identityOverride = null)
		{
			string method = "manage_neuron";
			CandidValueWithType p0 = CandidValueWithType.FromObject<ManageNeuron>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			ManageNeuronResponse r0 = responseArg.Values[0].ToObject<ManageNeuronResponse>();
			return (r0);
		}
		public async Task<Result> transfer_gtc_neuron(NeuronId arg0, NeuronId arg1, IIdentity? identityOverride = null)
		{
			string method = "transfer_gtc_neuron";
			CandidValueWithType p0 = CandidValueWithType.FromObject<NeuronId>(arg0, false);
			CandidValueWithType p1 = CandidValueWithType.FromObject<NeuronId>(arg1, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			Result r0 = responseArg.Values[0].ToObject<Result>();
			return (r0);
		}
		public async Task<Result> update_node_provider(UpdateNodeProvider arg0, IIdentity? identityOverride = null)
		{
			string method = "update_node_provider";
			CandidValueWithType p0 = CandidValueWithType.FromObject<UpdateNodeProvider>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			Result r0 = responseArg.Values[0].ToObject<Result>();
			return (r0);
		}
	}
}

