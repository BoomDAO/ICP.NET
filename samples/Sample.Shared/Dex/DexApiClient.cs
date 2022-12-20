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
		public DexApiClient(IAgent agent, Principal canisterId)
		{
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
		}
		public async Task<CancelOrderReceipt> cancelOrder(OrderId arg0, IIdentity? identityOverride = null)
		{
			string method = "cancelOrder";
			CandidTypedValue p0 = CandidTypedValue.FromObject<OrderId>(arg0, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			CancelOrderReceipt r0 = responseArg.Values[0].ToObject<CancelOrderReceipt>();
			return (r0);
		}
		public async Task clear(IIdentity? identityOverride = null)
		{
			string method = "clear";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
		}
		public async Task credit(EdjCase.ICP.Candid.Models.Principal arg0, Token arg1, EdjCase.ICP.Candid.UnboundedUInt arg2, IIdentity? identityOverride = null)
		{
			string method = "credit";
			CandidTypedValue p0 = CandidTypedValue.FromObject<EdjCase.ICP.Candid.Models.Principal>(arg0, false);
			CandidTypedValue p1 = CandidTypedValue.FromObject<Token>(arg1, false);
			CandidTypedValue p2 = CandidTypedValue.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg2, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
		}
		public async Task<DepositReceipt> deposit(Token arg0, IIdentity? identityOverride = null)
		{
			string method = "deposit";
			CandidTypedValue p0 = CandidTypedValue.FromObject<Token>(arg0, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			DepositReceipt r0 = responseArg.Values[0].ToObject<DepositReceipt>();
			return (r0);
		}
		public async Task<List<Balance>> getAllBalances(IIdentity? identityOverride = null)
		{
			string method = "getAllBalances";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			List<Balance> r0 = reply.Arg.Values[0].ToObject<List<Balance>>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.UnboundedUInt> getBalance(Token arg0, IIdentity? identityOverride = null)
		{
			string method = "getBalance";
			CandidTypedValue p0 = CandidTypedValue.FromObject<Token>(arg0, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.UnboundedUInt r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.UnboundedUInt>();
			return (r0);
		}
		public async Task<List<Balance>> getBalances(IIdentity? identityOverride = null)
		{
			string method = "getBalances";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			List<Balance> r0 = reply.Arg.Values[0].ToObject<List<Balance>>();
			return (r0);
		}
		public async Task<List<byte>> getDepositAddress(IIdentity? identityOverride = null)
		{
			string method = "getDepositAddress";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			List<byte> r0 = responseArg.Values[0].ToObject<List<byte>>();
			return (r0);
		}
		public async Task<Order?> getOrder(OrderId arg0, IIdentity? identityOverride = null)
		{
			string method = "getOrder";
			CandidTypedValue p0 = CandidTypedValue.FromObject<OrderId>(arg0, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			Order? r0 = responseArg.Values[0].ToObjectOrDefault<Order?>();
			return (r0);
		}
		public async Task<List<Order>> getOrders(IIdentity? identityOverride = null)
		{
			string method = "getOrders";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			List<Order> r0 = responseArg.Values[0].ToObject<List<Order>>();
			return (r0);
		}
		public async Task<string> getSymbol(Token arg0, IIdentity? identityOverride = null)
		{
			string method = "getSymbol";
			CandidTypedValue p0 = CandidTypedValue.FromObject<Token>(arg0, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			string r0 = responseArg.Values[0].ToObject<string>();
			return (r0);
		}
		public async Task<OrderPlacementReceipt> placeOrder(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, Token arg2, EdjCase.ICP.Candid.UnboundedUInt arg3, IIdentity? identityOverride = null)
		{
			string method = "placeOrder";
			CandidTypedValue p0 = CandidTypedValue.FromObject<Token>(arg0, false);
			CandidTypedValue p1 = CandidTypedValue.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg1, false);
			CandidTypedValue p2 = CandidTypedValue.FromObject<Token>(arg2, false);
			CandidTypedValue p3 = CandidTypedValue.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg3, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
				p3,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			OrderPlacementReceipt r0 = responseArg.Values[0].ToObject<OrderPlacementReceipt>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.Models.Principal> whoami(IIdentity? identityOverride = null)
		{
			string method = "whoami";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.Models.Principal r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.Models.Principal>();
			return (r0);
		}
		public async Task<WithdrawReceipt> withdraw(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, EdjCase.ICP.Candid.Models.Principal arg2, IIdentity? identityOverride = null)
		{
			string method = "withdraw";
			CandidTypedValue p0 = CandidTypedValue.FromObject<Token>(arg0, false);
			CandidTypedValue p1 = CandidTypedValue.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg1, false);
			CandidTypedValue p2 = CandidTypedValue.FromObject<EdjCase.ICP.Candid.Models.Principal>(arg2, false);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			WithdrawReceipt r0 = responseArg.Values[0].ToObject<WithdrawReceipt>();
			return (r0);
		}
	}
}
