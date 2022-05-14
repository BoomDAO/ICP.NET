using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class CandidRecordType : CandidRecordOrVariantType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Record;
		protected override string TypeString { get; } = "record";


		public CandidRecordType(Dictionary<CandidTag, CandidType> fields, CandidId? recursiveId = null) : base(fields, recursiveId)
		{
		}

	}
}
