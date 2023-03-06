using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccountResponse
	{
		[CandidName("result")]
		public OptionalValue<Result1> Result { get; set; }

		public ClaimOrRefreshNeuronFromAccountResponse(OptionalValue<Result1> result)
		{
			this.Result = result;
		}

		public ClaimOrRefreshNeuronFromAccountResponse()
		{
		}
	}
}