using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asn1DecoderNet5.Interfaces;
using Asn1DecoderNet5.Tags;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;

namespace EdjCase.ICP.Agent.Keys
{
	public class ED25519PublicKey : IHashable, IPublicKey
	{
		public byte[] Value { get; }

		public ED25519PublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public byte[] GetRawBytes()
		{
			return this.Value;
		}

		const int sequenceTagNumber = 48;
		const int oidTagNumber = 6;
		const int bitStringTagNumber = 3;

		public byte[] GetDerEncodedBytes()
		{
			ITag tag = new Tag
			{
				
			};
			tag.ConvertContentToReadableContent();
			string oid = tag.ReadableContent;
			return Asn1DecoderNet5.Encoding.OidEncoding.GetBytes(oid);
		}

		public static ED25519PublicKey FromDer(byte[] derEncodedPublicKey)
		{
			ITag tag = Asn1DecoderNet5.Decoder.Decode(derEncodedPublicKey);
			if (tag.TagNumber != bitStringTagNumber)
			{
				throw new InvalidEd25519PublicKey();
			}
			byte[] publicKey = tag.Content
				.Skip(1) // Skip 0 byte
				.ToArray();
			return new ED25519PublicKey(publicKey);
		}
	}


	public class InvalidEd25519PublicKey : Exception
	{
		public InvalidEd25519PublicKey()
		{
		}
	}
}
