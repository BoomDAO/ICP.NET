using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Agent.Models
{
	/// <summary>
	/// A model that contains data on delegating authority from an identity
	/// </summary>
	public class Delegation : IRepresentationIndependentHashItem, IHashable
	{
		/// <summary>
		/// The public key from the authorizing identity
		/// </summary>
		public SubjectPublicKeyInfo PublicKey { get; }

		/// <summary>
		/// The expiration when the delegation will no longer be valid
		/// </summary>
		public ICTimestamp Expiration { get; }

		/// <summary>
		/// Optional. A list of canister ids where the delegation can be sent to and be authorized
		/// </summary>
		public List<Principal>? Targets { get; }

		/// <summary>
		/// Optional. A list of sender ids that can send this delegation and be authorized
		/// </summary>
		public List<Principal>? Senders { get; }

		/// <param name="publicKey">The public key from the authorizing identity</param>
		/// <param name="expiration">The expiration when the delegation will no longer be valid</param>
		/// <param name="targets">Optional. A list of canister ids where the delegation can be sent to and be authorized</param>
		/// <param name="senders">Optional. A list of sender ids that can send this delegation and be authorized</param>
		/// <exception cref="ArgumentNullException"></exception>
		public Delegation(
			SubjectPublicKeyInfo publicKey,
			ICTimestamp expiration,
			List<Principal>? targets = null,
			List<Principal>? senders = null
		)
		{
			this.PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
			this.Expiration = expiration;
			this.Targets = targets;
			this.Senders = senders;
		}

		/// <inheritdoc/>
		public Dictionary<string, IHashable> BuildHashableItem()
		{
			var obj = new Dictionary<string, IHashable>
			{
				{Properties.PUBLIC_KEY, this.PublicKey.ToDerEncoding().ToHashable()},
				{Properties.EXPIRATION, this.Expiration},
			};

			if (this.Targets?.Any() == true)
			{
				obj.Add(Properties.TARGETS, this.Targets.ToHashable());
			}
			if (this.Senders?.Any() == true)
			{
				obj.Add(Properties.SENDERS, this.Senders.ToHashable());
			}


			return obj;
		}

		/// <inheritdoc/>
		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return this.BuildHashableItem()
				.ToHashable()
				.ComputeHash(hashFunction);
		}

		/// <summary>
		/// Creates a byte array of the data that can be signed by an algorithm for authorization/signature purposes
		/// </summary>
		/// <returns>Byte array representation of the data</returns>
		public byte[] BuildSigningChallenge()
		{
			Dictionary<string, IHashable> hashable = this.BuildHashableItem();
			// The signature is calculated by signing the concatenation of the domain separator
			// and the message.
			var hashFunction = SHA256HashFunction.Create();

			byte[] delegationHashDigest = new HashableObject(hashable)
				.ComputeHash(hashFunction);
			return Encoding.UTF8.GetBytes("\x1Aic-request-auth-delegation") // Prefix with domain seperator
				.Concat(delegationHashDigest)
				.ToArray();
		}

		internal class Properties
		{
			public const string PUBLIC_KEY = "pubkey";
			public const string EXPIRATION = "expiration";
			public const string TARGETS = "targets";
			public const string SENDERS = "senders";
		}
	}
}
