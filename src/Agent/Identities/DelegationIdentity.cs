using System.Collections.Generic;
using EdjCase.ICP.Agent.Models;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Identities
{
	// TODO validate and document
	public class DelegationIdentity : IIdentity
	{

		public IIdentity Identity { get; }
		public DelegationChain Chain { get; }

		public DelegationIdentity(IIdentity identity, DelegationChain chain)
		{
			this.Identity = identity;
			this.Chain = chain;
		}

		/// <inheritdoc/>
		public DerEncodedPublicKey GetPublicKey()
		{
			return this.Chain.PublicKey;
		}

		/// <inheritdoc/>
		public async Task<byte[]> SignAsync(byte[] blob)
		{
			return await this.Identity.SignAsync(blob);
		}

		/// <inheritdoc/>
		public List<SignedDelegation> GetSenderDelegations()
		{
			return this.Chain.Delegations;
		}
	}

}
