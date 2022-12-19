namespace Sample.Shared.Governance.Models
{
	public class SetDefaultFollowees
	{
		public List<default_followeesInfo> default_followees { get; set; }
		
		public class default_followeesInfo
		{
			public int F0 { get; set; }
			
			public Followees F1 { get; set; }
			
		}
	}
}
