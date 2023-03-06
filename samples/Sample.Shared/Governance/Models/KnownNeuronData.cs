using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Governance.Models
{
	public class KnownNeuronData
	{
		[CandidName("name")]
		public string Name { get; set; }

		[CandidName("description")]
		public OptionalValue<string> Description { get; set; }

		public KnownNeuronData(string name, OptionalValue<string> description)
		{
			this.Name = name;
			this.Description = description;
		}

		public KnownNeuronData()
		{
		}
	}
}