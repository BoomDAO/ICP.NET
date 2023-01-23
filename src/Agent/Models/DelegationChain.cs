using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Models
{

	public class DelegationChain
	{
		public DerEncodedPublicKey PublicKey { get; }
		public List<SignedDelegation> Delegations { get; }
		public DelegationChain(DerEncodedPublicKey publicKey, List<SignedDelegation> delegations)
		{
			this.PublicKey = publicKey;
			this.Delegations = delegations;
		}

		public static async Task<DelegationChain> CreateAsync(
			DerEncodedPublicKey publicKey,
			IIdentity identity,
			ICTimestamp expiration,
			DelegationChain? previousChain = null,
			List<Principal>? targets = null)
		{
			SignedDelegation signedDelegation = await SignedDelegation.CreateAsync(publicKey, identity, expiration, targets);
			List<SignedDelegation> delegations = previousChain?.Delegations ?? new List<SignedDelegation>();
			delegations.Add(signedDelegation);
			return new DelegationChain(identity.GetPublicKey(), delegations);
		}

		public bool IsExpirationValid(ICTimestamp timestamp)
		{
			return this.Delegations.All(d => d.Delegation.IsExpirationValid(timestamp));
		}
	}
}
