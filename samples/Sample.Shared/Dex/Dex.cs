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
		public async Task<CancelOrderReceipt> CancelOrderAsync(OrderId arg0, IIdentity? identityOverride = null)
		{
			string method = "cancelOrder";
			CandidValueWithType p0 = CandidValueWithType.FromObject<OrderId>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			CancelOrderReceipt r0 = responseArg.Values[0].ToObject<CancelOrderReceipt>();
			return (r0);
		}
		public async Task ClearAsync(IIdentity? identityOverride = null)
		{
			string method = "clear";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
		}
		public async Task CreditAsync(EdjCase.ICP.Candid.Models.Principal arg0, Token arg1, EdjCase.ICP.Candid.UnboundedUInt arg2, IIdentity? identityOverride = null)
		{
			string method = "credit";
			CandidValueWithType p0 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.Models.Principal>(arg0);
			CandidValueWithType p1 = CandidValueWithType.FromObject<Token>(arg1);
			CandidValueWithType p2 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg2);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
		}
		public async Task<DepositReceipt> DepositAsync(Token arg0, IIdentity? identityOverride = null)
		{
			string method = "deposit";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			DepositReceipt r0 = responseArg.Values[0].ToObject<DepositReceipt>();
			return (r0);
		}
		public async Task<List<Balance>> GetAllBalancesAsync(IIdentity? identityOverride = null)
		{
			string method = "getAllBalances";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			List<Balance> r0 = reply.Arg.Values[0].ToObject<List<Balance>>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.UnboundedUInt> GetBalanceAsync(Token arg0, IIdentity? identityOverride = null)
		{
			string method = "getBalance";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.UnboundedUInt r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.UnboundedUInt>();
			return (r0);
		}
		public async Task<List<Balance>> GetBalancesAsync(IIdentity? identityOverride = null)
		{
			string method = "getBalances";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			List<Balance> r0 = reply.Arg.Values[0].ToObject<List<Balance>>();
			return (r0);
		}
		public async Task<List<byte>> GetDepositAddressAsync(IIdentity? identityOverride = null)
		{
			string method = "getDepositAddress";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			List<byte> r0 = responseArg.Values[0].ToObject<List<byte>>();
			return (r0);
		}
		public async Task<Order?> GetOrderAsync(OrderId arg0, IIdentity? identityOverride = null)
		{
			string method = "getOrder";
			CandidValueWithType p0 = CandidValueWithType.FromObject<OrderId>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			Order? r0 = responseArg.Values[0].ToObjectOrDefault<Order?>();
			return (r0);
		}
		public async Task<List<Order>> GetOrdersAsync(IIdentity? identityOverride = null)
		{
			string method = "getOrders";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			List<Order> r0 = responseArg.Values[0].ToObject<List<Order>>();
			return (r0);
		}
		public async Task<string> GetSymbolAsync(Token arg0, IIdentity? identityOverride = null)
		{
			string method = "getSymbol";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			string r0 = responseArg.Values[0].ToObject<string>();
			return (r0);
		}
		public async Task<OrderPlacementReceipt> PlaceOrderAsync(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, Token arg2, EdjCase.ICP.Candid.UnboundedUInt arg3, IIdentity? identityOverride = null)
		{
			string method = "placeOrder";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			CandidValueWithType p1 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg1);
			CandidValueWithType p2 = CandidValueWithType.FromObject<Token>(arg2);
			CandidValueWithType p3 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg3);
			var candidArgs = new List<CandidValueWithType>
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
		public async Task<EdjCase.ICP.Candid.Models.Principal> WhoamiAsync(IIdentity? identityOverride = null)
		{
			string method = "whoami";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.Models.Principal r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.Models.Principal>();
			return (r0);
		}
		public async Task<WithdrawReceipt> WithdrawAsync(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, EdjCase.ICP.Candid.Models.Principal arg2, IIdentity? identityOverride = null)
		{
			string method = "withdraw";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			CandidValueWithType p1 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.UnboundedUInt>(arg1);
			CandidValueWithType p2 = CandidValueWithType.FromObject<EdjCase.ICP.Candid.Models.Principal>(arg2);
			var candidArgs = new List<CandidValueWithType>
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
