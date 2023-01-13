using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Keys;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Models
{

	public class DelegationChain
	{
		public IPublicKey PublicKey { get; }
		public List<SignedDelegation> Delegations { get; }
		public DelegationChain(IPublicKey publicKey, List<SignedDelegation> delegations)
		{
			this.PublicKey = publicKey;
			this.Delegations = delegations;
		}

		public static DelegationChain Create(
			SigningIdentityBase identity,
			IPublicKey publicKey,
			ICTimestamp expiration,
			DelegationChain? previousChain = null,
			List<Principal>? principalIds = null)
		{
			SignedDelegation signedDelegation = SignedDelegation.Create(identity, publicKey, expiration, principalIds);
			List<SignedDelegation> delegations = previousChain?.Delegations ?? new List<SignedDelegation>();
			delegations.Add(signedDelegation);
			return new DelegationChain(publicKey, delegations);
		}
	}
}
