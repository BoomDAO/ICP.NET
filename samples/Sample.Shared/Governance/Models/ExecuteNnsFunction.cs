using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class ExecuteNnsFunction
	{
		[CandidName("nns_function")]
		public int NnsFunction { get; set; }

		[CandidName("payload")]
		public List<byte> Payload { get; set; }

		public ExecuteNnsFunction(int nnsFunction, List<byte> payload)
		{
			this.NnsFunction = nnsFunction;
			this.Payload = payload;
		}

		public ExecuteNnsFunction()
		{
		}
	}
}