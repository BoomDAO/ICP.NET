using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class ClaimOrRefreshNeuronFromAccountResponse
	{
		[CandidName("result")]
		public OptionalValue<Result_1> Result { get; set; }

		public ClaimOrRefreshNeuronFromAccountResponse(OptionalValue<Result_1> result)
		{
			this.Result = result;
		}

		public ClaimOrRefreshNeuronFromAccountResponse()
		{
		}
	}
}