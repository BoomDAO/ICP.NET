using System.Collections.Generic;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidVariantType : CandidRecordOrVariantType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Variant;

		protected override string TypeString { get; } = "variant";

		public Dictionary<CandidTag, CandidType> Options { get; }

		public CandidVariantType(Dictionary<CandidTag, CandidType> options, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Options = options;
		}

		protected override Dictionary<CandidTag, CandidType> GetFieldsOrOptions()
		{
			return this.Options;
		}
	}
}
