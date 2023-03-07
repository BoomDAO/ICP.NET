using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using System.Threading.Tasks;
using Sample.Shared.AddressBook;
using EdjCase.ICP.Agent.Responses;

namespace Sample.Shared.AddressBook
{
	public class AddressBookApiClient
	{
		public IAgent Agent { get; }

		public Principal CanisterId { get; }

		public AddressBookApiClient(IAgent agent, Principal canisterId)
		{
			this.Agent = agent;
			this.CanisterId = canisterId;
		}

		public async Task SetAddress(string name, Models.Address addr)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(name), CandidTypedValue.FromObject(addr));
			CandidArg reply = await this.Agent.CallAndWaitAsync(this.CanisterId, "set_address", arg);
		}

		public async System.Threading.Tasks.Task<OptionalValue<Models.Address>> GetAddress(string name)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(name));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_address", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<OptionalValue<Models.Address>>();
		}
	}
}