namespace Sample.Shared.Dex.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(CancelOrderErrTag))]
	public class CancelOrderErr
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public CancelOrderErrTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private CancelOrderErr(CancelOrderErrTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected CancelOrderErr()
		{
		}
		
		public static CancelOrderErr NotAllowed()
		{
			return new CancelOrderErr(CancelOrderErrTag.NotAllowed, null);
		}
		
		public static CancelOrderErr NotExistingOrder()
		{
			return new CancelOrderErr(CancelOrderErrTag.NotExistingOrder, null);
		}
		
	}
	public enum CancelOrderErrTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NotAllowed")]
		NotAllowed,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("NotExistingOrder")]
		NotExistingOrder,
	}
}

