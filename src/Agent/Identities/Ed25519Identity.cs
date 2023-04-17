using EdjCase.ICP.Agent.Models;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Security;
using System.Collections.Generic;

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
		public SubjectPublicKeyInfo PublicKey { get; }

		/// <summary>
		/// The private key of the identity
		/// </summary>
		public byte[] PrivateKey { get; }

		/// <param name="publicKey">The public key of the identity</param>
		/// <param name="privateKey">The private key of the identity</param>
		public Ed25519Identity(byte[] publicKey, byte[] privateKey)
		{
			// TODO validate that pub+priv match?
			var algorithm = AlgorithmIdentifier.Ed25519();
			this.PublicKey = new SubjectPublicKeyInfo(algorithm, publicKey);
			this.PrivateKey = privateKey;
		}

		/// <inheritdoc/>
		public SubjectPublicKeyInfo GetPublicKey()
		{
			return this.PublicKey;
		}


		/// <inheritdoc/>
		public byte[] Sign(byte[] message)
		{
			var privateKey = new Ed25519PrivateKeyParameters(this.PrivateKey, 0);
			Ed25519Signer signer = new Ed25519Signer();
			signer.Init(true, privateKey);
			signer.BlockUpdate(message, 0, message.Length);
			return signer.GenerateSignature();
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
			var key = new Ed25519PrivateKeyParameters(new SecureRandom());
			byte[] privateKey = key.GetEncoded();
			byte[] publicKey = key.GeneratePublicKey().GetEncoded();
			return new Ed25519Identity(publicKey, privateKey);
		}

		/// <summary>
		/// Converts a raw ed25519 private key to a Secp256k1Identity, deriving the public key
		/// </summary>
		/// <param name="privateKey">Raw ed25519 private key</param>
		/// <returns>Ed25519Identity with specified private key</returns>
		public static Ed25519Identity FromPrivateKey(byte[] privateKey)
		{
			// Derive the public key
			byte[] publicKey = new Ed25519PrivateKeyParameters(privateKey, 0)
				.GeneratePublicKey()
				.GetEncoded();

			return new Ed25519Identity(publicKey, privateKey);
		}
	}
}
