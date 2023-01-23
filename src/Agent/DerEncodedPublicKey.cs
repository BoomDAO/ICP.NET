using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;

namespace EdjCase.ICP.Agent
{
	public class DerEncodedPublicKey
	{
		private static class OidConstants
		{
			public static readonly string[] Ed25519 = new string[] { "1.3.101.112" };
			public static readonly string[] Bls = new string[] { "1.3.6.1.4.1.44668.5.3.1.2.1", "1.3.6.1.4.1.44668.5.3.2.1" };
			public static readonly string[] Cose = new string[] { "1.3.6.1.4.1.56387.1.1" };
		}


		public byte[] Value { get; }
		public DerEncodedPublicKey(byte[] value)
		{
			this.Value = value;
		}

		public Principal ToPrincipal()
		{
			return Principal.SelfAuthenticating(this.Value);
		}

		public byte[] AsEd25519()
		{
			return this.As(OidConstants.Ed25519);
		}

		public byte[] AsBls()
		{
			return this.As(OidConstants.Bls);
		}

		public byte[] AsCose()
		{
			return this.As(OidConstants.Cose);
		}


		public byte[] As(string oid)
		{
			return this.As(new string[] { oid });
		}

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

		public static DerEncodedPublicKey FromDer(byte[] value)
		{
			return new DerEncodedPublicKey(value);
		}

		public static DerEncodedPublicKey FromEd25519(byte[] value)
		{
			return From(value, OidConstants.Ed25519);
		}

		public static DerEncodedPublicKey FromBls(byte[] value)
		{
			return From(value, OidConstants.Bls);
		}

		public static DerEncodedPublicKey FromCose(byte[] value)
		{
			return From(value, OidConstants.Cose);
		}

		public static DerEncodedPublicKey From(byte[] value, string oid)
		{
			return From(value, new string[] { oid });
		}

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