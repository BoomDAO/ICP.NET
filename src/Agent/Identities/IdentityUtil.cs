using Org.BouncyCastle.Crypto.Parameters;
using System;
using Org.BouncyCastle.Crypto;
using System.IO;
using Org.BouncyCastle.OpenSsl;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// Utility class for helper methods around Identities
	/// </summary>
	public static class IdentityUtil
	{
		/// <summary>
		/// Generates a new Secp256k1 identity with a new private key
		/// </summary>
		/// <returns>Secp256k1 identity</returns>
		public static Secp256k1Identity GenerateSecp256k1Identity()
		{
			return Secp256k1Identity.Generate();
		}

		/// <summary>
		/// Generates a new Ed25519 identity with a new private key
		/// </summary>
		/// <returns>Ed25519 identity</returns>
		public static Ed25519Identity GenerateEd25519Identity()
		{
			return Ed25519Identity.Generate();
		}

		/// <summary>
		/// Converts a raw private key into a Secp256k1Identity class
		/// </summary>
		/// <param name="privateKey">A raw Secp256k1 private key</param>
		/// <returns>Secp256k1 identity</returns>
		public static Secp256k1Identity FromSecp256k1PrivateKey(byte[] privateKey)
		{
			return Secp256k1Identity.FromPrivateKey(privateKey);
		}


		/// <summary>
		/// Converts a raw private key into a Ed25519Identity class
		/// </summary>
		/// <param name="privateKey">A raw Ed25519 private key</param>
		/// <returns>Ed25519 identity</returns>
		public static Ed25519Identity FromEd25519PrivateKey(byte[] privateKey)
		{
			return Ed25519Identity.FromPrivateKey(privateKey);
		}

		/// <summary>
		/// Parses a PEM file into the proper IIdentity class
		/// </summary>
		/// <param name="pemFilePath">The file path of a PEM file</param>
		/// <param name="password">Optional. The password of the encrypted PEM file</param>
		/// <returns>IIdentity for the private key</returns>
		public static IIdentity FromPemFile(string pemFilePath, string? password)
		{
			using (FileStream stream = File.OpenRead(pemFilePath))
			{
				return FromPemFile(stream, password);
			}
		}
		/// <summary>
		/// Parses a PEM file into the proper IIdentity class
		/// </summary>
		/// <param name="pemFile">The stream of a PEM file</param>
		/// <param name="password">Optional. The password of the encrypted PEM file</param>
		/// <returns>IIdentity for the private key</returns>
		public static IIdentity FromPemFile(Stream pemFile, string? password)
		{
			using (StreamReader reader = new StreamReader(pemFile))
			{
				return FromPemFile(reader, password);
			}
		}

		/// <summary>
		/// Parses a PEM file into the proper IIdentity class
		/// </summary>
		/// <param name="pemFileReader">The text reader of a PEM file</param>
		/// <param name="password">Optional. The password of the encrypted PEM file</param>
		/// <returns>IIdentity for the private key</returns>
		public static IIdentity FromPemFile(TextReader pemFileReader, string? password)
		{
			IPasswordFinder? passwordFinder = null;
			if (password != null)
			{
				passwordFinder = new StaticPassworFinder(password);
			};
			PemReader pemReader = new PemReader(pemFileReader, passwordFinder);
			object obj = pemReader.ReadObject();
			if (obj == null)
			{
				throw new Exception("Could not read PEM file");
			}
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

		private class StaticPassworFinder : IPasswordFinder
		{
			private readonly string password;
			public StaticPassworFinder(string password)
			{
				this.password = password;
			}
			public char[] GetPassword()
			{
				return this.password.ToCharArray();
			}
		}
	}
}
