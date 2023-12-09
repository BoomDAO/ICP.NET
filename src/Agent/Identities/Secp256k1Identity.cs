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
	/// <summary>
	/// An identity using a Secp256k1 key
	/// </summary>
	public class Secp256k1Identity : EcdsaIdentity
	{
		internal const string CURVE_OID = "1.3.132.0.10";

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="publicKey">The raw Secp256k1 public key</param>
		/// <param name="privateKey">The raw Secp256k1 private key</param>
		public Secp256k1Identity(byte[] publicKey, byte[] privateKey)
			: base(publicKey, privateKey, CURVE_OID)
		{
		}

		/// <summary>
		/// Converts a raw secp256k1 private key to a Secp256k1Identity, deriving the public key
		/// </summary>
		/// <param name="privateKey">Raw secp256k1 private key</param>
		/// <returns>Secp256k1Identity with specified private key</returns>
		public static Secp256k1Identity FromPrivateKey(byte[] privateKey)
		{
			byte[] publicKey = EcdsaIdentity.DeriveUncompressedPublicKey(
				privateKey,
				CURVE_OID
			);
			return new Secp256k1Identity(publicKey, privateKey);
		}

		/// <summary>
		/// Generates a new secp256k1 public/private key pair and creates an identity
		/// for them
		/// </summary>
		/// <returns>Secp256k1Identity with new key pair</returns>
		public static Secp256k1Identity Generate()
		{
			byte[] privateKey = GeneratePrivateKey(CURVE_OID);
			return Secp256k1Identity.FromPrivateKey(privateKey);
		}
	}
}
