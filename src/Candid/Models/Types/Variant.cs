using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidVariantType : CandidRecordOrVariantType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Variant;

		protected override string TypeString { get; } = "variant";

		public CandidVariantType(Dictionary<CandidTag, CandidType> options, CandidId? recursiveId = null) : base(options, recursiveId)
		{
			if (options?.Any() != true)
			{
				throw new ArgumentException("At least one variant option must be specified", nameof(options));
			}
		}
	}
}
