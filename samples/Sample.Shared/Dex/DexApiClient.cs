using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Dex.Models;

namespace Sample.Shared.Dex
{
	public class DexApiClient
	{
		public IAgent Agent { get; }
		public Principal CanisterId { get; }
		public DexApiClient(EdjCase.ICP.Agent.Agents.IAgent agent, EdjCase.ICP.Candid.Models.Principal canisterId)
		{
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
		}
		public async Task<CancelOrderReceipt> CancelOrder(OrderId arg0)
		{
			string method = "cancelOrder";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			CancelOrderReceipt r0 = responseArg.Values[0].ToObject<CancelOrderReceipt>();
			return (r0);
		}
		public async Task Clear()
		{
			string method = "clear";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
		}
		public async Task Credit(EdjCase.ICP.Candid.Models.Principal arg0, Token arg1, UnboundedUInt arg2)
		{
			string method = "credit";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			CandidTypedValue p2 = CandidTypedValue.FromObject(arg2);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
		}
		public async Task<DepositReceipt> Deposit(Token arg0)
		{
			string method = "deposit";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg);
			DepositReceipt r0 = responseArg.Values[0].ToObject<DepositReceipt>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<Balance>> GetAllBalances()
		{
			string method = "getAllBalances";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			CandidArg reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<Balance> r0 = reply.Values[0].ToObject<System.Collections.Generic.List<Balance>>();
			return (r0);
		}
		public async Task<UnboundedUInt> GetBalance(Token arg0)
		{
			string method = "getBalance";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			CandidArg reply = response.ThrowOrGetReply();
			UnboundedUInt r0 = reply.Values[0].ToObject<UnboundedUInt>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<Balance>> GetBalances()
		{
			string method = "getBalances";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			CandidArg reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<Balance> r0 = reply.Values[0].ToObject<System.Collections.Generic.List<Balance>>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<byte>> GetDepositAddress()
		{
			string method = "getDepositAddress";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			System.Collections.Generic.List<byte> r0 = responseArg.Values[0].ToObject<System.Collections.Generic.List<byte>>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.Models.OptionalValue<Order>> GetOrder(OrderId arg0)
		{
			string method = "getOrder";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			EdjCase.ICP.Candid.Models.OptionalValue<Order> r0 = responseArg.Values[0].ToObject<EdjCase.ICP.Candid.Models.OptionalValue<Order>>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<Order>> GetOrders()
		{
			string method = "getOrders";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			System.Collections.Generic.List<Order> r0 = responseArg.Values[0].ToObject<System.Collections.Generic.List<Order>>();
			return (r0);
		}
		public async Task<string> GetSymbol(Token arg0)
		{
			string method = "getSymbol";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			string r0 = responseArg.Values[0].ToObject<string>();
			return (r0);
		}
		public async Task<OrderPlacementReceipt> PlaceOrder(Token arg0, UnboundedUInt arg1, Token arg2, UnboundedUInt arg3)
		{
			string method = "placeOrder";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			CandidTypedValue p2 = CandidTypedValue.FromObject(arg2);
			CandidTypedValue p3 = CandidTypedValue.FromObject(arg3);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
				p3,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			OrderPlacementReceipt r0 = responseArg.Values[0].ToObject<OrderPlacementReceipt>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.Models.Principal> Whoami()
		{
			string method = "whoami";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			CandidArg reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.Models.Principal r0 = reply.Values[0].ToObject<EdjCase.ICP.Candid.Models.Principal>();
			return (r0);
		}
		public async Task<WithdrawReceipt> Withdraw(Token arg0, UnboundedUInt arg1, EdjCase.ICP.Candid.Models.Principal arg2)
		{
			string method = "withdraw";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			CandidTypedValue p2 = CandidTypedValue.FromObject(arg2);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			WithdrawReceipt r0 = responseArg.Values[0].ToObject<WithdrawReceipt>();
			return (r0);
		}
	}
}

