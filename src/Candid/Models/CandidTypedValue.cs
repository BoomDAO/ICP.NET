using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A model representing a candid type and value combination. The type and value must match
	/// </summary>
	public class CandidTypedValue : IEquatable<CandidTypedValue>
	{
		/// <summary>
		/// The candid value
		/// </summary>
		public CandidValue Value { get; }

		/// <summary>
		/// The candid type
		/// </summary>
		public CandidType Type { get; }

		/// <param name="value">The candid value</param>
		/// <param name="type">The candid type</param>
		public CandidTypedValue(CandidValue value, CandidType type)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
		}

		/// <summary>
		/// Helper method to convert a typed value to an optional value
		/// </summary>
		/// <typeparam name="T">Type to convert the candid value to</typeparam>
		/// <param name="converter">Optional. Converter to use for the conversion, otherwise will use default converter</param>
		/// <returns>Optional value of T</returns>
		public OptionalValue<T> ToOptionalObject<T>(CandidConverter? converter = null)
		{
			return (converter ?? CandidConverter.Default).ToOptionalObject<T>(this.Value);
		}

		/// <summary>
		/// Helper method to convert a typed value to an generic type value
		/// </summary>
		/// <typeparam name="T">Type to convert the candid value to</typeparam>
		/// <param name="converter">Optional. Converter to use for the conversion, otherwise will use default converter</param>
		/// <returns>Value of type T</returns>
		public T ToObject<T>(CandidConverter? converter = null)
		{
			return this.ToOptionalObject<T>(converter).GetValueOrThrow();
		}

		/// <inheritdoc cref="CandidValue.AsPrimitive"/>
		public CandidPrimitive AsPrimitive()
		{
			return this.Value.AsPrimitive();
		}

		/// <inheritdoc cref="CandidValue.AsVector"/>
		public CandidVector AsVector()
		{
			return this.Value.AsVector();
		}

		/// <inheritdoc cref="CandidValue.AsVectorAsList{T}(Func{CandidValue, T})"/>
		public List<T> AsVectorAsList<T>(Func<CandidValue, T> converter)
		{
			return this.Value.AsVectorAsList(converter);
		}

		/// <inheritdoc cref="CandidValue.AsVectorAsArray{T}(Func{CandidValue, T})"/>
		public T[] AsVectorAsArray<T>(Func<CandidValue, T> converter)
		{
			return this.Value.AsVectorAsArray(converter);
		}

		/// <inheritdoc cref="CandidValue.IsNull"/>
		public bool IsNull()
		{
			return this.Value.IsNull();
		}

		/// <inheritdoc cref="CandidValue.AsRecord"/>
		public CandidRecord AsRecord()
		{
			return this.Value.AsRecord();
		}

		/// <inheritdoc cref="CandidValue.AsRecord{T}(Func{CandidRecord, T})"/>
		public T AsRecord<T>(Func<CandidRecord, T> converter)
		{
			return this.Value.AsRecord(converter);
		}

		/// <inheritdoc cref="CandidValue.AsVariant"/>
		public CandidVariant AsVariant()
		{
			return this.Value.AsVariant();
		}

		/// <inheritdoc cref="CandidValue.AsVariant{T}(Func{CandidVariant, T})"/>
		public T AsVariant<T>(Func<CandidVariant, T> converter)
		{
			return this.Value.AsVariant(converter);
		}

		/// <inheritdoc cref="CandidValue.AsFunc"/>
		public CandidFunc AsFunc()
		{
			return this.Value.AsFunc();
		}

		/// <inheritdoc cref="CandidValue.AsService"/>
		public CandidService AsService()
		{
			return this.Value.AsService();
		}

		/// <inheritdoc cref="CandidValue.AsOptional"/>
		public CandidOptional AsOptional()
		{
			return this.Value.AsOptional();
		}

		/// <inheritdoc cref="CandidValue.AsOptional{T}(Func{CandidValue, T})"/>
		public OptionalValue<T> AsOptional<T>(Func<CandidValue, T> valueConverter)
		{
			return this.Value.AsOptional(valueConverter);
		}

		/// <inheritdoc cref="CandidValue.AsText"/>
		public string AsText()
		{
			return this.Value.AsText();
		}

		/// <inheritdoc cref="CandidValue.AsNat"/>
		public UnboundedUInt AsNat()
		{
			return this.Value.AsNat();
		}

		/// <inheritdoc cref="CandidValue.AsNat8"/>
		public byte AsNat8()
		{
			return this.Value.AsNat8();
		}

		/// <inheritdoc cref="CandidValue.AsNat16"/>
		public ushort AsNat16()
		{
			return this.Value.AsNat16();
		}

		/// <inheritdoc cref="CandidValue.AsNat32"/>
		public uint AsNat32()
		{
			return this.Value.AsNat32();
		}

		/// <inheritdoc cref="CandidValue.AsNat64"/>
		public ulong AsNat64()
		{
			return this.Value.AsNat64();
		}

		/// <inheritdoc cref="CandidValue.AsInt"/>
		public UnboundedInt AsInt()
		{
			return this.Value.AsInt();
		}

		/// <inheritdoc cref="CandidValue.AsInt8"/>
		public sbyte AsInt8()
		{
			return this.Value.AsInt8();
		}

		/// <inheritdoc cref="CandidValue.AsInt16"/>
		public short AsInt16()
		{
			return this.Value.AsInt16();
		}

		/// <inheritdoc cref="CandidValue.AsInt32"/>
		public int AsInt32()
		{
			return this.Value.AsInt32();
		}

		/// <inheritdoc cref="CandidValue.AsInt64"/>
		public long AsInt64()
		{
			return this.Value.AsInt64();
		}

		/// <inheritdoc cref="CandidValue.AsFloat32"/>
		public float AsFloat32()
		{
			return this.Value.AsFloat32();
		}

		/// <inheritdoc cref="CandidValue.AsFloat64"/>
		public double AsFloat64()
		{
			return this.Value.AsFloat64();
		}

		/// <inheritdoc cref="CandidValue.AsBool"/>
		public bool AsBool()
		{
			return this.Value.AsBool();
		}

		/// <inheritdoc cref="CandidValue.AsPrincipal"/>
		public Principal AsPrincipal()
		{
			return this.Value.AsPrincipal();
		}
		
		/// <summary>
		/// Helper method to convert a type and a value to a typed value. Type and value must match 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="type"></param>
		/// <returns></returns>
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

		public static CandidTypedValue Text(string value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Text(value),
				CandidType.Text()
			);
		}

		public static CandidTypedValue Nat(UnboundedUInt value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Nat(value),
				CandidType.Nat()
			);
		}

		public static CandidTypedValue Nat8(byte value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Nat8(value),
				CandidType.Nat8()
			);
		}

		public static CandidTypedValue Nat16(ushort value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Nat16(value),
				CandidType.Nat16()
			);
		}

		public static CandidTypedValue Nat32(uint value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Nat32(value),
				CandidType.Nat32()
			);
		}

		public static CandidTypedValue Nat64(ulong value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Nat64(value),
				CandidType.Nat64()
			);
		}

		public static CandidTypedValue Int(UnboundedInt value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Int(value),
				CandidType.Int()
			);
		}

		public static CandidTypedValue Int8(sbyte value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Int8(value),
				CandidType.Int8()
			);
		}

		public static CandidTypedValue Int16(short value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Int16(value),
				CandidType.Int16()
			);
		}

		public static CandidTypedValue Int32(int value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Int32(value),
				CandidType.Int32()
			);
		}

		public static CandidTypedValue Int64(long value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Int64(value),
				CandidType.Int64()
			);
		}

		public static CandidTypedValue Float32(float value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Float32(value),
				CandidType.Float32()
			);
		}

		public static CandidTypedValue Float64(double value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Float64(value),
				CandidType.Float64()
			);
		}

		public static CandidTypedValue Bool(bool value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Bool(value),
				CandidType.Bool()
			);
		}
		public static CandidTypedValue Principal(Principal value)
		{
			return new CandidTypedValue(
				CandidPrimitive.Principal(value),
				CandidType.Principal()
			);
		}

		public static CandidTypedValue Null()
		{
			return new CandidTypedValue(
				CandidPrimitive.Null(),
				CandidType.Null()
			);
		}

		public static CandidTypedValue Reserved()
		{
			return new CandidTypedValue(
				CandidPrimitive.Reserved(),
				CandidType.Reserved()
			);
		}

		public static CandidTypedValue Empty()
		{
			return new CandidTypedValue(
				CandidPrimitive.Empty(),
				CandidType.Empty()
			);
		}

		public static CandidTypedValue Opt(CandidTypedValue typedValue)
		{
			return new CandidTypedValue(
				new CandidOptional(typedValue.Value),
				new CandidOptionalType(typedValue.Type)
			);
		}

		public static CandidTypedValue Vector<T>(
			CandidType innerType,
			IEnumerable<T> values,
			Func<T, CandidValue> valueConverter)
		{
			return new CandidTypedValue(
				new CandidVector(values.Select(valueConverter).ToArray()),
				new CandidVectorType(innerType)
			);
		}

		public static CandidTypedValue Vector(
			CandidType innerType, 
			CandidValue[] values
		)
		{
			return new CandidTypedValue(
				new CandidVector(values),
				new CandidVectorType(innerType)
			);
		}
	}
}
