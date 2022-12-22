using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Agent.Auth;
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
		public async Task<CancelOrderReceipt> cancelOrder(OrderId arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
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
		public async Task clear(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "clear";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
		}
		public async Task credit(EdjCase.ICP.Candid.Models.Principal arg0, Token arg1, EdjCase.ICP.Candid.UnboundedUInt arg2, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
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
		public async Task<DepositReceipt> deposit(Token arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "deposit";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			DepositReceipt r0 = responseArg.Values[0].ToObject<DepositReceipt>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<Balance>> getAllBalances(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "getAllBalances";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<Balance> r0 = reply.Arg.Values[0].ToObject<System.Collections.Generic.List<Balance>>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.UnboundedUInt> getBalance(Token arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "getBalance";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.UnboundedUInt r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.UnboundedUInt>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<Balance>> getBalances(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "getBalances";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<Balance> r0 = reply.Arg.Values[0].ToObject<System.Collections.Generic.List<Balance>>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<byte>> getDepositAddress(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
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
		public async Task<EdjCase.ICP.Candid.Models.OptionalValue<Order>> getOrder(OrderId arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "getOrder";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			EdjCase.ICP.Candid.Models.OptionalValue<Order> r0 = responseArg.Values[0].ToObjectOrDefault<EdjCase.ICP.Candid.Models.OptionalValue<Order>>();
			return (r0);
		}
		public async Task<System.Collections.Generic.List<Order>> getOrders(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
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
		public async Task<string> getSymbol(Token arg0, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
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
		public async Task<OrderPlacementReceipt> placeOrder(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, Token arg2, EdjCase.ICP.Candid.UnboundedUInt arg3, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
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
		public async Task<EdjCase.ICP.Candid.Models.Principal> whoami(EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
		{
			string method = "whoami";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.Models.Principal r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.Models.Principal>();
			return (r0);
		}
		public async Task<WithdrawReceipt> withdraw(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, EdjCase.ICP.Candid.Models.Principal arg2, EdjCase.ICP.Agent.Auth.IIdentity? identityOverride = null)
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

