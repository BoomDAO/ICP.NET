using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIClient = EdjCase.ICP.InternetIdentity.Models;

namespace EdjCase.ICP.InternetIdentity
{
	public class UnknownUserException : System.Exception
	{
		public ulong UserNumber { get; }

		public UnknownUserException(ulong userNumber)
		{
			this.UserNumber = userNumber;
		}
	}

	public static class IIModelConvertExt
	{
		public static Delegation Convert(this IIClient.Delegation delegation)
		{
			return new Delegation(
				delegation.Pubkey.ToArray(),
				ICTimestamp.FromNanoSeconds(delegation.Expiration),
				delegation.Targets.GetValueOrDefault()
			);
		}

		public static SignedDelegation Convert(this IIClient.SignedDelegation signedDeletation)
		{
			return new SignedDelegation(
				signedDeletation.Delegation.Convert(),
				signedDeletation.Signature.ToArray()
			);
		}
	}


	public class IIConnection
	{
		public static readonly Principal InternetIdentityBackendCanister = Principal.FromText("rdmx6-jaaaa-aaaaa-aaadq-cai");

		public IIdentity Identity
		{
			get => this._identity ?? new AnonymousIdentity();
			set
			{
				this._identity = value;
				this._agent = null;
				this._client = null;
			}
		}

		public IAgent Agent
		{
			get
			{
				return this._agent ?? (this._agent = new HttpAgent(this.Identity));
			}
		}
		
		public InternetIdentityApiClient Client
		{
			get
			{
				return this._client ?? (this._client = new InternetIdentityApiClient(this.Agent, InternetIdentityBackendCanister));
			}
		}

		public IIConnection()
		{
		}

		public async Task<DelegationIdentity> Login(ulong userNumber)
		{
			var authenticatorDevices = await this.LookupAuthenticators(userNumber);
			if (authenticatorDevices.FirstOrDefault() == null)
			{
				throw new UnknownUserException(userNumber);
			}

			var userIdentity = MultiWebAuthnIdentity.FromDevices(authenticatorDevices);
			return await this.RequestNewDelegation(userIdentity, InternetIdentityBackendCanister);
		}


		public async Task<AuthenticatedIIConnection> LoginToConn(ulong userNumber)
		{
			return new AuthenticatedIIConnection(userNumber, await this.Login(userNumber));
		}

		public async Task<IEnumerable<IIClient.DeviceData>> LookupAuthenticators(ulong userNumber)
		{
			var devices = await this.Client.Lookup(userNumber);
			return devices.Where(d => d.Purpose.Tag == IIClient.PurposeTag.Authentication);
		}

		// this is responsible for converting a "direct" user identity, which identifies the user,
		// into a delegation identity, which is anonymized, has an expiry, etc.
		//
		// corresponds to `requestFEDelegation' from @dfinity/internet-identity
		public async Task<DelegationIdentity> RequestNewDelegation(SigningIdentityBase userSignIdentity, Principal targetCanisterId)
		{
			var sessionKey = Ed25519Identity.Generate();
			var tenMinutesInNanoSeconds = 600 * 1_000_000_000ul;
			var delegationChain = await DelegationChain.CreateAsync(
				userSignIdentity,
				sessionKey.PublicKey,
				ICTimestamp.FromNanoSecondsInFuture(tenMinutesInNanoSeconds),
				principalIds: new List<Principal> { targetCanisterId }
			);
			return new DelegationIdentity(sessionKey, delegationChain);
		}



		private IAgent? _agent;
		private InternetIdentityApiClient? _client;
		public IIdentity? _identity;
	}


	public class AuthenticatedIIConnection : IIConnection
	{
		public readonly ulong UserNumber;


		public AuthenticatedIIConnection(ulong UserNumber, IIdentity identity)
		{
			this.UserNumber = UserNumber;
			this.Identity = identity;
		}


		public async Task<DelegationIdentity> PrepareAndGetDelegation(string hostname, SigningIdentityBase sessionKey, ulong? maxTimeToLive = null)
		{
			var sessionPubkey = sessionKey.GetPublicKey().Value.ToList();

			var (userkey, timestamp) = await this.Client.PrepareDelegation(
				this.UserNumber,
				hostname,
				sessionPubkey,
				new OptionalValue<ulong>(maxTimeToLive.HasValue, maxTimeToLive ?? 0)
			);
			var signedDelegation = (await this.Client.GetDelegation(
				this.UserNumber,
				hostname,
				sessionPubkey,
				timestamp
			)).AsSignedDelegation().Convert();

			return new DelegationIdentity(
					sessionKey,
					new DelegationChain(
						new DerEncodedPublicKey(userkey.ToArray()), // TODO: no copy
						new List<SignedDelegation> { signedDelegation }));
		}

	}
}
