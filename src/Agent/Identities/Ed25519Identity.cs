using Chaos.NaCl;
using EdjCase.ICP.Agent.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// An identity using a Ed25519 key
	/// </summary>
	public class Ed25519Identity : IIdentity
	{
		/// <summary>
		/// The public key of the identity
		/// </summary>
		public DerEncodedPublicKey PublicKey { get; }

		/// <summary>
		/// The private key of the identity
		/// </summary>
		public byte[] PrivateKey { get; }

		/// <param name="publicKey">The public key of the identity</param>
		/// <param name="privateKey">The private key of the identity</param>
		public Ed25519Identity(DerEncodedPublicKey publicKey, byte[] privateKey)
		{
			// TODO validate that pub+priv match?
			this.PublicKey = publicKey;
			this.PrivateKey = privateKey;
		}

		/// <inheritdoc/>
		public DerEncodedPublicKey GetPublicKey()
		{
			return this.PublicKey;
		}


		/// <inheritdoc/>
		public Task<byte[]> SignAsync(byte[] message)
		{
			byte[] signature = Ed25519.Sign(message, this.PrivateKey);
			return Task.FromResult(signature);
		}

		/// <inheritdoc/>
		public List<SignedDelegation>? GetSenderDelegations()
		{
			return null;
		}

		/// <summary>
		/// Generates an identity with a new Ed25519 key pair
		/// </summary>
		/// <returns>A Ed25519 identity</returns>
		public static Ed25519Identity Generate()
		{
			byte[] seed = new byte[Ed25519.PrivateKeySeedSizeInBytes];
			using (var cryptoRng = new RNGCryptoServiceProvider())
			{
				cryptoRng.GetBytes(seed);
				Ed25519.KeyPairFromSeed(publicKey: out var pub, expandedPrivateKey: out var priv, seed);
				var publicKey = DerEncodedPublicKey.FromEd25519(pub);
				return new Ed25519Identity(publicKey, priv);
			}
		}
	}
}
