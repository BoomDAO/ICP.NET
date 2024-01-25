using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using BatchId = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the arguments for computing evidence for a proposed batch in the asset canister.
	/// </summary>
	public class ComputeEvidenceArguments
	{
		/// <summary>
		/// The unique identifier of the batch for which evidence is being computed.
		/// </summary>
		[CandidName("batch_id")]
		public BatchId BatchId { get; set; }

		/// <summary>
		/// The maximum number of iterations to use in the computation of evidence.
		/// </summary>
		[CandidName("max_iterations")]
		public OptionalValue<ushort> MaxIterations { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ComputeEvidenceArguments"/> class with specified batch ID and maximum iterations.
		/// </summary>
		/// <param name="batchId">The unique identifier of the batch.</param>
		/// <param name="maxIterations">The maximum number of iterations for the evidence computation.</param>
		public ComputeEvidenceArguments(BatchId batchId, OptionalValue<ushort> maxIterations)
		{
			this.BatchId = batchId;
			this.MaxIterations = maxIterations;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ComputeEvidenceArguments"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ComputeEvidenceArguments()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
