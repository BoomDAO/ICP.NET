using System;
using System.Buffers;
using System.Collections.Generic;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// A model for candid optional types
	/// </summary>
	public class CandidOptionalType : CandidCompoundType
	{
		/// <inheritdoc />
		public override CandidTypeCode Type { get; } = CandidTypeCode.Opt;

		/// <summary>
		/// The inner value type of the optional value, if the value is not null
		/// </summary>
		public CandidType Value { get; }

		/// <param name="value">The inner value type of the optional value, if the value is not null</param>
		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
		public CandidOptionalType(CandidType value, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		internal override void EncodeInnerTypes(CompoundTypeTable compoundTypeTable, IBufferWriter<byte> destination)
		{
			this.Value.Encode(compoundTypeTable, destination);
		}

		internal override IEnumerable<CandidType> GetInnerTypes()
		{
			yield return this.Value;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is CandidOptionalType def)
			{
				return this.Value == def.Value;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(CandidTypeCode.Opt, this.Value);
		}
	}
}
