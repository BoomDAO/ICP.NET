using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Models
{
    public class CandidValueWithType
    {
        public CandidValue Value { get; }
        public CandidType Type { get; }

        private CandidValueWithType(CandidValue value, CandidType type)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
		}
		public T? ToObjectOrDefault<T>(CandidConverter? converter = null)
		{
			return (converter ?? CandidConverter.Default).ToObject<T>(this.Value);
		}

		public T ToObject<T>(CandidConverter? converter = null)
		{
			return this.ToObjectOrDefault<T>(converter) ?? throw new Exception("Candid value is null");
		}

		public static CandidValueWithType FromValueAndType(CandidValue value, CandidType type)
        {
            // TODO validate
            return new CandidValueWithType(value, type);
		}

		public static CandidValueWithType FromObject<T>(T obj, CandidConverter? converter = null)
		{
			return (converter ?? CandidConverter.Default).FromObject(obj);
		}


		public bool Equals(CandidValueWithType? other)
		{
			if(object.ReferenceEquals(other, null))
            {
				return false;
            }
			return this.Value == other.Value && this.Type == other.Type;
		}


        public override bool Equals(object? obj)
		{
			return this.Equals(obj as CandidValueWithType);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Value, this.Type);
		}

		public static bool operator ==(CandidValueWithType? v1, CandidValueWithType? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		public static bool operator !=(CandidValueWithType? v1, CandidValueWithType? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}

		public static CandidValueWithType Null()
		{
            CandidPrimitive value = CandidPrimitive.Null();
            CandidPrimitiveType type = new CandidPrimitiveType(PrimitiveType.Null);
			return new CandidValueWithType(value, type);
		}
	}
}
