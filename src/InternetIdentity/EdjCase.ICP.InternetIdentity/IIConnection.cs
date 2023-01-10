using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIClient = EdjCase.ICP.InternetIdentity.Models;

namespace EdjCase.ICP.InternetIdentity
{
	public class UnknownUserException : System.Exception
	{
		public readonly ulong userNumber;

		public UnknownUserException(ulong userNumber)
		{
			this.userNumber = userNumber;
		}
	}

	public class IIConnection
	{
		public static readonly Principal InternetIdentityBackendCanister = Principal.FromText("rdmx6-jaaaa-aaaaa-aaadq-cai");

		public IAgent agent
		{
			get
			{
				return this._agent ?? (this._agent = new HttpAgent(new AnonymousIdentity()));
			}
		}
		
		public InternetIdentityApiClient client
		{
			get
			{
				return this._client ?? (this._client = new InternetIdentityApiClient(this.agent, InternetIdentityBackendCanister));
			}
		}

		public IIConnection()
		{
		}

		public async Task<DelegationIdentity> Login(ulong userNumber, Principal targetCanisterId)
		{
			var authenticatorDevices = await this.LookupAuthenticators(userNumber);
			if (authenticatorDevices.FirstOrDefault() == null)
			{
				throw new UnknownUserException(userNumber);
			}

			var userIdentity = MultiWebAuthnIdentity.FromDevices(authenticatorDevices);
			return await this.RequestNewDelegation(userIdentity, targetCanisterId);
		}

		public async Task<IEnumerable<IIClient.DeviceData>> LookupAuthenticators(ulong userNumber)
		{
			var devices = await this.client.Lookup(userNumber);
			return devices.Where(d => d.Purpose.Tag == IIClient.PurposeTag.Authentication);
		}

		// this is responsible for converting a "direct" user identity, which identifies the user,
		// and can be used to sign for all canisters, into a delegation identity, which is anonymized and
		// can only be used to sign for a specific target canister.
		//
		// corresponds to `requestFEDelegation' from @dfinity/internet-identity
		public async Task<DelegationIdentity> RequestNewDelegation(SigningIdentityBase userSignIdentity, Principal targetCanisterId)
		{
			var sessionKey = ED25519Identity.Generate();
			var tenMinutesInNanoSeconds = 600 * 1_000_000_000ul;
			var delegationChain = DelegationChain.Create(
				userSignIdentity, sessionKey.PublicKey,
				ICTimestamp.FromNanoSecondsInFuture(tenMinutesInNanoSeconds),
				principalIds: new List<Principal> { targetCanisterId }
			);
			return new DelegationIdentity(sessionKey, delegationChain);
		}

		private IAgent? _agent;
		private InternetIdentityApiClient? _client;
	}
}
