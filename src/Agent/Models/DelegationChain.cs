using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Models
{
	/// <summary>
	/// A model containing a list of signed delegations to authorize an identity 
	/// to act on behalf of the chain's public key
	/// </summary>
	public class DelegationChain
	{
		/// <summary>
		/// The public key of the identity that has delegated authority, DER encoded
		/// </summary>
		public SubjectPublicKeyInfo PublicKey { get; }

		/// <summary>
		/// <para>The chain of delegations to authorize a key<br/>
		/// Each delegation is signed by its parent key<br/>
		/// The first delegation's parent is the root key (`PublicKey` in `DelegationChain`)<br/>
		/// The last delegation is for the key to be authorized</para>
		/// </summary>
		public List<SignedDelegation> Delegations { get; }

		/// <param name="publicKey">The public key of the identity that has delegated authority, DER encoded</param>
		/// <param name="delegations">The chain of delegations to authorize a key</param>
		public DelegationChain(SubjectPublicKeyInfo publicKey, List<SignedDelegation> delegations)
		{
			this.PublicKey = publicKey;
			this.Delegations = delegations;
		}

		/// <summary>
		/// Creates a delegation chain from the specified keys
		/// </summary>
		/// <param name="keyToDelegateTo">The key to delegate authority to, DER encoded</param>
		/// <param name="delegatingIdentity">The identity that is signing the delegation</param>
		/// <param name="expiration">How long to delegate for</param>
		/// <param name="targets">Optional. List of canister ids to limit delegation to</param>
		/// <param name="senders">Optional. List of pricipals where requests can originate from</param>
		/// <returns>A delegation chain signed by the delegating identity</returns>
		public static DelegationChain Create(
			SubjectPublicKeyInfo keyToDelegateTo,
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
