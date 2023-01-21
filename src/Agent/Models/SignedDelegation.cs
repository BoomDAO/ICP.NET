using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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

		public static async Task<SignedDelegation> CreateAsync(
			DerEncodedPublicKey publicKey,
			IIdentity signingIdentity,
			ICTimestamp expiration,
			List<Principal>? targets = null)
		{
			return await CreateAsync(publicKey, signingIdentity.SignAsync, expiration, targets);
		}

		public static async Task<SignedDelegation> CreateAsync(
			DerEncodedPublicKey publicKey,
			Func<byte[], Task<byte[]>> signingFunc,
			ICTimestamp expiration,
			List<Principal>? targets = null)
		{

			var delegation = new Delegation(publicKey.Value, expiration, targets);
			byte[] challenge = delegation.BuildSigningChallenge();
			byte[] signature = await signingFunc(challenge);
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
