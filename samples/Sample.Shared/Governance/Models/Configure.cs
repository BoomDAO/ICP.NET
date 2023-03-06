using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using EdjCase.ICP.Candid.Models;

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