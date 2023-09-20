using Dict = System.Collections.Generic.List<(System.String, EdjCase.ICP.Candid.Models.UnboundedUInt)>;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid;
using System.Threading.Tasks;
using Sample.Shared.AddressBook;
using EdjCase.ICP.Agent.Responses;

namespace Sample.Shared.AddressBook
{
	public class AddressBookApiClient
	{
		public IAgent Agent { get; }

		public Principal CanisterId { get; }

		public CandidConverter? Converter { get; }

		public AddressBookApiClient(IAgent agent, Principal canisterId, CandidConverter? converter = default)
		{
			this.Agent = agent;
			this.CanisterId = canisterId;
			this.Converter = converter;
		}

		public async Task set_address(string name, Address addr)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(name), CandidTypedValue.FromObject(addr));
			await this.Agent.CallAndWaitAsync(this.CanisterId, "set_address", arg);
		}

		public async Task<OptionalValue<Address>> get_address(string name)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(name));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_address", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<OptionalValue<Address>>(this.Converter);
		}

		public async Task<Dict> get_dict()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "get_dict", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Dict>(this.Converter);
		}
	}
}