using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidVectorType : CandidCompoundType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Vector;

		public CandidType InnerType { get; }

		public CandidVectorType(CandidType innerType, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.InnerType = innerType ?? throw new ArgumentNullException(nameof(innerType));
		}

		internal override byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable)
		{
			return this.InnerType.Encode(compoundTypeTable);
		}

		internal override IEnumerable<CandidType> GetInnerTypes()
		{
			yield return this.InnerType;
		}

		public override bool Equals(object? obj)
		{
			if (obj is CandidVectorType def)
			{
				return this.InnerType == def.InnerType;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(CandidTypeCode.Vector, this.InnerType);
		}
	}
}
