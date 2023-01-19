using System;
using System.Formats.Asn1;

namespace EdjCase.ICP.Agent
{
	public class DerEncodedPublicKey
	{
		public byte[] Value { get; }
		public DerEncodedPublicKey(byte[] value)
		{
			this.Value = value;
		}

		public byte[] AsEd25519()
		{
			return this.As("1.3.101.112");
		}

		public byte[] AsBls()
		{
			return this.As("1.3.6.1.4.1.44668.5.3.1.2.1", "1.3.6.1.4.1.44668.5.3.2.1");
		}

		public byte[] AsCose()
		{
			return this.As("0x2b, 0x06, 0x01, 0x04, 0x01, 0x83, 0xb8, 0x43, 0x01, 0x01");
		}

		public byte[] As(params string[] oids)
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


		public static DerEncodedPublicKey FromEd25519(byte[] value)
		{
			return From(value, "1.3.101.112");
		}

		public static DerEncodedPublicKey FromBls(byte[] value)
		{
			return From(value, "1.3.6.1.4.1.44668.5.3.1.2.1", "1.3.6.1.4.1.44668.5.3.2.1");
		}

		public static DerEncodedPublicKey FromCose(byte[] value)
		{
			return From(value, "0x2b, 0x06, 0x01, 0x04, 0x01, 0x83, 0xb8, 0x43, 0x01, 0x01");
		}

		public static DerEncodedPublicKey From(byte[] value, params string[] oids)
		{
			if (oids == null || oids.Length < 1)
			{
				throw new ArgumentNullException(nameof(oids));
			}
			AsnWriter writer = new(AsnEncodingRules.DER);

			using (writer.PushSequence())
			{
				using (writer.PushSequence())
				{
					foreach (string oid in oids)
					{
						writer.WriteObjectIdentifier(oid);
					}
				}
				// BIT STRING(256 bit)
				writer.WriteBitString(value);
			}

			byte[] derValue = writer.Encode();
			return new DerEncodedPublicKey(derValue);
		}
	}
}