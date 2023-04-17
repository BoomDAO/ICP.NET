using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Utilities;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto;
using System.IO;
using Org.BouncyCastle.OpenSsl;

namespace EdjCase.ICP.Agent.Identities
{
	public class Secp256k1Identity : EcdsaIdentity
	{
		internal const string CURVE_OID = "1.3.132.0.10";
		public Secp256k1Identity(byte[] publicKey, byte[] privateKey)
			: base(publicKey, privateKey, CURVE_OID)
		{
		}

		public SubjectPublicKeyInfo GetPublicKey()
		{
			return this.PublicKey;
		}

		public List<SignedDelegation>? GetSenderDelegations()
		{
			return null;
		}

		public static Secp256k1Identity FromPrivateKey(byte[] privateKey)
		{
			byte[] publicKey = EcdsaIdentity.DeriveUncompressedPublicKey(
				privateKey,
				CURVE_OID
			);
			return new Secp256k1Identity(publicKey, privateKey);
		}

		public static Secp256k1Identity Generate()
		{
			byte[] privateKey = CreatePrivateKey(CURVE_OID);
			return Secp256k1Identity.FromPrivateKey(privateKey);
		}
	}
}
