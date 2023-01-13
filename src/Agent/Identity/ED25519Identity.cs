using Chaos.NaCl;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Keys;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

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


		public override byte[] Sign(byte[] message)
		{
			return Ed25519.Sign(message, this.PrivateKey);
		}
	}
}
