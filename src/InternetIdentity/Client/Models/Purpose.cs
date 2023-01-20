using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(PurposeTag))]
	public class Purpose
	{
		[VariantTagProperty]
		public PurposeTag Tag { get; set; }
		[VariantValueProperty]
		public object? Value { get; set; }
		private Purpose(PurposeTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Purpose()
		{
		}
		
		public static Purpose Recovery()
		{
			return new Purpose(PurposeTag.Recovery, null);
		}
		
		public static Purpose Authentication()
		{
			return new Purpose(PurposeTag.Authentication, null);
		}
		
	}
	public enum PurposeTag
	{
		[CandidName("recovery")]
		Recovery,
		[CandidName("authentication")]
		Authentication,
	}
}

