using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Dex;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Dex
{
	public class DexApiClient
	{
		public IAgent Agent { get; }

		public Principal CanisterId { get; }

		public CandidConverter? CandidConverter { get; }

		public DexApiClient(IAgent agent, Principal canisterId, CandidConverter? converter = null)
		{
			this.Agent = agent;
			this.CanisterId = canisterId;
			this.CandidConverter = converter;
		}

		public async System.Threading.Tasks.Task<Models.CancelOrderReceipt> CancelOrder(OrderId arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "cancelOrder", arg);
			return reply.ToObjects<Models.CancelOrderReceipt>(this.CandidConverter);
		}

		public async Task Clear()
		{
			CandidArg arg = CandidArg.FromCandid();
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "clear", arg);
		}

		public async Task Credit(Principal arg0, Token arg1, UnboundedUInt arg2)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0), CandidTypedValue.FromObject(arg1), CandidTypedValue.FromObject(arg2));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "credit", arg);
		}

		public async System.Threading.Tasks.Task<Models.DepositReceipt> Deposit(Token arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "deposit", arg);
			return reply.ToObjects<Models.DepositReceipt>();
		}

		public async System.Threading.Tasks.Task<List<Models.Balance>> GetAllBalances()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getAllBalances", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<Models.Balance>>();
		}

		public async System.Threading.Tasks.Task<UnboundedUInt> GetBalance(Token arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getBalance", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<UnboundedUInt>();
		}

		public async System.Threading.Tasks.Task<List<Models.Balance>> GetBalances()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getBalances", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<Models.Balance>>();
		}

		public async System.Threading.Tasks.Task<List<byte>> GetDepositAddress()
		{
			CandidArg arg = CandidArg.FromCandid();
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "getDepositAddress", arg);
			return reply.ToObjects<List<byte>>();
		}

		public async System.Threading.Tasks.Task<OptionalValue<Models.Order>> GetOrder(OrderId arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "getOrder", arg);
			return reply.ToObjects<OptionalValue<Models.Order>>();
		}

		public async System.Threading.Tasks.Task<List<Models.Order>> GetOrders()
		{
			CandidArg arg = CandidArg.FromCandid();
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "getOrders", arg);
			return reply.ToObjects<List<Models.Order>>();
		}

		public async System.Threading.Tasks.Task<string> GetSymbol(Token arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "getSymbol", arg);
			return reply.ToObjects<string>();
		}

		public async System.Threading.Tasks.Task<Models.OrderPlacementReceipt> PlaceOrder(Token arg0, UnboundedUInt arg1, Token arg2, UnboundedUInt arg3)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0), CandidTypedValue.FromObject(arg1), CandidTypedValue.FromObject(arg2), CandidTypedValue.FromObject(arg3));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "placeOrder", arg);
			return reply.ToObjects<Models.OrderPlacementReceipt>();
		}

		public async System.Threading.Tasks.Task<Principal> Whoami()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "whoami", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Principal>();
		}

		public async System.Threading.Tasks.Task<Models.WithdrawReceipt> Withdraw(Token arg0, UnboundedUInt arg1, Principal arg2)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0), CandidTypedValue.FromObject(arg1), CandidTypedValue.FromObject(arg2));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "withdraw", arg);
			return reply.ToObjects<Models.WithdrawReceipt>();
		}
	}
}