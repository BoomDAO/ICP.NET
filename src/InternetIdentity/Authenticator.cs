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
		private IInternetIdentityClient client { get; }

		internal Authenticator(IInternetIdentityClient client)
		{
			this.client = client;
		}

		public async Task<LoginResult> LoginAsync(
			ulong anchor,
			string clientHostname,
			IIdentity? sessionIdentity = null,
			TimeSpan? maxTimeToLive = null)
		{
			List<DeviceInfo> devices = await this.client.LookupAsync(anchor);
			if (!devices.Any())
			{
				return LoginResult.FromError(ErrorType.InvalidAnchorOrNoDevices);
			}

			sessionIdentity = sessionIdentity ?? Ed25519Identity.Generate();

			try
			{
				DelegationIdentity deviceIdentity = await this.BuildDeviceIdentityAsync(devices, sessionIdentity);


				// Sign the session identity using a delegation
				// the device key signs a delegation saying the 
				// session key is authorized for the next X amount
				// of time for the specified host
				DelegationIdentity identity = await this.client.PrepareAndGetDelegationAsync(
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
			return new Authenticator(client);
		}



		/// <summary>
		/// This is responsible for converting a "direct" user identity, which identifies the user,
		/// into a delegation identity, which is anonymized, has an expiry, etc.
		/// 
		/// Corresponds to `requestFEDelegation' from @dfinity/internet-identity
		/// </summary>
		private async Task<DelegationIdentity> BuildDeviceIdentityAsync(IList<DeviceInfo> devices, IIdentity sessionIdentity)
		{
			// Only allow the anonymizing delegation to last for 10 minutes
			ICTimestamp expiration = ICTimestamp.Future(TimeSpan.FromMinutes(10));

			Principal identityCanisterId = this.client.GetCanisterId();
			var targets = new List<Principal> { identityCanisterId };

			DerEncodedPublicKey sessionPublicKey = sessionIdentity.GetPublicKey();

			// Create delegation represented by the session key, then sign it with the device
			var sessionDelegation = new Delegation(sessionPublicKey.Value, expiration, targets);
			byte[] challenge = sessionDelegation.BuildSigningChallenge();

			DelegationChain chain;
			using (var assert = new FidoAssertion())
			{
				// TODO can detect the device before signing?
				(DerEncodedPublicKey devicePublicKey, byte[] deviceSignature) = await Fido2.SignAsync(challenge, assert, devices);

				SignedDelegation signedDelegation = new SignedDelegation(sessionDelegation, deviceSignature);

				// Have a delegation chain that represents the device but is delegated to 
				// the session key
				chain = new DelegationChain(
					devicePublicKey,
					new List<SignedDelegation> { signedDelegation }
				);
			}


			return new DelegationIdentity(sessionIdentity, chain);
		}
	}

}
