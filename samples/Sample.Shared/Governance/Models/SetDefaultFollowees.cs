namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		public List<DefaultFolloweesInfo> DefaultFollowees { get; set; }
		
		public class DefaultFolloweesInfo
		{
			public int F0 { get; set; }
			
			public Followees F1 { get; set; }
			
		}
	}
}
