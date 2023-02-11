using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Standards.ICRC1.Models;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Standards.ICRC1
{
	public class ICRC1Client
	{
		public IAgent Agent { get; }

		public Principal CanisterId { get; }

		public ICRC1Client(IAgent agent, Principal canisterId)
		{
			this.Agent = agent;
			this.CanisterId = canisterId;
		}

		public async Task<List<MetaData>> MetaData()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_meta_data", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<MetaData>>();
		}

		public async Task<string> Name()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_name", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<string>();
		}

		public async Task<string> Symbol()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_symbol", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<string>();
		}

		public async Task<int> Decimals()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_decimals", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<byte>();
		}

		public async Task<UnboundedUInt> Fee()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_fee", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<UnboundedUInt>();
		}

		public async Task<UnboundedUInt> TotalSupply()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_total_supply", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<UnboundedUInt>();
		}

		public async Task<OptionalValue<Account>> MintingAccount()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_minting_account", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<OptionalValue<Account>>();
		}

		public async Task<UnboundedUInt> BalanceOf(Account arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_balance_of", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<UnboundedUInt>();
		}

		public async Task<TransferResult> Transfer(TransferArgs arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "icrc1_transfer", arg);
			return reply.ToObjects<TransferResult>();
		}

		public async Task<List<SupportedStandard>> SupportedStandards()
		{
			CandidArg arg = CandidArg.FromCandid();
			Responses.QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "icrc1_supported_standards", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<SupportedStandard>>();
		}
	}

	public class SupportedStandard
	{
		[CandidName("name")]
		public string Name { get; }

		[CandidName("url")]
		public string Url { get; }
	}

	public class MetaData
	{
		[CandidName("0")]
		public string Key { get; set; }

		[CandidName("1")]
		public MetaDataValue Value { get; set; }

		public MetaData(string key, MetaDataValue value)
		{
			this.Key = key;
			this.Value = value;
		}
	}

	public class MetaDataValue
	{

	}

}