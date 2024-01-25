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

		/// <param name="value">The candid value. Must match the specified type</param>
		/// <param name="type">The candid type. Must match the specified value</param>
		public CandidTypedValue(CandidValue value, CandidType type)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Value.ToString(); // TODO is this the best format?
		}

		/// <summary>
		/// Helper method to convert a typed value to an generic type value
		/// </summary>
		/// <typeparam name="T">Type to convert the candid value to</typeparam>
		/// <param name="converter">Optional. Converter to use for the conversion, otherwise will use default converter</param>
		/// <returns>Value of type T</returns>
		public T ToObject<T>(CandidConverter? converter = null)
		{
			return (converter ?? CandidConverter.Default).ToObject<T>(this.Value);
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
		public string? AsText()
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
		public Principal? AsPrincipal()
		{
			return this.Value.AsPrincipal();
		}

		/// <summary>
		/// Helper method to convert a type and a value to a typed value. Type and value must match 
		/// </summary>
		/// <param name="value">The candid value. Must match the specified type</param>
		/// <param name="type">The candid type. Must match the specified value</param>
		/// <returns>A candid typed value of the specified type and value</returns>
		public static CandidTypedValue FromValueAndType(CandidValue value, CandidType type)
		{
			return new CandidTypedValue(value, type);
		}

		/// <summary>
		/// Converts the object into a typed value. If a converter is not specified, the default
		/// converter will be used
		/// </summary>
		/// <param name="value">An object that can be converted into a candid type/value</param>
		/// <param name="converter">Optional. Converter to use for the conversion, otherwise will use default converter</param>
		/// <returns>A candid typed value based on the specified value</returns>
		public static CandidTypedValue FromObject<T>(T value, CandidConverter? converter = null)
			where T : notnull
		{
			converter ??= CandidConverter.Default;
			return converter.FromTypedObject(value);
		}

		/// <inheritdoc />
		public bool Equals(CandidTypedValue? other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			return this.Value == other.Value && this.Type == other.Type;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return this.Equals(obj as CandidTypedValue);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Value, this.Type);
		}

		/// <inheritdoc />
		public static bool operator ==(CandidTypedValue? v1, CandidTypedValue? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		/// <inheritdoc />
		public static bool operator !=(CandidTypedValue? v1, CandidTypedValue? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}

		/// <summary>
		/// A helper method to create a typed text value
		/// </summary>
		/// <param name="value">The value to use for the text</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Text(string value)
		{
			return new CandidTypedValue(
				CandidValue.Text(value),
				CandidType.Text()
			);
		}

		/// <summary>
		/// A helper method to create a typed nat value
		/// </summary>
		/// <param name="value">The value to use for the nat</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Nat(UnboundedUInt value)
		{
			return new CandidTypedValue(
				CandidValue.Nat(value),
				CandidType.Nat()
			);
		}

		/// <summary>
		/// A helper method to create a typed nat8 value
		/// </summary>
		/// <param name="value">The value to use for the nat8</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Nat8(byte value)
		{
			return new CandidTypedValue(
				CandidValue.Nat8(value),
				CandidType.Nat8()
			);
		}

		/// <summary>
		/// A helper method to create a typed nat16 value
		/// </summary>
		/// <param name="value">The value to use for the nat16</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Nat16(ushort value)
		{
			return new CandidTypedValue(
				CandidValue.Nat16(value),
				CandidType.Nat16()
			);
		}

		/// <summary>
		/// A helper method to create a typed nat32 value
		/// </summary>
		/// <param name="value">The value to use for the nat32</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Nat32(uint value)
		{
			return new CandidTypedValue(
				CandidValue.Nat32(value),
				CandidType.Nat32()
			);
		}

		/// <summary>
		/// A helper method to create a typed nat64 value
		/// </summary>
		/// <param name="value">The value to use for the nat64</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Nat64(ulong value)
		{
			return new CandidTypedValue(
				CandidValue.Nat64(value),
				CandidType.Nat64()
			);
		}

		/// <summary>
		/// A helper method to create a typed int value
		/// </summary>
		/// <param name="value">The value to use for the int</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Int(UnboundedInt value)
		{
			return new CandidTypedValue(
				CandidValue.Int(value),
				CandidType.Int()
			);
		}

		/// <summary>
		/// A helper method to create a typed int8 value
		/// </summary>
		/// <param name="value">The value to use for the int8</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Int8(sbyte value)
		{
			return new CandidTypedValue(
				CandidValue.Int8(value),
				CandidType.Int8()
			);
		}

		/// <summary>
		/// A helper method to create a typed int16 value
		/// </summary>
		/// <param name="value">The value to use for the int16</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Int16(short value)
		{
			return new CandidTypedValue(
				CandidValue.Int16(value),
				CandidType.Int16()
			);
		}

		/// <summary>
		/// A helper method to create a typed int32 value
		/// </summary>
		/// <param name="value">The value to use for the int32</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Int32(int value)
		{
			return new CandidTypedValue(
				CandidValue.Int32(value),
				CandidType.Int32()
			);
		}

		/// <summary>
		/// A helper method to create a typed int64 value
		/// </summary>
		/// <param name="value">The value to use for the int64</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Int64(long value)
		{
			return new CandidTypedValue(
				CandidValue.Int64(value),
				CandidType.Int64()
			);
		}

		/// <summary>
		/// A helper method to create a typed float32 value
		/// </summary>
		/// <param name="value">The value to use for the float32</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Float32(float value)
		{
			return new CandidTypedValue(
				CandidValue.Float32(value),
				CandidType.Float32()
			);
		}

		/// <summary>
		/// A helper method to create a typed float64 value
		/// </summary>
		/// <param name="value">The value to use for the float64</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Float64(double value)
		{
			return new CandidTypedValue(
				CandidValue.Float64(value),
				CandidType.Float64()
			);
		}

		/// <summary>
		/// A helper method to create a typed bool value
		/// </summary>
		/// <param name="value">The value to use for the bool</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Bool(bool value)
		{
			return new CandidTypedValue(
				CandidValue.Bool(value),
				CandidType.Bool()
			);
		}

		/// <summary>
		/// A helper method to create a typed principal value
		/// </summary>
		/// <param name="value">The value to use for the principal</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Principal(Principal value)
		{
			return new CandidTypedValue(
				CandidValue.Principal(value),
				CandidType.Principal()
			);
		}

		/// <summary>
		/// A helper method to create a typed null value
		/// </summary>
		/// <returns>A candid typed value of null</returns>
		public static CandidTypedValue Null()
		{
			return new CandidTypedValue(
				CandidValue.Null(),
				CandidType.Null()
			);
		}

		/// <summary>
		/// A helper method to create a typed reserved value
		/// </summary>
		/// <returns>A candid typed value of reserved</returns>
		public static CandidTypedValue Reserved()
		{
			return new CandidTypedValue(
				CandidValue.Reserved(),
				CandidType.Reserved()
			);
		}

		/// <summary>
		/// A helper method to create a typed empty value
		/// </summary>
		/// <returns>A candid typed value of empty</returns>
		public static CandidTypedValue Empty()
		{
			return new CandidTypedValue(
				CandidValue.Empty(),
				CandidType.Empty()
			);
		}


		/// <summary>
		/// A helper method to create a typed opt value
		/// </summary>
		/// <param name="typedValue">The inner typed value to wrap an opt around</param>
		/// <returns>A candid typed value of the specified value</returns>
		public static CandidTypedValue Opt(CandidTypedValue typedValue)
		{
			return new CandidTypedValue(
				new CandidOptional(typedValue.Value),
				new CandidOptionalType(typedValue.Type)
			);
		}

		/// <summary>
		/// A helper method to create a typed vector value
		/// </summary>
		/// <param name="innerType">The item type of the vector</param>
		/// <param name="values">An enumerable of values to use as vector items</param>
		/// <param name="valueConverter">A function to convert the enumerable type to a candid value</param>
		/// <returns>A candid typed value of the enumerable</returns>
		public static CandidTypedValue Vector<T>(
			CandidType innerType,
			IEnumerable<T> values,
			Func<T, CandidValue> valueConverter
		)
		{
			return new CandidTypedValue(
				new CandidVector(values.Select(valueConverter).ToArray()),
				new CandidVectorType(innerType)
			);
		}

		/// <summary>
		/// A helper method to create a typed vector value
		/// </summary>
		/// <param name="innerType">The item type of the vector</param>
		/// <param name="values">An array of values to use as vector items</param>
		/// <returns>A candid typed value of the array</returns>
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

		/// <summary>
		/// A helper method to create a typed record value with no fields
		/// </summary>
		/// <returns>A candid typed value of the empty record</returns>
		public static CandidTypedValue EmptyRecord()
		{
			return new CandidTypedValue(
				new CandidRecord(new Dictionary<CandidTag, CandidValue>()),
				new CandidRecordType(new Dictionary<CandidTag, CandidType>())
			);
		}
	}
}
