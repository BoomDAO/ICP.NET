using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Buffers;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// A candid type that is NOT a reference type. This type is known before any resolution
	/// </summary>
	public abstract class CandidKnownType : CandidType
	{
		/// <summary>
		/// The candid type that this model represents
		/// </summary>
		public abstract CandidTypeCode Type { get; }
	}

	/// <summary>
	/// The base candid type model that all candid types inherit
	/// </summary>
	public abstract class CandidType : IEquatable<CandidType>
	{
		internal abstract void Encode(CompoundTypeTable compoundTypeTable, IBufferWriter<byte> destination);



		/// <inheritdoc />
		public abstract override bool Equals(object? obj);

		/// <inheritdoc />
		public abstract override int GetHashCode();

		/// <inheritdoc />
		public override string ToString()
		{
			return CandidTextGenerator.Generate(this);
		}

		/// <summary>
		/// Checks for equality of this type against the specified type
		/// </summary>
		/// <param name="other">Another type to compare against</param>
		/// <returns>True if they are structurally the same, otherwise false</returns>
		public bool Equals(CandidType? other)
		{
			return this.Equals(other as object);
		}

		/// <inheritdoc />
		public static bool operator ==(CandidType? def1, CandidType? def2)
		{
			if (ReferenceEquals(def1, null))
			{
				return ReferenceEquals(def2, null);
			}
			return def1.Equals(def2);
		}

				/// <inheritdoc />
		public static bool operator !=(CandidType? def1, CandidType? def2)
		{
			if (ReferenceEquals(def1, null))
			{
				return !ReferenceEquals(def2, null);
			}
			return !def1.Equals(def2);
		}

		/// <summary>
		/// Helper method to create a Text candid type
		/// </summary>
		/// <returns>Text candid type</returns>
		public static CandidPrimitiveType Text()
		{
			return new CandidPrimitiveType(PrimitiveType.Text);
		}

		/// <summary>
		/// Helper method to create a Nat candid type
		/// </summary>
		/// <returns>Nat candid type</returns>
		public static CandidPrimitiveType Nat()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat);
		}

		/// <summary>
		/// Helper method to create a Nat8 candid type
		/// </summary>
		/// <returns>Nat8 candid type</returns>
		public static CandidPrimitiveType Nat8()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat8);
		}

		/// <summary>
		/// Helper method to create a Nat16 candid type
		/// </summary>
		/// <returns>Nat16 candid type</returns>
		public static CandidPrimitiveType Nat16()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat16);
		}

		/// <summary>
		/// Helper method to create a Nat32 candid type
		/// </summary>
		/// <returns>Nat32 candid type</returns>
		public static CandidPrimitiveType Nat32()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat32);
		}

		/// <summary>
		/// Helper method to create a Nat64 candid type
		/// </summary>
		/// <returns>Nat64 candid type</returns>
		public static CandidPrimitiveType Nat64()
		{
			return new CandidPrimitiveType(PrimitiveType.Nat64);
		}

		/// <summary>
		/// Helper method to create a Int candid type
		/// </summary>
		/// <returns>Int candid type</returns>
		public static CandidPrimitiveType Int()
		{
			return new CandidPrimitiveType(PrimitiveType.Int);
		}

		/// <summary>
		/// Helper method to create a Int8 candid type
		/// </summary>
		/// <returns>Int8 candid type</returns>
		public static CandidPrimitiveType Int8()
		{
			return new CandidPrimitiveType(PrimitiveType.Int8);
		}

		/// <summary>
		/// Helper method to create a Int16 candid type
		/// </summary>
		/// <returns>Int16 candid type</returns>
		public static CandidPrimitiveType Int16()
		{
			return new CandidPrimitiveType(PrimitiveType.Int16);
		}

		/// <summary>
		/// Helper method to create a Int32 candid type
		/// </summary>
		/// <returns>Int32 candid type</returns>
		public static CandidPrimitiveType Int32()
		{
			return new CandidPrimitiveType(PrimitiveType.Int32);
		}

		/// <summary>
		/// Helper method to create a Int64 candid type
		/// </summary>
		/// <returns>Int64 candid type</returns>
		public static CandidPrimitiveType Int64()
		{
			return new CandidPrimitiveType(PrimitiveType.Int64);
		}

		/// <summary>
		/// Helper method to create a Float32 candid type
		/// </summary>
		/// <returns>Float32 candid type</returns>
		public static CandidPrimitiveType Float32()
		{
			return new CandidPrimitiveType(PrimitiveType.Float32);
		}

		/// <summary>
		/// Helper method to create a Float64 candid type
		/// </summary>
		/// <returns>Float64 candid type</returns>
		public static CandidPrimitiveType Float64()
		{
			return new CandidPrimitiveType(PrimitiveType.Float64);
		}

		/// <summary>
		/// Helper method to create a Bool candid type
		/// </summary>
		/// <returns>Bool candid type</returns>
		public static CandidPrimitiveType Bool()
		{
			return new CandidPrimitiveType(PrimitiveType.Bool);
		}

		/// <summary>
		/// Helper method to create a Principal candid type
		/// </summary>
		/// <returns>Principal candid type</returns>
		public static CandidPrimitiveType Principal()
		{
			return new CandidPrimitiveType(PrimitiveType.Principal);
		}

		/// <summary>
		/// Helper method to create a Blob/Vec Nat8 candid type
		/// </summary>
		/// <returns>Blob/Vec Nat8 candid type</returns>
		public static CandidVectorType Blob()
		{
			return new CandidVectorType(CandidType.Nat8());
		}

		/// <summary>
		/// Helper method to create a Null candid type
		/// </summary>
		/// <returns>Null candid type</returns>
		public static CandidPrimitiveType Null()
		{
			return new CandidPrimitiveType(PrimitiveType.Null);
		}

		/// <summary>
		/// Helper method to create a Reserved candid type
		/// </summary>
		/// <returns>Reserved candid type</returns>
		public static CandidPrimitiveType Reserved()
		{
			return new CandidPrimitiveType(PrimitiveType.Reserved);
		}

		/// <summary>
		/// Helper method to create a Empty candid type
		/// </summary>
		/// <returns>Empty candid type</returns>
		public static CandidPrimitiveType Empty()
		{
			return new CandidPrimitiveType(PrimitiveType.Empty);
		}

		/// <summary>
		/// Helper method to create a Opt candid type
		/// </summary>
		/// <returns>Opt candid type</returns>
		public static CandidOptionalType Opt(CandidType inner)
		{
			return new CandidOptionalType(inner);
		}
	}
}
