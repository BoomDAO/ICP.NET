using EdjCase.ICP.Candid.Encodings;
using System;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// A special candid type model that is a pointer to a different type.
	/// Usually due to recursive types where a parent type has an inner type
	/// that references that same parent type. The parent type must have a `RecursiveId`
	/// specified and the `Id` of the reference type must match that
	/// </summary>
	public class CandidReferenceType : CandidType
	{
		/// <summary>
		/// The id to reference in a parent type. The parent type must have the `RecursiveId` specified
		/// </summary>
		public CandidId Id { get; }

		/// <param name="id">The id to reference in a parent type. The parent type must have the `RecursiveId` specified</param>
		public CandidReferenceType(CandidId id)
		{
			this.Id = id;
		}

		internal override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			uint index = compoundTypeTable.GetReferenceById(this.Id).Index;
			return LEB128.EncodeUnsigned(index);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is CandidReferenceType r)
			{
				return this.Id == r.Id;
			}
			if (obj is CandidCompoundType c)
			{
				return this.Id == c.RecursiveId;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Id);
		}
	}
}
