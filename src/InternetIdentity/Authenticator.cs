using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using Fido2Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityClient = EdjCase.ICP.InternetIdentity.Models;

namespace EdjCase.ICP.InternetIdentity
{
	public class Authenticator
	{

		private InternetIdentityApiClient client { get; }
		private Principal identityCanisterId { get; }

		public Authenticator(IAgent agent, Principal? identityCanisterOverride = null)
		{
			this.identityCanisterId = identityCanisterOverride ?? Principal.FromText("rdmx6-jaaaa-aaaaa-aaadq-cai");
			this.client = new InternetIdentityApiClient(agent, this.identityCanisterId);
		}

		public async Task<LoginResult> LoginAsync(
			ulong anchor,
			string clientHostname,
			IIdentity? sessionIdentity = null,
			ulong? maxTimeToLive = null)
		{
			List<DeviceInfo> devices = await this.GetDevicesAsync(anchor);
			if (!devices.Any())
			{
				return LoginResult.FromError(ErrorType.InvalidAnchorOrNoDevices);
			}

			sessionIdentity = sessionIdentity ?? Ed25519Identity.Generate();

			try
			{
				DelegationIdentity deviceIdentity = await this.BuildDeviceIdentityAsync(devices, sessionIdentity);

				// Authenticate requests as device
				this.client.Agent.Identity = deviceIdentity;

				DelegationIdentity identity = await this.PrepareAndGetDelegationAsync(anchor, clientHostname, sessionIdentity, maxTimeToLive);
				return LoginResult.FromSuccess(identity);
			}
			catch (FidoException)
			{
				return LoginResult.FromError(ErrorType.CouldNotAuthenticate);
			}

		}

		public static Authenticator WithHttpAgent(Principal? identityCanisterOverride = null)
		{
			IAgent agent = new HttpAgent();
			return new Authenticator(agent, identityCanisterOverride);
		}


		private async Task<List<DeviceInfo>> GetDevicesAsync(ulong anchor)
		{
			List<IdentityClient.DeviceData> devices = await this.client.Lookup(anchor);
			return devices
				.Where(d => d.Purpose.Tag == IdentityClient.PurposeTag.Authentication)
				.Select(DeviceInfo.FromModel)
				.ToList();
		}

		private async Task<DelegationIdentity> PrepareAndGetDelegationAsync(ulong anchor, string hostname, IIdentity sessionIdentity, ulong? maxTimeToLive = null)
		{
			DerEncodedPublicKey publicKey = sessionIdentity.GetPublicKey();
			(byte[] userkey, ulong expiration) = await this.client.PrepareDelegation(
				anchor,
				hostname,
				publicKey.Value,
				new OptionalValue<ulong>(maxTimeToLive.HasValue, maxTimeToLive ?? 0)
			);
			IdentityClient.GetDelegationResponse response = await this.client.GetDelegation(
				anchor,
				hostname,
				publicKey.Value,
				expiration
			);
			SignedDelegation signedDelegation;
			switch (response.Tag)
			{
				case IdentityClient.GetDelegationResponseTag.SignedDelegation:
					IdentityClient.SignedDelegation signedDelegationResponse = response.AsSignedDelegation();
					IdentityClient.Delegation delegationResponse = signedDelegationResponse.Delegation;
					Delegation delegation = new Delegation(
						delegationResponse.Pubkey,
						ICTimestamp.FromNanoSeconds(delegationResponse.Expiration),
						delegationResponse.Targets.GetValueOrDefault()
					);
					signedDelegation = new SignedDelegation(delegation, signedDelegationResponse.Signature);
					break;
				case IdentityClient.GetDelegationResponseTag.NoSuchDelegation:
					throw new Exception("Failed to find delegation. Delegation preparation must have failed.");
				default:
					throw new NotImplementedException();
			}

			var chain = new DelegationChain(
				new DerEncodedPublicKey(userkey),
				new List<SignedDelegation> { signedDelegation }
			);
			return new DelegationIdentity(sessionIdentity, chain);
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

			var targets = new List<Principal> { this.identityCanisterId };

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
