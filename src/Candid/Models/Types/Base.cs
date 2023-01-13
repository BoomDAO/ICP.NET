using System;

namespace EdjCase.ICP.Candid.Models.Types
{
	public abstract class CandidKnownType : CandidType
	{
		public abstract CandidTypeCode Type { get; }
	}

	public abstract class CandidType : IEquatable<CandidType>
	{

		public abstract override bool Equals(object? obj);

		public abstract override int GetHashCode();
		public override string ToString()
		{
			return CandidTextGenerator.Generate(this);
		}

		public abstract byte[] Encode(CompoundTypeTable compoundTypeTable);

		public bool Equals(CandidType? other)
		{
			return this.Equals(other as object);
		}

		public static bool operator ==(CandidType? def1, CandidType? def2)
		{
			if (object.ReferenceEquals(def1, null))
			{
				return object.ReferenceEquals(def2, null);
			}
			return def1.Equals(def2);
		}

		public static bool operator !=(CandidType? def1, CandidType? def2)
		{
			if (object.ReferenceEquals(def1, null))
			{
				return !object.ReferenceEquals(def2, null);
			}
			return !def1.Equals(def2);
		}

	}
}