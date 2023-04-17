using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Text;

namespace EdjCase.ICP.Agent
{
	public class SubjectPublicKeyInfo
	{
		public AlgorithmIdentifier Algorithm { get; }
		public byte[] PublicKey { get; }

		public SubjectPublicKeyInfo(AlgorithmIdentifier algorithm, byte[] subjectPublicKey)
		{
			this.Algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
			this.PublicKey = subjectPublicKey ?? throw new ArgumentNullException(nameof(subjectPublicKey));
		}

		/// <summary>
		/// Converts the key to a self authenticating principal value
		/// </summary>
		/// <returns></returns>
		public Principal ToPrincipal()
		{
			byte[] derEncodedPublicKey = this.ToDerEncoding();
			return Principal.SelfAuthenticating(derEncodedPublicKey);
		}

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
		public static SubjectPublicKeyInfo Ed25519(byte[] publicKey)
		{
			var algorithm = AlgorithmIdentifier.Ed25519();
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}

		public static SubjectPublicKeyInfo Ecdsa(byte[] publicKey, string curveOid)
		{
			var algorithm = AlgorithmIdentifier.Ecdsa(curveOid);
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}
		public static SubjectPublicKeyInfo Secp256k1(byte[] publicKey)
		{
			var algorithm = AlgorithmIdentifier.Secp256k1();
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}

		public static SubjectPublicKeyInfo Bls(byte[] publicKey)
		{
			var algorithm = AlgorithmIdentifier.Bls();
			return new SubjectPublicKeyInfo(algorithm, publicKey);
		}
	}

	public class AlgorithmIdentifier
	{
		public AlgorithmIdentifier(string algorithmOid, string? parametersOid = null)
		{
			this.AlgorithmOid = algorithmOid ?? throw new ArgumentNullException(nameof(algorithmOid));
			this.ParametersOid = parametersOid;
		}

		public string AlgorithmOid { get; }
		public string? ParametersOid { get; }

		public static AlgorithmIdentifier Ed25519()
		{
			return new AlgorithmIdentifier("1.3.101.112");
		}
		public static AlgorithmIdentifier Secp256k1()
		{
			return Ecdsa("1.3.132.0.10");
		}

		public static AlgorithmIdentifier Ecdsa(string curveOid)
		{
			return new AlgorithmIdentifier("1.2.840.10045.2.1", curveOid);
		}

		public static AlgorithmIdentifier Bls()
		{
			return new AlgorithmIdentifier("1.3.6.1.4.1.44668.5.3.1.2.1", "1.3.6.1.4.1.44668.5.3.2.1");
		}
	}
}
