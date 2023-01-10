using Chaos.NaCl;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace EdjCase.ICP.Agent.Identity
{
	public class ED25519Identity : SigningIdentityBase
	{
		public ED25519PublicKey PublicKey { get; }
		public byte[] PrivateKey { get; }

		public ED25519Identity(ED25519PublicKey publicKey, byte[] privateKey)
		{
			// TODO validate that pub+priv match
			this.PublicKey = publicKey;
			this.PrivateKey = privateKey;
		}


		public override IPublicKey GetPublicKey()
		{
			return this.PublicKey;
		}


		public override Signature Sign(byte[] message)
		{
			byte[] signatureBytes = Ed25519.Sign(message, this.PrivateKey);
			return new Signature(signatureBytes);
		}

		public static ED25519Identity Generate()
		{
			byte[] seed = new byte[Ed25519.PrivateKeySeedSizeInBytes];
			using var cryptoRng = new RNGCryptoServiceProvider();
			cryptoRng.GetBytes(seed);
			Ed25519.KeyPairFromSeed(publicKey: out var pub, expandedPrivateKey: out var priv, seed);
			return new ED25519Identity(new ED25519PublicKey(pub), priv);
		}
	}
}
