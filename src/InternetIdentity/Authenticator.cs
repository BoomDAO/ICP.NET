using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using Fido2Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{
	public class Authenticator
	{
		private IInternetIdentityClient identityClient { get; }
		private IFido2Client fidoClient { get; }

		internal Authenticator(IInternetIdentityClient client, IFido2Client fidoClient)
		{
			this.identityClient = client;
			this.fidoClient = fidoClient;
		}

		public async Task<LoginResult> LoginAsync(
			ulong anchor,
			string clientHostname,
			IIdentity? sessionIdentity = null,
			TimeSpan? maxTimeToLive = null)
		{
			List<DeviceInfo> devices = await this.identityClient.LookupAsync(anchor);
			if (!devices.Any())
			{
				return LoginResult.FromError(ErrorType.InvalidAnchorOrNoDevices);
			}

			sessionIdentity = sessionIdentity ?? Ed25519Identity.Create();

			try
			{
				DelegationIdentity? deviceIdentity = await this.BuildDeviceIdentityAsync(devices, sessionIdentity);

				if (deviceIdentity == null)
				{
					return LoginResult.FromError(ErrorType.NoMatchingDevice);
				}

				// Sign the session identity using a delegation
				// the device key signs a delegation saying the 
				// session key is authorized for the next X amount
				// of time for the specified host
				DelegationIdentity identity = await this.identityClient.PrepareAndGetDelegationAsync(
					anchor,
					clientHostname,
					// Authenticate canister requests as device to get the delegated session identity
					deviceIdentity,
					// Identity to sign in a delegation
					sessionIdentity,
					maxTimeToLive
				);
				return LoginResult.FromSuccess(identity);
			}
			catch (FidoException)
			{
				return LoginResult.FromError(ErrorType.CouldNotAuthenticate);
			}

		}

		public static Authenticator WithHttpAgent(Principal? identityCanisterOverride = null)
		{
			var agent = new HttpAgent();
			IInternetIdentityClient client = new AgentInternetIdentityClient(agent, identityCanisterOverride);
			IFido2Client fido2Signer = new Fido2Client();
			return new Authenticator(client, fido2Signer);
		}



		/// <summary>
		/// This is responsible for converting a "direct" user identity, which identifies the user,
		/// into a delegation identity, which is anonymized, has an expiry, etc.
		/// 
		/// Corresponds to `requestFEDelegation' from @dfinity/internet-identity
		/// </summary>
		private async Task<DelegationIdentity?> BuildDeviceIdentityAsync(IList<DeviceInfo> devices, IIdentity sessionIdentity)
		{
			// Only allow the anonymizing delegation to last for 10 minutes
			ICTimestamp expiration = ICTimestamp.Future(TimeSpan.FromMinutes(10));

			Principal identityCanisterId = this.identityClient.GetCanisterId();
			var targets = new List<Principal> { identityCanisterId };

			DerEncodedPublicKey sessionPublicKey = sessionIdentity.GetPublicKey();

			// Create delegation represented by the session key, then sign it with the device
			var sessionDelegation = new Delegation(sessionPublicKey.Value, expiration, targets);
			byte[] challenge = sessionDelegation.BuildSigningChallenge();

			// TODO can detect the device before signing?
			(DerEncodedPublicKey PublicKey, byte[] Signature)? signInfo = await this.fidoClient.SignAsync(challenge, devices);

			if (signInfo == null)
			{
				return null;
			}

			// convert the assertion response into the form required by II (cbor)
			//byte[] assertion = SerializeAssertion();

			SignedDelegation signedDelegation = new SignedDelegation(sessionDelegation, signInfo.Value.Signature);

			// Have a delegation chain that represents the device but is delegated to 
			// the session key
			DelegationChain chain = new DelegationChain(
				signInfo.Value.PublicKey,
				new List<SignedDelegation> { signedDelegation }
			);


			return new DelegationIdentity(sessionIdentity, chain);
		}

	}

}
