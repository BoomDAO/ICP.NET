using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using System.Numerics;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{
	public class IIConnection
	{
		public static readonly Principal InternetIdentityBackendCanister = Principal.FromText("rdmx6-jaaaa-aaaaa-aaadq-cai");

		public readonly Principal canisterId;

		public IAgent agent
		{
			get
			{
				return this._agent ?? (this._agent = new HttpAgent(new AnonymousIdentity()));
			}
		}

		public IIConnection(Principal? canisterId = null)
		{
		
		}

		public async Task Login(BigInteger userNumber)
		{
			throw new System.NotImplementedException();
		}

		private IAgent _agent;
	}
}
