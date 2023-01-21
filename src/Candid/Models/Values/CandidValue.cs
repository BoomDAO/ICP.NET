using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	public enum CandidValueType
	{
		Primitive,
		Vector,
		Record,
		Variant,
		Func,
		Service,
		Optional,
		Principal
	}

	public abstract class CandidValue : IEquatable<CandidValue>
	{
		public abstract CandidValueType Type { get; }

		public abstract byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType);
		public abstract override int GetHashCode();
		public abstract bool Equals(CandidValue? other);
		public abstract override string ToString();


		public override bool Equals(object? obj)
		{
			if (obj is CandidValue v)
			{
				return this.Equals(v);
			}
			return false;
		}

		public static bool operator ==(CandidValue v1, CandidValue v2)
		{
			return v1.Equals(v2);
		}

		public static bool operator !=(CandidValue v1, CandidValue v2)
		{
			return !v1.Equals(v2);
		}

		public CandidPrimitive AsPrimitive()
		{
			this.ValidateType(CandidValueType.Primitive);
			return (CandidPrimitive)this;
		}

		public CandidVector AsVector()
		{
			this.ValidateType(CandidValueType.Vector);
			return (CandidVector)this;
		}

		public List<T> AsVectorAsList<T>(Func<CandidValue, T> converter)
		{
			CandidVector vector = this.AsVector();
			return vector.Values
				.Select(v => converter(v))
				.ToList();
		}

		public T[] AsVectorAsArray<T>(Func<CandidValue, T> converter)
		{
			CandidVector vector = this.AsVector();
			return vector.Values
				.Select(v => converter(v))
				.ToArray();
		}

		public bool IsNull()
		{
			return this is CandidPrimitive p && p.ValueType == PrimitiveType.Null;
		}

		public CandidRecord AsRecord()
		{
			this.ValidateType(CandidValueType.Record);
			return (CandidRecord)this;
		}
		public T AsRecord<T>(Func<CandidRecord, T> converter)
		{
			CandidRecord record = this.AsRecord();
			return converter(record);
		}

		public CandidVariant AsVariant()
		{
			this.ValidateType(CandidValueType.Variant);
			return (CandidVariant)this;
		}

		public T AsVariant<T>(Func<CandidVariant, T> converter)
		{
			CandidVariant v = this.AsVariant();
			return converter(v);
		}

		public CandidFunc AsFunc()
		{
			this.ValidateType(CandidValueType.Func);
			return (CandidFunc)this;
		}

		public CandidService AsService()
		{
			this.ValidateType(CandidValueType.Service);
			return (CandidService)this;
		}

		public CandidOptional AsOptional()
		{
			this.ValidateType(CandidValueType.Optional);
			return (CandidOptional)this;
		}

		public OptionalValue<T> AsOptional<T>(Func<CandidValue, T> valueConverter)
		{
			CandidOptional? optional = this.AsOptional();
			if (optional.Value is CandidPrimitive p && p.ValueType == PrimitiveType.Null)
			{
				return OptionalValue<T>.NoValue();
			}
			T value = valueConverter(optional.Value);
			return OptionalValue<T>.WithValue(value);
		}

		public string AsText()
		{
			return this.AsPrimitive().AsText();
		}

		public UnboundedUInt AsNat()
		{
			return this.AsPrimitive().AsNat();
		}

		public byte AsNat8()
		{
			return this.AsPrimitive().AsNat8();
		}

		public ushort AsNat16()
		{
			return this.AsPrimitive().AsNat16();
		}

		public uint AsNat32()
		{
			return this.AsPrimitive().AsNat32();
		}

		public ulong AsNat64()
		{
			return this.AsPrimitive().AsNat64();
		}

		public UnboundedInt AsInt()
		{
			return this.AsPrimitive().AsInt();
		}

		public sbyte AsInt8()
		{
			return this.AsPrimitive().AsInt8();
		}

		public short AsInt16()
		{
			return this.AsPrimitive().AsInt16();
		}

		public int AsInt32()
		{
			return this.AsPrimitive().AsInt32();
		}

		public long AsInt64()
		{
			return this.AsPrimitive().AsInt64();
		}

		public float AsFloat32()
		{
			return this.AsPrimitive().AsFloat32();
		}

		public double AsFloat64()
		{
			return this.AsPrimitive().AsFloat64();
		}

		public bool AsBool()
		{
			return this.AsPrimitive().AsBool();
		}

		/// <summary>
		/// If opaque, returns null, otherwise the principalid
		/// </summary>
		/// <returns></returns>
		public Principal AsPrincipal()
		{
			return this.AsPrimitive().AsPrincipal();
		}


		public static CandidPrimitive Text(string value)
		{
			return CandidPrimitive.Text(value);
		}

		public static CandidPrimitive Nat(UnboundedUInt value)
		{
			return CandidPrimitive.Nat(value);
		}

		public static CandidPrimitive Nat8(byte value)
		{
			return CandidPrimitive.Nat8(value);
		}

		public static CandidPrimitive Nat16(ushort value)
		{
			return CandidPrimitive.Nat16(value);
		}

		public static CandidPrimitive Nat32(uint value)
		{
			return CandidPrimitive.Nat32(value);
		}

		public static CandidPrimitive Nat64(ulong value)
		{
			return CandidPrimitive.Nat64(value);
		}

		public static CandidPrimitive Int(UnboundedInt value)
		{
			return CandidPrimitive.Int(value);
		}

		public static CandidPrimitive Int8(sbyte value)
		{
			return CandidPrimitive.Int8(value);
		}

		public static CandidPrimitive Int16(short value)
		{
			return CandidPrimitive.Int16(value);
		}

		public static CandidPrimitive Int32(int value)
		{
			return CandidPrimitive.Int32(value);
		}

		public static CandidPrimitive Int64(long value)
		{
			return CandidPrimitive.Int64(value);
		}

		public static CandidPrimitive Float32(float value)
		{
			return CandidPrimitive.Float32(value);
		}

		public static CandidPrimitive Float64(double value)
		{
			return CandidPrimitive.Float64(value);
		}

		public static CandidPrimitive Bool(bool value)
		{
			return CandidPrimitive.Bool(value);
		}
		public static CandidPrimitive Principal(Principal value)
		{
			return CandidPrimitive.Principal(value);
		}

		public static CandidPrimitive Null()
		{
			return CandidPrimitive.Null();
		}

		public static CandidPrimitive Reserved()
		{
			return CandidPrimitive.Reserved();
		}

		public static CandidPrimitive Empty()
		{
			return CandidPrimitive.Empty();
		}

		protected void ValidateType(CandidValueType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
			}
		}
	}
}
