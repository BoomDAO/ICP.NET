using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Keys;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Agent.Models
{

	public class SignedDelegation : IHashable
	{
		[Dahomey.Cbor.Attributes.CborProperty("delegation")]
		public Delegation Delegation { get; }
		[Dahomey.Cbor.Attributes.CborProperty("signature")]
		public byte[] Signature { get; }

		public SignedDelegation(Delegation delegation, byte[] signature)
		{
			this.Delegation = delegation ?? throw new ArgumentNullException(nameof(delegation));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
		}

		public static SignedDelegation Create(
			SigningIdentityBase fromIdentity,
			IPublicKey publicKey,
			ICTimestamp expiration,
			List<Principal>? targets = null)
		{
			var delegation = new Delegation(publicKey.GetRawBytes(), expiration, targets);
			Dictionary<string, IHashable> hashable = delegation.BuildHashableItem();
			// The signature is calculated by signing the concatenation of the domain separator
			// and the message.
			var hashFunction = SHA256HashFunction.Create();

			byte[] delegationHashDigest = new HashableObject(hashable).ComputeHash(hashFunction);
			byte[] challenge = Encoding.UTF8.GetBytes("\x1Aic-request-auth-delegation") // Prefix with domain seperator
				.Concat(delegationHashDigest)
				.ToArray();

			byte[] signature = fromIdentity.Sign(challenge); // Sign the domain sep + delegation hash digest
			return new SignedDelegation(delegation, signature);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return new Dictionary<string, IHashable>
			{
				{ "delegation", this.Delegation },
				{ "signature", this.Signature.ToHashable() }
			}
				.ToHashable()
				.ComputeHash(hashFunction);
		}
	}

}
