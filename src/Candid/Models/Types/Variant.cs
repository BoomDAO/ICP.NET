using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class VariantCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Variant;

		protected override string TypeString { get; } = "variant";

		public VariantCandidTypeDefinition(Dictionary<CandidTag, CandidTypeDefinition> options, string? recursiveId = null) : base(options, recursiveId)
		{
			if (options?.Any() != true)
			{
				throw new ArgumentException("At least one variant option must be specified", nameof(options));
			}
		}
	}
}
