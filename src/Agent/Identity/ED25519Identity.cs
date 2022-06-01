using Chaos.NaCl;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent.Identity
{
	public class ED25519Identity : SigningIdentityBase
	{
		public byte[] PrivateKey { get; }
		public ED25519Identity(byte[] privateKey)
		{
			this.PrivateKey = privateKey;
		}

		public override Principal GetPrincipal()
		{
			byte[] publicKey = this.GetPublicKey().GetDerEncodedBytes().Value;
			return Principal.SelfAuthenticating(publicKey);
		}

		public override IPublicKey GetPublicKey()
		{
			return Ed25519.PublicKeyFromSeed(this.PrivateKey);
		}

		public override Signature Sign(byte[] message)
		{
			byte[] expandedPrivateKey = Ed25519.ExpandedPrivateKeyFromSeed(this.PrivateKey);
			byte[] signatureBytes = Ed25519.Sign(message, expandedPrivateKey);
			return new Signature(signatureBytes);
		}

		public static ED25519Identity FromPem(byte[] pemBytes)
		{

		}
	}
}
