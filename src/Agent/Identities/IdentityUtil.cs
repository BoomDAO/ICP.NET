using Org.BouncyCastle.Crypto.Parameters;
using System;
using Org.BouncyCastle.Crypto;
using System.IO;
using Org.BouncyCastle.OpenSsl;

namespace EdjCase.ICP.Agent.Identities
{
	public static class IdentityUtil
	{
		public static Secp256k1Identity GenerateSecp256k1Identity()
		{
			return Secp256k1Identity.Generate();
		}

		public static Ed25519Identity GenerateEd25519Identity()
		{
			return Ed25519Identity.Generate();
		}

		public static Secp256k1Identity FromSecp256k1PrivateKey(byte[] privateKey)
		{
			return Secp256k1Identity.FromPrivateKey(privateKey);
		}

		public static Ed25519Identity FromEd25519PrivateKey(byte[] privateKey)
		{
			return Ed25519Identity.FromPrivateKey(privateKey);
		}

		public static IIdentity FromPemFile(Stream pemFile)
		{
			using (StreamReader reader = new StreamReader(pemFile))
			{
				return FromPemFile(reader);
			}
		}

		public static IIdentity FromPemFile(TextReader pemFileReader)
		{
			PemReader pemReader = new PemReader(pemFileReader);
			object obj = pemReader.ReadObject();
			if (obj is AsymmetricCipherKeyPair aKeyPair
				&& aKeyPair.Private is ECPrivateKeyParameters ecPrivateKey)
			{
				byte[] privateKeyBytes = ecPrivateKey.D.ToByteArrayUnsigned();
				string curveOid = ecPrivateKey.PublicKeyParamSet.Id;
				switch (curveOid)
				{
					case Secp256k1Identity.CURVE_OID:
						return Secp256k1Identity.FromPrivateKey(privateKeyBytes);
					default:
						throw new NotImplementedException($"There is no support for curve oid '{curveOid}'");
				}
			}
			else if (obj is Ed25519PrivateKeyParameters ed25519PrivateKey)
			{
				return Ed25519Identity.FromPrivateKey(ed25519PrivateKey.GetEncoded());
			}
			else
			{
				throw new NotImplementedException($"PEM data of type '{obj.GetType()}' is not yet supported");
			}
		}
	}
}
