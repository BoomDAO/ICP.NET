using Chaos.NaCl;
using EdjCase.ICP.Agent.Keys;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// An identity using a Ed25519 key
	/// </summary>
	public class Ed25519Identity : SigningIdentityBase
	{
		/// <summary>
		/// The public key of the identity
		/// </summary>
		public Ed25519PublicKey PublicKey { get; }

		/// <summary>
		/// The private key of the identity
		/// </summary>
		public byte[] PrivateKey { get; }

		/// <param name="publicKey">The public key of the identity</param>
		/// <param name="privateKey">The private key of the identity</param>
		public Ed25519Identity(Ed25519PublicKey publicKey, byte[] privateKey)
		{
			// TODO validate that pub+priv match
			this.PublicKey = publicKey;
			this.PrivateKey = privateKey;
		}

		/// <inheritdoc/>
		public override IPublicKey GetPublicKey()
		{
			return this.PublicKey;
		}


		/// <inheritdoc/>
		public override byte[] Sign(byte[] message)
		{
			return Ed25519.Sign(message, this.PrivateKey);
		}
	}
}
