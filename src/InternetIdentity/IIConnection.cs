using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityClient = EdjCase.ICP.InternetIdentity.Models;

namespace EdjCase.ICP.InternetIdentity
{
	public class UnknownAnchorException : System.Exception
	{
		public ulong Anchor { get; }

		public UnknownAnchorException(ulong anchor)
		{
			this.Anchor = anchor;
		}

		public override string Message => $"Unknown anchor '{this.Anchor}'";
	}


	public class IIConnection
	{
		private static readonly Principal internetIdentityBackendCanister = Principal.FromText("rdmx6-jaaaa-aaaaa-aaadq-cai");
		private InternetIdentityApiClient client { get; }
		public IIConnection(IAgent agent)
		{
			this.client = new InternetIdentityApiClient(agent, IIConnection.internetIdentityBackendCanister);
		}

		public async Task<DelegationIdentity> LoginAsync(ulong anchor)
		{
			List<DeviceInfo> devices = await this.GetDevicesAsync(anchor);
			if (!devices.Any())
			{
				throw new UnknownAnchorException(anchor);
			}

			var userIdentity = MultiWebAuthnIdentity.FromDevices(devices);
			return await this.RequestNewDelegation(userIdentity);
		}


		public async Task<List<DeviceInfo>> GetDevicesAsync(ulong anchor)
		{
			List<IdentityClient.DeviceData> devices = await this.client.Lookup(anchor);
			return devices
				.Where(d => d.Purpose.Tag == IdentityClient.PurposeTag.Authentication)
				.Select(DeviceInfo.FromModel)
				.ToList();
		}

		public async Task<DelegationIdentity> PrepareAndGetDelegation(string hostname, SigningIdentityBase sessionKey, ulong? maxTimeToLive = null)
		{
			var sessionPubkey = sessionKey.GetPublicKey().Value.ToList();

			(byte[] userkey, ICTimestamp timestamp) = await this.client.PrepareDelegation(
				this.IdentityAnchor,
				hostname,
				sessionPubkey,
				new OptionalValue<ulong>(maxTimeToLive.HasValue, maxTimeToLive ?? 0)
			);
			IdentityClient.GetDelegationResponse response = await this.client.GetDelegation(
				this.IdentityAnchor,
				hostname,
				sessionPubkey,
				timestamp
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
					throw new System.Exception(); // TODO
				default:
					throw new NotImplementedException();
			}

			var chain = new DelegationChain(
				new DerEncodedPublicKey(userkey),
				new List<SignedDelegation> { signedDelegation }
			);
			return new DelegationIdentity(sessionKey, chain);



		/// <summary>
		/// This is responsible for converting a "direct" user identity, which identifies the user,
		/// into a delegation identity, which is anonymized, has an expiry, etc.
		/// 
		/// Corresponds to `requestFEDelegation' from @dfinity/internet-identity
		/// </summary>
		private async Task<DelegationIdentity> RequestNewDelegation(SigningIdentityBase identity)
		{
			Ed25519Identity sessionKey = Ed25519Identity.Generate();
			ICTimestamp tenMinutesFromNow = ICTimestamp.FromNanoSecondsInFuture(600 * 1_000_000_000ul);
			DelegationChain delegationChain = await DelegationChain.CreateAsync(
				identity,
				sessionKey.PublicKey,
				expiration: tenMinutesFromNow,
				principalIds: new List<Principal> { internetIdentityBackendCanister }
			);
			return new DelegationIdentity(sessionKey, delegationChain);
		}
	}
}
