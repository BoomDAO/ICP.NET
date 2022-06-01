namespace Sample.Shared.Dex.Models
{
	public enum CancelOrderErrType
	{
		NotAllowed,
		NotExistingOrder,
	}
	public class CancelOrderErr
	{
		public CancelOrderErrType Type { get; }
		private readonly object? value;
		
		public CancelOrderErr(CancelOrderErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static CancelOrderErr NotAllowed()
		{
			return new CancelOrderErr(CancelOrderErrType.NotAllowed, null);
		}
		
		public static CancelOrderErr NotExistingOrder()
		{
			return new CancelOrderErr(CancelOrderErrType.NotExistingOrder, null);
		}
		
		private void ValidateType(CancelOrderErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
