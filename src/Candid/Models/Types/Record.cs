using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class RecordCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Record;
		protected override string TypeString { get; } = "record";


		public RecordCandidTypeDefinition(Dictionary<CandidLabel, CandidTypeDefinition> fields, string? recursiveId = null) : base(fields, recursiveId)
		{
		}

	}
}
