using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using UserNumber = System.UInt64;
using PublicKey = System.Collections.Generic.List<System.Byte>;
using CredentialId = System.Collections.Generic.List<System.Byte>;
using DeviceKey = PublicKey;
using UserKey = PublicKey;
using SessionKey = PublicKey;
using FrontendHostname = System.String;
using Timestamp = System.UInt64;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(KeyTypeTag))]
	public class KeyType
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public KeyTypeTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private KeyType(KeyTypeTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected KeyType()
		{
		}
		
		public static KeyType Unknown()
		{
			return new KeyType(KeyTypeTag.Unknown, null);
		}
		
		public static KeyType Platform()
		{
			return new KeyType(KeyTypeTag.Platform, null);
		}
		
		public static KeyType CrossPlatform()
		{
			return new KeyType(KeyTypeTag.CrossPlatform, null);
		}
		
		public static KeyType SeedPhrase()
		{
			return new KeyType(KeyTypeTag.SeedPhrase, null);
		}
		
	}
	public enum KeyTypeTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("unknown")]
		Unknown,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("platform")]
		Platform,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("cross_platform")]
		CrossPlatform,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("seed_phrase")]
		SeedPhrase,
	}
}

