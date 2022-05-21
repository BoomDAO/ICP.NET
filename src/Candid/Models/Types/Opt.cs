using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidOptType : CandidCompoundType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Opt;
		public CandidType Value { get; }

		public CandidOptType(CandidType value, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		internal override byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable)
		{
			return this.Value.Encode(compoundTypeTable);
		}

		internal override IEnumerable<CandidType> GetInnerTypes()
		{
			yield return this.Value;
		}

		public override bool Equals(object? obj)
		{
			if (obj is CandidOptType def)
			{
				return this.Value == def.Value;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(CandidTypeCode.Opt, this.Value);
		}
	}
}
