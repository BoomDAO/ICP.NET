namespace Sample.Shared.Dex.Models
{
	public enum CancelOrderErrType
	{
		NotAllowed,
		NotExistingOrder,
	}
	public class CancelOrderErr : EdjCase.ICP.Candid.CandidVariantValueBase<CancelOrderErrType>
	{
		public CancelOrderErr(CancelOrderErrType type, object? value)  : base(type, value)
		{
		}
		
		protected CancelOrderErr()
		{
		}
		
		public static CancelOrderErr NotAllowed()
		{
			return new CancelOrderErr(CancelOrderErrType.NotAllowed, null);
		}
		
		public static CancelOrderErr NotExistingOrder()
		{
			return new CancelOrderErr(CancelOrderErrType.NotExistingOrder, null);
		}
		
	}
}
