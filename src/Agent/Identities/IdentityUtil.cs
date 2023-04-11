using EdjCase.ICP.Candid.Models;
using System;
using System.Formats.Asn1;
using System.Numerics;

namespace EdjCase.ICP.Agent.Identities
{
	public static class IdentityUtil
	{
		public static IIdentity FromSec1(byte[] sec1Value)
		{
			try
			{
				AsnReader reader = new(sec1Value, AsnEncodingRules.DER);
				AsnReader seqReader = reader.ReadSequence();
				BigInteger version = seqReader.ReadInteger();
				if(version != 1)
				{
					throw new NotImplementedException("Only version 1 in SEC1 parsing is implemented");
				}
				byte[] privateKey = seqReader.ReadOctetString();

				Asn1Tag oidTag = new Asn1Tag(TagClass.ContextSpecific, tagValue: 0, isConstructed: true);

				string oid = seqReader
					.ReadSequence(oidTag)
					.ReadObjectIdentifier();

				Asn1Tag publicKeyTag = new Asn1Tag(TagClass.ContextSpecific, tagValue: 1, isConstructed: true);
				byte[] publicKeyBytes = seqReader
					.ReadSequence(publicKeyTag)
					.ReadBitString(out int unusedBitCount);
				
				var publicKey = DerEncodedPublicKey.From(publicKeyBytes, oid);
				var p = publicKey.ToPrincipal();
				switch (oid)
				{
					case "1.3.132.0.10":
						return new Secp256k1Identity(publicKey, privateKey);
					case "1.3.101.112":
						return new Ed25519Identity(publicKey, privateKey);
					default:
						throw new NotImplementedException($"DER oid '{oid}' not implemented");
				}
			}
			catch (Exception e)
			{
				throw new InvalidSec1Key(e);
			}
		}
	}
}
