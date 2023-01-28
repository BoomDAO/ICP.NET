using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	/// <summary>
	/// A model representing a candid vector value
	/// </summary>
	public class CandidVector : CandidValue
	{
		/// <inheritdoc />
		public override CandidValueType Type { get; } = CandidValueType.Vector;

		/// <summary>
		/// Each candid value that the vector contains. All must be of the same type
		/// </summary>
		public CandidValue[] Values { get; }

		/// <param name="values">Each candid value that the vector contains. All must be of the same type</param>
		/// <exception cref="ArgumentException">Throws if all the values are not of the same type</exception>
		public CandidVector(CandidValue[] values)
		{
			CandidValueType? valueType = values.FirstOrDefault()?.Type;
			if (valueType != null && values.Any(v => v.Type != valueType))
			{
				throw new ArgumentException("All vector values must be the same type");
			}
			this.Values = values;
		}

		/// <inheritdoc />
		internal override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			CandidVectorType t;
			if (type is CandidReferenceType r)
			{
				t = (CandidVectorType)getReferencedType(r.Id);
			}
			else
			{
				t = (CandidVectorType)type;
			}
			byte[] valueListBytes = this.Values
				.SelectMany(v => v.EncodeValue(t.InnerType, getReferencedType))
				.ToArray();
			byte[] valueCountBytes = LEB128.EncodeSigned(this.Values.Length);
			return valueCountBytes
				.Concat(valueListBytes)
				.ToArray();
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Values.Select(v => v.GetHashCode()));
		}

		/// <inheritdoc />
		public override bool Equals(CandidValue? other)
		{
			if (other is CandidVector v)
			{
				if (this.Values.Length == v.Values.Length)
				{
					return this.Values.SequenceEqual(v.Values);
				}
			}
			return false;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			IEnumerable<string> values = this.Values.Select(v => v.ToString());
			return $"[{string.Join(", ", values)}]";
		}
	}

}
