using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Identity
{
	public class DelegationIdentity : SigningIdentityBase
	{
		public SigningIdentityBase Identity { get; }
		public DelegationChain Chain { get; }

		public DelegationIdentity(SigningIdentityBase identity, DelegationChain chain)
		{
			this.Identity = identity;
			this.Chain = chain;
		}

		public override IPublicKey GetPublicKey()
		{
			return this.Chain.PublicKey;
		}

		public override Signature Sign(byte[] blob)
		{
			return this.Identity.Sign(blob);
		}

		public override Principal GetPrincipal()
		{
			return this.Identity.GetPrincipal();
		}
	}

	public class DelegationChain
	{
		public DerEncodedPublicKey PublicKey { get; }
		public List<SignedDelegation> Delegations { get; }
		public DelegationChain(DerEncodedPublicKey publicKey, List<SignedDelegation> delegations)
		{
			this.PublicKey = publicKey;
			this.Delegations = delegations;
		}

		public static DelegationChain Create(
			SigningIdentityBase identity,
			DerEncodedPublicKey publicKey,
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

	public class SignedDelegation
	{
		public Delegation Delegation { get; }
		public Signature Signature { get; }

		public SignedDelegation(Delegation delegation, Signature signature)
		{
			this.Delegation = delegation ?? throw new ArgumentNullException(nameof(delegation));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
		}

		public static SignedDelegation Create(
			SigningIdentityBase fromIdentity,
			DerEncodedPublicKey publicKey,
			ICTimestamp expiration,
			List<Principal>? targets = null)
		{
			var delegation = new Delegation(publicKey, expiration, targets);
			Dictionary<string, IHashable> hashable = delegation.BuildHashableItem();
			// The signature is calculated by signing the concatenation of the domain separator
			// and the message.
			var hashFunction = SHA256HashFunction.Create();

			byte[] delegationHashDigest = new HashableObject(hashable).ComputeHash(hashFunction);
			byte[] challenge = Encoding.UTF8.GetBytes("\x1Aic-request-auth-delegation") // Prefix with domain seperator
				.Concat(delegationHashDigest)
				.ToArray();

			Signature signature = fromIdentity.Sign(challenge); // Sign the domain sep + delegation hash digest
			return new SignedDelegation(delegation, signature);
		}
	}
}
