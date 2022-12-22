using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Models
{
	public class CandidTypedValue : IEquatable<CandidTypedValue>
	{
		public CandidValue Value { get; }
		public CandidType Type { get; }

		public CandidTypedValue(CandidValue value, CandidType type)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
		}

		public OptionalValue<T> ToOptionalObject<T>(CandidConverter? converter = null)
		{
			return (converter ?? CandidConverter.Default).ToOptionalObject<T>(this.Value);
		}

		public T ToObject<T>(CandidConverter? converter = null)
		{
			return this.ToOptionalObject<T>(converter).GetValueOrThrow();
		}

		public static CandidTypedValue FromValueAndType(CandidValue value, CandidType type)
		{
			return new CandidTypedValue(value, type);
		}

		public static CandidTypedValue FromObject(object obj, CandidConverter? converter = null)
		{
			return (converter ?? CandidConverter.Default).FromObject(obj);
		}


		public bool Equals(CandidTypedValue? other)
		{
			if (object.ReferenceEquals(other, null))
			{
				return false;
			}
			return this.Value == other.Value && this.Type == other.Type;
		}


		public override bool Equals(object? obj)
		{
			return this.Equals(obj as CandidTypedValue);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Value, this.Type);
		}

		public static bool operator ==(CandidTypedValue? v1, CandidTypedValue? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		public static bool operator !=(CandidTypedValue? v1, CandidTypedValue? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}

		public static CandidTypedValue Null()
		{
			CandidPrimitive value = CandidPrimitive.Null();
			CandidPrimitiveType type = new CandidPrimitiveType(PrimitiveType.Null);
			return new CandidTypedValue(value, type);
		}

		public static CandidTypedValue Opt(CandidTypedValue typedValue)
		{
			return new CandidTypedValue(
				new CandidOptional(typedValue.Value),
				new CandidOptionalType(typedValue.Type)
			);
		}

	}
}
