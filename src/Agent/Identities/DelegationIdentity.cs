using System.Collections.Generic;
using EdjCase.ICP.Agent.Models;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// An identity that has a signed delegation chain that authorizes
	/// the identity to act as another identity
	/// 
	/// This is commonly used for things like Internet Identity where
	/// a login session always generates a new key but that key has been 
	/// signed by an authorized device through internet identity
	/// </summary>
	public class DelegationIdentity : IIdentity
	{
		/// <summary>
		/// The identity that authorization has been delegated to
		/// </summary>
		public IIdentity Identity { get; }

		/// <summary>
		/// The chain of singed delegations that prove authorization of the identity
		/// </summary>
		public DelegationChain Chain { get; }

		/// <param name="identity">The identity that authorization has been delegated to</param>
		/// <param name="chain">The chain of singed delegations that prove authorization of the identity</param>
		public DelegationIdentity(IIdentity identity, DelegationChain chain)
		{
			this.Identity = identity;
			this.Chain = chain;
		}

		/// <inheritdoc/>
		public SubjectPublicKeyInfo GetPublicKey()
		{
			return this.Chain.PublicKey;
		}

		/// <inheritdoc/>
		public byte[] Sign(byte[] message)
		{
			return this.Identity.Sign(message);
		}

		/// <inheritdoc/>
		public List<SignedDelegation> GetSenderDelegations()
		{
			return this.Chain.Delegations;
		}
	}

}
