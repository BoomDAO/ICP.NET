namespace Sample.Shared.Governance.Models
{
	public class Motion
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("motion_text")]
		public string MotionText { get; set; }
		
	}
}

