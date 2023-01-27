using EdjCase.ICP.Candid.Encodings;
using System;
using System.Linq;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Exceptions;

namespace EdjCase.ICP.Candid.Models.Values
{
	/// <summary>
	/// A model representing a candid variant value
	/// </summary>
	public class CandidVariant : CandidValue
	{
		/// <inheritdoc />
		public override CandidValueType Type { get; } = CandidValueType.Variant;

		/// <summary>
		/// The tag (id/name) of the chosen variant option
		/// </summary>
		public CandidTag Tag { get; }

		/// <summary>
		/// The value of the chosen variant option, whose type is based on the option
		/// </summary>
		public CandidValue Value { get; }

		/// <param name="tag">The tag (id/name) of the chosen variant option</param>
		/// <param name="value">The value of the chosen variant option, whose type is based on the option</param>
		public CandidVariant(CandidTag tag, CandidValue? value = null)
		{
			this.Tag = tag;
			this.Value = value ?? CandidPrimitive.Null();
		}

		/// <inheritdoc />
		internal override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			CandidVariantType t;
			if (type is CandidReferenceType r)
			{
				t = (CandidVariantType)getReferencedType(r.Id);
			}
			else
			{
				t = (CandidVariantType)type;
			}
			int index = t.Options.AsEnumerable()
				.OrderBy(f => f.Key)
				.ToList()
				.FindIndex(f => f.Key == this.Tag);
			if (index < 0)
			{
				throw new InvalidCandidException($"Variant option '{this.Tag}' was not found in the type", null);
			}
			// bytes = index (LEB128) + encoded value
			return LEB128.EncodeUnsigned((uint)index)
				.Concat(this.Value.EncodeValue(t.Options[this.Tag], getReferencedType))
				.ToArray();
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Tag.Id, this.Value);
		}

		/// <inheritdoc />
		public override bool Equals(CandidValue? other)
		{
			if (other is CandidVariant v)
			{
				return this.Tag.Id == v.Tag.Id && this.Value.Equals(v.Value);
			}
			return false;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"{{{this.Tag}:{this.Value}}}";
		}
	}

}
