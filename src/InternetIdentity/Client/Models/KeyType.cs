using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(KeyTypeTag))]
	public class KeyType
	{
		[VariantTagProperty]
		public KeyTypeTag Tag { get; set; }
		[VariantValueProperty]
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
		[CandidName("unknown")]
		Unknown,
		[CandidName("platform")]
		Platform,
		[CandidName("cross_platform")]
		CrossPlatform,
		[CandidName("seed_phrase")]
		SeedPhrase,
	}
}

