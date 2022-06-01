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
		public DexApiClient(IAgent agent, Principal canisterId)
		{
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
		}
		public async Task<CancelOrderReceipt> CancelOrder(OrderId arg0)
		{
			string method = "cancelOrder";
			CandidValueWithType p0 = CandidValueWithType.FromObject<OrderId>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			CancelOrderReceipt r0 = reply.Arg.Values[0].ToObject<CancelOrderReceipt>();
			return (r0);
		}
		public async Task Clear()
		{
			string method = "clear";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
		}
		public async Task Credit(EdjCase.ICP.Candid.Models.Principal arg0, Token arg1, EdjCase.ICP.Candid.UnboundedUInt arg2)
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
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
		}
		public async Task<DepositReceipt> Deposit(Token arg0)
		{
			string method = "deposit";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			DepositReceipt r0 = reply.Arg.Values[0].ToObject<DepositReceipt>();
			return (r0);
		}
		public async Task<List<Balance>> GetAllBalances()
		{
			string method = "getAllBalances";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			List<Balance> r0 = reply.Arg.Values[0].ToObject<List<Balance>>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.UnboundedUInt> GetBalance(Token arg0)
		{
			string method = "getBalance";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.UnboundedUInt r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.UnboundedUInt>();
			return (r0);
		}
		public async Task<List<Balance>> GetBalances()
		{
			string method = "getBalances";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			List<Balance> r0 = reply.Arg.Values[0].ToObject<List<Balance>>();
			return (r0);
		}
		public async Task<List<System.Byte>> GetDepositAddress()
		{
			string method = "getDepositAddress";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			List<System.Byte> r0 = reply.Arg.Values[0].ToObject<List<System.Byte>>();
			return (r0);
		}
		public async Task<Order?> GetOrder(OrderId arg0)
		{
			string method = "getOrder";
			CandidValueWithType p0 = CandidValueWithType.FromObject<OrderId>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			Order? r0 = reply.Arg.Values[0].ToObject<Order?>();
			return (r0);
		}
		public async Task<List<Order>> GetOrders()
		{
			string method = "getOrders";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			List<Order> r0 = reply.Arg.Values[0].ToObject<List<Order>>();
			return (r0);
		}
		public async Task<System.String> GetSymbol(Token arg0)
		{
			string method = "getSymbol";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Token>(arg0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			System.String r0 = reply.Arg.Values[0].ToObject<System.String>();
			return (r0);
		}
		public async Task<OrderPlacementReceipt> PlaceOrder(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, Token arg2, EdjCase.ICP.Candid.UnboundedUInt arg3)
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
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			OrderPlacementReceipt r0 = reply.Arg.Values[0].ToObject<OrderPlacementReceipt>();
			return (r0);
		}
		public async Task<EdjCase.ICP.Candid.Models.Principal> Whoami()
		{
			string method = "whoami";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.Models.Principal r0 = reply.Arg.Values[0].ToObject<EdjCase.ICP.Candid.Models.Principal>();
			return (r0);
		}
		public async Task<WithdrawReceipt> Withdraw(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, EdjCase.ICP.Candid.Models.Principal arg2)
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
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			WithdrawReceipt r0 = reply.Arg.Values[0].ToObject<WithdrawReceipt>();
			return (r0);
		}
	}
}
