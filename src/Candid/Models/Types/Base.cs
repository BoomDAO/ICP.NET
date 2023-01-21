using EdjCase.ICP.Candid.Models.Values;
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


		public static CandidPrimitiveType Text()
		{
			return new CandidPrimitiveType(PrimitiveType.Text);
		}

		public static CandidPrimitiveType Nat()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat);
		}

		public static CandidPrimitiveType Nat8()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat8);
		}

		public static CandidPrimitiveType Nat16()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat16);
		}

		public static CandidPrimitiveType Nat32()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat32);
		}

		public static CandidPrimitiveType Nat64()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat64);
		}

		public static CandidPrimitiveType Int()
		{
			return new CandidPrimitiveType(PrimitiveType.Int);
		}

		public static CandidPrimitiveType Int8()
		{
			return new CandidPrimitiveType(PrimitiveType.Int8);
		}

		public static CandidPrimitiveType Int16()
		{
			return new CandidPrimitiveType(PrimitiveType.Int16);
		}

		public static CandidPrimitiveType Int32()
		{
			return new CandidPrimitiveType(PrimitiveType.Int32);
		}

		public static CandidPrimitiveType Int64()
		{
			return new CandidPrimitiveType(PrimitiveType.Int64);
		}

		public static CandidPrimitiveType Float32()
		{
			return new CandidPrimitiveType(PrimitiveType.Float32);
		}

		public static CandidPrimitiveType Float64()
		{
			return new CandidPrimitiveType(PrimitiveType.Float64);
		}

		public static CandidPrimitiveType Bool()
		{
			return new CandidPrimitiveType(PrimitiveType.Bool);
		}
		public static CandidPrimitiveType Principal()
		{
			return new CandidPrimitiveType(PrimitiveType.Principal);
		}

		public static CandidPrimitiveType Null()
		{
			return new CandidPrimitiveType(PrimitiveType.Null);
		}

		public static CandidPrimitiveType Reserved()
		{
			return new CandidPrimitiveType(PrimitiveType.Reserved);
		}

		public static CandidPrimitiveType Empty()
		{
			return new CandidPrimitiveType(PrimitiveType.Empty);
		}

		public static CandidOptionalType Opt(CandidType inner)
		{
			return new CandidOptionalType(inner);
		}

	}
}