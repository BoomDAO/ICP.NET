using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Buffers;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	/// <summary>
	/// A model representing the value of a candid opt
	/// </summary>
	public class CandidOptional : CandidValue
	{
		/// <inheritdoc />
		public override CandidValueType Type { get; } = CandidValueType.Optional;

		/// <summary>
		/// The inner value of an opt. If not set, will be a candid null value
		/// </summary>
		public CandidValue Value { get; }

		/// <param name="value">The inner value of an opt. If not set, will be a candid null value</param>
		public CandidOptional(CandidValue? value = null)
		{
			this.Value = value ?? Null();
		}

		/// <inheritdoc />
		internal override void EncodeValue(
			CandidType type,
			Func<CandidId, CandidCompoundType> getReferencedType,
			IBufferWriter<byte> destination
		)
		{
			CandidOptionalType t = DereferenceType<CandidOptionalType>(type, getReferencedType);
			if (this.Value.Type == CandidValueType.Primitive
				&& this.Value.AsPrimitive().ValueType == PrimitiveType.Null)
			{
				destination.WriteOne<byte>(0); // Encode null
				return;
			}
			destination.WriteOne<byte>(1); // Encode not null
			this.Value.EncodeValue(t.Value, getReferencedType, destination);
		}


		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Value);
		}

		/// <inheritdoc />
		public override bool Equals(CandidValue? other)
		{
			if (other is CandidOptional o)
			{
				return this.Value.Equals(o.Value);
			}
			return false;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return $"opt {this.Value}";
		}
	}
}