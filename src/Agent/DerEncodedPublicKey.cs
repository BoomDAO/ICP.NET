using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EdjCase.ICP.Agent
{
	/// <summary>
	/// A helper class to encode and decode public keys to the DER encoding
	/// </summary>
	public class DerEncodedPublicKey
	{
		private static class OidConstants
		{
			public static readonly string[] Ed25519 = new string[] { "1.3.101.112" };
			public static readonly string[] Bls = new string[] { "1.3.6.1.4.1.44668.5.3.1.2.1", "1.3.6.1.4.1.44668.5.3.2.1" };
			public static readonly string[] Cose = new string[] { "1.3.6.1.4.1.56387.1.1" };
		}


		/// <summary>
		/// Raw value of the DER encoded public key
		/// </summary>
		public byte[] Value { get; }

		/// <param name="derEncodedBytes">Raw value of the DER encoded public key</param>
		public DerEncodedPublicKey(byte[] derEncodedBytes)
		{
			this.Value = derEncodedBytes;
		}

		/// <summary>
		/// Converts the key to a self authenticating principal value
		/// </summary>
		/// <returns></returns>
		public Principal ToPrincipal()
		{
			return Principal.SelfAuthenticating(this.Value);
		}

		/// <summary>
		/// Converts the DER encoded key to a `Ed25519` key IF a `Ed25519` key
		/// otherwise throws an exception
		/// </summary>
		/// <exception cref="InvalidPublicKey">Throws if not a `Ed25519` key</exception>
		/// <returns>Bytes of the `Ed25519` public key</returns>
		public byte[] AsEd25519()
		{
			return this.As(OidConstants.Ed25519);
		}

		/// <summary>
		/// Converts the DER encoded key to a `BLS` key IF a `BLS` key
		/// otherwise throws an exception
		/// </summary>
		/// <exception cref="InvalidPublicKey">Throws if not a `BLS` key</exception>
		/// <returns>Bytes of the `BLS` public key</returns>
		public byte[] AsBls()
		{
			return this.As(OidConstants.Bls);
		}

		/// <summary>
		/// Converts the DER encoded key to a `COSE` key IF a `COSE` key
		/// otherwise throws an exception
		/// </summary>
		/// <exception cref="InvalidPublicKey">Throws if not a `COSE` key</exception>
		/// <returns>Bytes of the `COSE` public key</returns>
		public byte[] AsCose()
		{
			return this.As(OidConstants.Cose);
		}

		/// <summary>
		/// Converts the DER encoded key to a raw key based on the OID specified IF its has
		/// the specified OID, otherwise throws an exception
		/// </summary>
		/// <exception cref="InvalidPublicKey">Throws if not a key with the specified OID</exception>
		/// <returns>Bytes of the decoded public key</returns>
		public byte[] As(string oid)
		{
			return this.As(new string[] { oid });
		}

		/// <summary>
		/// Converts the DER encoded key to a raw key based on the OIDs specified IF its has
		/// the specified OIDs, otherwise throws an exception
		/// </summary>
		/// <exception cref="InvalidPublicKey">Throws if not a key with the specified OIDs</exception>
		/// <returns>Bytes of the decoded public key</returns>
		public byte[] As(IEnumerable<string> oids)
		{
			byte[] publicKey;
			try
			{
				AsnReader reader = new(this.Value, AsnEncodingRules.DER);
				AsnReader seqReader = reader.ReadSequence();
				AsnReader seq2Reader = seqReader.ReadSequence();

				foreach (string oid in oids)
				{
					string actualOid = seq2Reader.ReadObjectIdentifier();
					if (actualOid != oid)
					{
						throw new InvalidPublicKey();
					}
				}
				publicKey = seqReader.ReadBitString(out int _);
			}
			catch
			{
				throw new InvalidPublicKey();
			}
			return publicKey;
		}

		/// <summary>
		/// Creates a DER public key from the raw bytes of a DER encoded key. Same as the constructor
		/// </summary>
		/// <param name="value">DER encoded bytes of a public key</param>
		/// <returns>DER encoded key</returns>
		public static DerEncodedPublicKey FromDer(byte[] value)
		{
			return new DerEncodedPublicKey(value);
		}

		/// <summary>
		/// Creates a DER public key from the raw bytes of a `ED25519` key.
		/// </summary>
		/// <param name="value">Bytes of a `ED25519` public key</param>
		/// <returns>DER encoded key</returns>
		public static DerEncodedPublicKey FromEd25519(byte[] value)
		{
			return From(value, OidConstants.Ed25519);
		}

		/// <summary>
		/// Creates a DER public key from the raw bytes of a `BLS` key.
		/// </summary>
		/// <param name="value">Bytes of a `BLS` public key</param>
		/// <returns>DER encoded key</returns>
		public static DerEncodedPublicKey FromBls(byte[] value)
		{
			return From(value, OidConstants.Bls);
		}

		/// <summary>
		/// Creates a DER public key from the raw bytes of a `COSE` key.
		/// </summary>
		/// <param name="value">Bytes of a `COSE` public key</param>
		/// <returns>DER encoded key</returns>
		public static DerEncodedPublicKey FromCose(byte[] value)
		{
			return From(value, OidConstants.Cose);
		}

		/// <summary>
		/// Creates a DER public key from the raw bytes of a key that has the specified OID
		/// </summary>
		/// <param name="value">Bytes of a public key</param>
		/// <param name="oid">The OID for the DER encoding</param>
		/// <returns>DER encoded key</returns>
		public static DerEncodedPublicKey From(byte[] value, string oid)
		{
			return From(value, new string[] { oid });
		}

		/// <summary>
		/// Creates a DER public key from the raw bytes of a key that has the specified OIDs
		/// </summary>
		/// <param name="value">Bytes of a public key</param>
		/// <param name="oids">The OIDs for the DER encoding</param>
		/// <returns>DER encoded key</returns>
		public static DerEncodedPublicKey From(byte[] value, IEnumerable<string> oids)
		{
			if (oids == null)
			{
				throw new ArgumentNullException(nameof(oids));
			}
			AsnWriter writer = new(AsnEncodingRules.DER);

			using (writer.PushSequence())
			{
				using (writer.PushSequence())
				{
					bool any = false;
					foreach (string oid in oids)
					{
						writer.WriteObjectIdentifier(oid);
						any = true;
					}
					if (!any)
					{
						throw new ArgumentException("At least one OID is required", nameof(oids));
					}
				}
				writer.WriteBitString(value);
			}

			byte[] derValue = writer.Encode();
			return new DerEncodedPublicKey(derValue);
		}
	}
}