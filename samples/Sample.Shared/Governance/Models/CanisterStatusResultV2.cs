using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class CanisterStatusResultV2
	{
		[CandidName("status")]
		public OptionalValue<int> Status { get; set; }

		[CandidName("freezing_threshold")]
		public OptionalValue<ulong> FreezingThreshold { get; set; }

		[CandidName("controllers")]
		public List<Principal> Controllers { get; set; }

		[CandidName("memory_size")]
		public OptionalValue<ulong> MemorySize { get; set; }

		[CandidName("cycles")]
		public OptionalValue<ulong> Cycles { get; set; }

		[CandidName("idle_cycles_burned_per_day")]
		public OptionalValue<ulong> IdleCyclesBurnedPerDay { get; set; }

		[CandidName("module_hash")]
		public List<byte> ModuleHash { get; set; }

		public CanisterStatusResultV2(OptionalValue<int> status, OptionalValue<ulong> freezingThreshold, List<Principal> controllers, OptionalValue<ulong> memorySize, OptionalValue<ulong> cycles, OptionalValue<ulong> idleCyclesBurnedPerDay, List<byte> moduleHash)
		{
			this.Status = status;
			this.FreezingThreshold = freezingThreshold;
			this.Controllers = controllers;
			this.MemorySize = memorySize;
			this.Cycles = cycles;
			this.IdleCyclesBurnedPerDay = idleCyclesBurnedPerDay;
			this.ModuleHash = moduleHash;
		}

		public CanisterStatusResultV2()
		{
		}
	}
}