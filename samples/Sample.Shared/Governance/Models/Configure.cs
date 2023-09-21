using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class Configure
	{
		[CandidName("operation")]
		public OptionalValue<Operation> Operation { get; set; }

		public Configure(OptionalValue<Operation> operation)
		{
			this.Operation = operation;
		}

		public Configure()
		{
		}
	}
}