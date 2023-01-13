namespace Sample.Shared.Governance.Models
{
	public class ExecuteNnsFunction
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("nns_function")]
		public int NnsFunction { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("payload")]
		public System.Collections.Generic.List<byte> Payload { get; set; }
		
	}
}

