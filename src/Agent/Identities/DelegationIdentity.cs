using EdjCase.ICP.Agent.Keys;
using System.Collections.Generic;
using EdjCase.ICP.Agent.Models;
using System.Threading.Tasks;

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
		public override async Task<byte[]> SignAsync(byte[] blob)
		{
			return await this.Identity.SignAsync(blob);
		}

		/// <inheritdoc/>
		public override List<SignedDelegation> GetSenderDelegations()
		{
			return this.Chain.Delegations;
		}
	}

}
