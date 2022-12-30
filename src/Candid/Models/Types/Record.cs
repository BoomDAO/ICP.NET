using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidRecordType : CandidRecordOrVariantType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Record;
		protected override string TypeString { get; } = "record";

		public Dictionary<CandidTag, CandidType> Fields { get; }

		public CandidRecordType(Dictionary<CandidTag, CandidType> fields, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Fields = fields;
		}

		protected override Dictionary<CandidTag, CandidType> GetFieldsOrOptions()
		{
			return this.Fields;
		}
	}
}
