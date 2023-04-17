using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Text;

namespace EdjCase.ICP.Agent
{
	/// <summary>
	/// A model representing a public key value and the cryptographic algorithm that is for
	/// </summary>
	public class SubjectPublicKeyInfo
	{
		/// <summary>
		/// The cryptographic algorithm that the public key is for
		/// </summary>
		public AlgorithmIdentifier Algorithm { get; }

		/// <summary>
		/// The raw public key bytes
		/// </summary>
		public byte[] PublicKey { get; }

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="algorithm">The cryptographic algorithm that the public key is for</param>
		/// <param name="subjectPublicKey">The raw public key bytes</param>
		public SubjectPublicKeyInfo(AlgorithmIdentifier algorithm, byte[] subjectPublicKey)
		{
			this.Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
			this.PublicKey = subjectPublicKey ?? throw new ArgumentNullException(nameof(subjectPublicKey));
		}

		/// <summary>
		/// Converts the key to a self authenticating principal value
		/// </summary>
		/// <returns>Principal of the public key</returns>
		public Principal ToPrincipal()
		{
			byte[] derEncodedPublicKey = this.ToDerEncoding();
			return Principal.SelfAuthenticating(derEncodedPublicKey);
		}

		/// <summary>
		/// Converts the subject public key info into a DER encoded byte array
		/// </summary>
		/// <returns>A DER encoded byte array</returns>
		public byte[] ToDerEncoding()
		{
			AsnWriter writer = new(AsnEncodingRules.DER);

			using (writer.PushSequence())
			{
				using (writer.PushSequence())
				{
					writer.WriteObjectIdentifier(this.Algorithm.AlgorithmOid);
					if (this.Algorithm.ParametersOid != null)
					{
						writer.WriteObjectIdentifier(this.Algorithm.ParametersOid);
					}
				}
				writer.WriteBitString(this.PublicKey);
			}

			return writer.Encode();
		}

		/// <summary>
		/// Parses a DER encoded subject public key info 
		/// </summary>
		/// <param name="derEncodedPublicKey">A DER encoded public key</param>
		/// <returns></returns>
		/// <exception cref="InvalidPublicKey"></exception>
		public static SubjectPublicKeyInfo FromDerEncoding(byte[] derEncodedPublicKey)
		{
			try
			{
				AsnReader reader = new(derEncodedPublicKey, AsnEncodingRules.DER);
				AsnReader seqReader = reader.ReadSequence();
				AsnReader seq2Reader = seqReader.ReadSequence();

				string actualOid = seq2Reader.ReadObjectIdentifier();
				string? parametersOid = null;
				if (seq2Reader.HasData)
				{
					parametersOid = seq2Reader.ReadObjectIdentifier();
				}
				byte[] publicKey = seqReader.ReadBitString(out int _);

				var algorithm = new AlgorithmIdentifier(actualOid, parametersOid);
				return new SubjectPublicKeyInfo(algorithm, publicKey);
			}
			catch (Exception e)
			{
				throw new InvalidPublicKey(e);
			}
		}

		/// <summary>
		/// Converts a raw ed25519 public key into a subject public key info
		/// </summary>
		/// <param name="publicKey">A raw ed25519 public key</param>
		/// <returns>Ed25519 SubjectPublicKeyInfo</returns>
		public static SubjectPublicKeyInfo Ed25519(byte[] publicKey)
		{
			var algorithm = AlgorithmIdentifier.Ed25519();
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}

		/// <summary>
		/// Converts a raw ed25519 public key into a subject public key info
		/// </summary>
		/// <param name="publicKey">A raw ed25519 public key</param>
		/// <param name="curveOid">The OID of the ecdsa curve (eg "1.3.132.0.10" for secp256k1)</param>
		/// <returns>Ed25519 SubjectPublicKeyInfo</returns>
		public static SubjectPublicKeyInfo Ecdsa(byte[] publicKey, string curveOid)
		{
			var algorithm = AlgorithmIdentifier.Ecdsa(curveOid);
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}

		/// <summary>
		/// Converts a raw secp256k1 public key into a subject public key info
		/// </summary>
		/// <param name="publicKey">A raw secp256k1 public key</param>
		/// <returns>Secp256k1 SubjectPublicKeyInfo</returns>
		public static SubjectPublicKeyInfo Secp256k1(byte[] publicKey)
		{
			var algorithm = AlgorithmIdentifier.Secp256k1();
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}

		/// <summary>
		/// Converts a raw bls public key into a subject public key info
		/// </summary>
		/// <param name="publicKey">A raw bls public key</param>
		/// <returns>Bls SubjectPublicKeyInfo</returns>
		public static SubjectPublicKeyInfo Bls(byte[] publicKey)
		{
			var algorithm = AlgorithmIdentifier.Bls();
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}
	}
}
