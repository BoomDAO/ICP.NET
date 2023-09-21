using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	public class Migrations
	{
		[CandidName("neuron_indexes_migration")]
		public OptionalValue<Migration> NeuronIndexesMigration { get; set; }

		[CandidName("copy_inactive_neurons_to_stable_memory_migration")]
		public OptionalValue<Migration> CopyInactiveNeuronsToStableMemoryMigration { get; set; }

		public Migrations(OptionalValue<Migration> neuronIndexesMigration, OptionalValue<Migration> copyInactiveNeuronsToStableMemoryMigration)
		{
			this.NeuronIndexesMigration = neuronIndexesMigration;
			this.CopyInactiveNeuronsToStableMemoryMigration = copyInactiveNeuronsToStableMemoryMigration;
		}

		public Migrations()
		{
		}
	}
}