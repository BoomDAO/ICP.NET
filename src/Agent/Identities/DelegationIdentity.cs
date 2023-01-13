using EdjCase.ICP.Agent.Keys;
using System.Collections.Generic;
using EdjCase.ICP.Agent.Models;

namespace EdjCase.ICP.Agent.Identities
{
	// TODO validate and document
	public class DelegationIdentity : SigningIdentityBase
	{

		public SigningIdentityBase Identity { get; }
		public DelegationChain Chain { get; }

		public DelegationIdentity(SigningIdentityBase identity, DelegationChain chain)
		{
			this.Identity = identity;
			this.Chain = chain;
		}

		/// <inheritdoc/>
		public override IPublicKey GetPublicKey()
		{
			return this.Chain.PublicKey;
		}

		/// <inheritdoc/>
		public override byte[] Sign(byte[] blob)
		{
			return this.Identity.Sign(blob);
		}

		/// <inheritdoc/>
		public override List<SignedDelegation> GetSenderDelegations()
		{
			return this.Chain.Delegations;
		}
	}

}
