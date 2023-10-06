using EdjCase.ICP.Candid.Encodings;
using System;
using System.Linq;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Exceptions;
using System.Buffers;

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
			this.Value = value ?? Null();
		}

		/// <inheritdoc />
		internal override void EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType, IBufferWriter<byte> destination)
		{
			CandidVariantType t = DereferenceType<CandidVariantType>(type, getReferencedType);
			// Order the options by key and get the index of the variant option
			// This is used for encoding vs the tag value
			uint optionIndex = 0;
			foreach(var option in t.Options.AsEnumerable().OrderBy(o => o.Key))
			{
				if (option.Key == this.Tag)
				{
					// If the tag was matched, then use this index
					break;
				}
				optionIndex++;
			}
			if (optionIndex >= t.Options.Count)
			{
				throw new InvalidCandidException($"Variant option '{this.Tag}' was not found in the type", null);
			}
			// bytes = index (LEB128) + encoded value
			LEB128.EncodeUnsigned(optionIndex, destination);// Encode chosen option index
			this.Value.EncodeValue(t.Options[this.Tag], getReferencedType, destination); // Encode option value
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
