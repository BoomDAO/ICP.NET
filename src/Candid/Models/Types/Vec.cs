using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidVectorType : CandidCompoundType
	{
		/// <inheritdoc />
		public override CandidTypeCode Type { get; } = CandidTypeCode.Vector;

		/// <summary>
		/// The type of the vectors inner values
		/// </summary>
		public CandidType InnerType { get; }

		/// <param name="innerType">The type of the vectors inner values</param>
		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
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

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is CandidVectorType def)
			{
				return this.InnerType == def.InnerType;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(CandidTypeCode.Vector, this.InnerType);
		}
	}
}
