using Dahomey.Cbor.Attributes;
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
	/// <summary>
	/// A delegation that has been signed by an identity
	/// </summary>
	public class SignedDelegation : IHashable
	{
		/// <summary>
		/// The delegation that is signed
		/// </summary>
		[CborProperty(Properties.DELEGATION)]
		public Delegation Delegation { get; }

		/// <summary>
		/// The signature for the delegation
		/// </summary>
		[CborProperty(Properties.SIGNATURE)]
		public byte[] Signature { get; }

		/// <param name="delegation">The delegation that is signed</param>
		/// <param name="signature">The signature for the delegation</param>
		public SignedDelegation(Delegation delegation, byte[] signature)
		{
			this.Delegation = delegation ?? throw new ArgumentNullException(nameof(delegation));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
		}


		/// <summary>
		/// Creates a delegation signed by the delegating identity, authorizing the public key
		/// </summary>
		/// <param name="keyToDelegateTo">The key to delegate authority to</param>
		/// <param name="delegatingIdentity">The identity that is signing the delegation</param>
		/// <param name="expiration">How long to delegate for</param>
		/// <param name="targets">Optional. List of canister ids to limit delegation to</param>
		/// <param name="senders">Optional. List of pricipals where requests can originate from</param>
		/// <returns>A delegation signed by the delegating identity</returns>
		public static async Task<SignedDelegation> CreateAsync(
			DerEncodedPublicKey keyToDelegateTo,
			IIdentity delegatingIdentity,
			ICTimestamp expiration,
			List<Principal>? targets = null,
			List<Principal>? senders = null)
		{
			return await CreateAsync(keyToDelegateTo, delegatingIdentity.SignAsync, expiration, targets, senders);
		}

		/// <summary>
		/// Creates a delegation signed by the delegating identity, authorizing the public key
		/// </summary>
		/// <param name="keyToDelegateTo">The key to delegate authority to</param>
		/// <param name="signingFunc">Function to sign the delegation bytes</param>
		/// <param name="expiration">How long to delegate for</param>
		/// <param name="targets">Optional. List of canister ids to limit delegation to</param>
		/// <param name="senders">Optional. List of pricipals where requests can originate from</param>
		/// <returns>A delegation signed by the delegating identity</returns>
		public static async Task<SignedDelegation> CreateAsync(
			DerEncodedPublicKey keyToDelegateTo,
			Func<byte[], Task<byte[]>> signingFunc,
			ICTimestamp expiration,
			List<Principal>? targets = null,
			List<Principal>? senders = null)
		{
			var delegation = new Delegation(keyToDelegateTo.Value, expiration, targets, senders);
			byte[] challenge = delegation.BuildSigningChallenge();
			byte[] signature = await signingFunc(challenge);
			return new SignedDelegation(delegation, signature);
		}

		/// <inheritdoc />
		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return new Dictionary<string, IHashable>
			{
				{ Properties.DELEGATION, this.Delegation },
				{ Properties.SIGNATURE, this.Signature.ToHashable() }
			}
				.ToHashable()
				.ComputeHash(hashFunction);
		}

		private class Properties
		{
			public const string DELEGATION = "delegation";
			public const string SIGNATURE = "signature";
		}
	}

}
