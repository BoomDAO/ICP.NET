using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Asn1DecoderNet5.Interfaces;
using Asn1DecoderNet5.Tags;
using EdjCase.Cryptography.BLS;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Agent.Keys
{
	public class BlsPublicKey : IHashable, IPublicKey
	{
		public byte[] Value { get; }

		public BlsPublicKey(byte[] value)
		{
			this.Value = value;
		}


		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}


		const int sequenceTagNumber = 48;
		const int oidTagNumber = 6;
		const int bitStringTagNumber = 3;

		//1.3.6.1.4.1.44668.5.3.1.2.1
		static byte[] algorithmOid = new byte[]
		{
			0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0xDC, 0x7C, 0x05, 0x03, 0x01, 0x02, 0x01
		};
		//1.3.6.1.4.1.44668.5.3.2.1
		static byte[] curveOid = new byte[]
		{
			0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0xDC, 0x7C, 0x05, 0x03, 0x02, 0x01
		};

		public byte[] GetDerEncodedBytes()
		{
			ITag tag = new Tag
			{
				Childs = new List<ITag>
				{
					new Tag
					{
						TagNumber = sequenceTagNumber,
						Childs = new List<ITag>
						{
							new Tag
							{
								TagNumber = oidTagNumber,
								Content = algorithmOid
							},
							new Tag
							{
								TagNumber = oidTagNumber,
								Content = curveOid
							}
						}
					},
					new Tag
					{
						TagNumber = bitStringTagNumber,
						Content = new byte[] { 0x00 }
							.Concat(this.Value)
							.ToArray()
					}
				}
			};
			tag.ConvertContentToReadableContent();
			string oid = tag.ReadableContent;
			return Asn1DecoderNet5.Encoding.OidEncoding.GetBytes(oid);
		}

		public static BlsPublicKey FromDer(byte[] derEncodedPublicKey)
		{
			// DER encoding
			// SEQUENCE (2 elem)
			//   SEQUENCE(2 elem)
			//     OBJECT IDENTIFIER 1.3.6.1.4.1.44668.5.3.1.2.1
			//     OBJECT IDENTIFIER 1.3.6.1.4.1.44668.5.3.2.1
			//   BIT STRING(768 bit) …
			ITag derTag = Asn1DecoderNet5.Decoder.Decode(derEncodedPublicKey);
			if (derTag.TagNumber != sequenceTagNumber || derTag.Childs.Count != 2)
			{
				throw new InvalidBlsPublicKey();
			}
			ITag oids = derTag.Childs.First();
			if (oids.Childs.Count != 2)
			{
				throw new InvalidBlsPublicKey();
			}
			ITag oid1 = oids.Childs[0];
			ITag oid2 = oids.Childs[1];
			if (oid1.TagNumber != oidTagNumber
				|| oid2.TagNumber != oidTagNumber)
			{
				throw new InvalidBlsPublicKey();
			}

			if (!oid1.Content.SequenceEqual(algorithmOid)
				|| !oid2.Content.SequenceEqual(curveOid))
			{
				throw new InvalidBlsPublicKey();
			}

			byte[] publicKey = derTag.Childs[1].Content
				.Skip(1) // Skip 0 byte
				.ToArray();
			return new BlsPublicKey(publicKey);
		}

		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		public bool ValidateSignature(byte[] hash, byte[] signature)
		{
			return BlsUtil.VerifyHash(this.Value, hash, signature);
		}
	}


	public class InvalidBlsPublicKey : Exception
	{
		public InvalidBlsPublicKey()
		{
		}
	}
}
