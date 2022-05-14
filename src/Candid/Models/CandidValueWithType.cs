using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
using System;

namespace ICP.Candid.Models
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

        public static CandidValueWithType FromValueAndType(CandidValue value, CandidType type)
        {
            // TODO validate
            return new CandidValueWithType(value, type);
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
	}
}
