using EdjCase.ICP.Candid.Mapping;

namespace Sample.Shared.Governance.Models
{
	public class Motion
	{
		[CandidName("motion_text")]
		public string MotionText { get; set; }

		public Motion(string motionText)
		{
			this.MotionText = motionText;
		}

		public Motion()
		{
		}
	}
}