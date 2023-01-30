using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Models
{
	/// <summary>
	/// A model containing a list of signed delegations to authorize an identity 
	/// to act on behalf of the chain's public key
	/// </summary>
	public class DelegationChain
	{
		/// <summary>
		/// The public key of the identity that has delegated authority
		/// </summary>
		public DerEncodedPublicKey PublicKey { get; }

		/// <summary>
		/// <para>The chain of delegations to authorize a key<br/>
		/// Each delegation is signed by its parent key<br/>
		/// The first delegation's parent is the root key (`PublicKey` in `DelegationChain`)<br/>
		/// The last delegation is for the key to be authorized</para>
		/// </summary>
		public List<SignedDelegation> Delegations { get; }

		/// <param name="publicKey">The public key of the identity that has delegated authority</param>
		/// <param name="delegations">The chain of delegations to authorize a key</param>
		public DelegationChain(DerEncodedPublicKey publicKey, List<SignedDelegation> delegations)
		{
			this.PublicKey = publicKey;
			this.Delegations = delegations;
		}

		/// <summary>
		/// Creates a delegation chain from the specified keys
		/// </summary>
		/// <param name="keyToDelegateTo">The key to delegate authority to</param>
		/// <param name="delegatingIdentity">The identity that is signing the delegation</param>
		/// <param name="expiration">How long to delegate for</param>
		/// <param name="targets">Optional. List of canister ids to limit delegation to</param>
		/// <param name="senders">Optional. List of pricipals where requests can originate from</param>
		/// <returns>A delegation chain signed by the delegating identity</returns>
		public static DelegationChain Create(
			DerEncodedPublicKey keyToDelegateTo,
			IIdentity delegatingIdentity,
			ICTimestamp expiration,
			List<Principal>? targets = null,
			List<Principal>? senders = null
		)
		{
			SignedDelegation signedDelegation = SignedDelegation.Create(
				keyToDelegateTo,
				delegatingIdentity,
				expiration,
				targets,
				senders
			);
			List<SignedDelegation> delegations = new List<SignedDelegation>
			{
				signedDelegation
			};
			return new DelegationChain(delegatingIdentity.GetPublicKey(), delegations);
		}
	}
}
