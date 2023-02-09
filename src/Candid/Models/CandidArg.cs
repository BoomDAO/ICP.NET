using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Exceptions;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Parsers;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A model representing a candid arg. Used as the list of arguments for a function
	/// </summary>
	public class CandidArg : IHashable, IEquatable<CandidArg>
	{
		/// <summary>
		/// Order list of typed values for the arg
		/// </summary>
		public List<CandidTypedValue> Values { get; }

		/// <param name="values">Order list of typed values for the arg</param>
		public CandidArg(List<CandidTypedValue> values)
		{
			this.Values = values;
		}

		/// <inheritdoc />
		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Encode());
		}

		/// <summary>
		/// Encodes the candid arg into a byte array which can be used in sending requests to
		/// a canister
		/// </summary>
		/// <returns></returns>
		public byte[] Encode()
		{
			ArrayBufferWriter<byte> bufferWriter = new();
			this.Encode(bufferWriter);
			return bufferWriter.WrittenMemory.ToArray();
		}

		/// <summary>
		/// Encodes the candid arg into a byte array which can be used in sending requests to
		/// a canister
		/// </summary>
		/// <returns></returns>
		public void Encode(IBufferWriter<byte> destination)
		{
			// Header
			int bytesWritten = Encoding.UTF8.GetBytes("DIDL", destination.GetSpan(4));
			destination.Advance(bytesWritten);

			// Type table
			CompoundTypeTable compoundTypeTable = CompoundTypeTable.FromTypes(this.Values);

			compoundTypeTable.Encode(destination); // Encode type table


			// Types
			LEB128.EncodeSigned(this.Values.Count, destination); // Encode type count
			foreach (CandidTypedValue typedValue in this.Values)
			{
				typedValue.Type.Encode(compoundTypeTable, destination); // Encode type
			}

			// Build method to resolve the referenced types
			Func<CandidId, CandidCompoundType> getReferenceType = (id) => compoundTypeTable.GetReferenceById(id).Type;

			// Values
			foreach (CandidTypedValue typedValue in this.Values)
			{
				typedValue.Value.EncodeValue(typedValue.Type, getReferenceType, destination); // Encode value
			}
		}

		/// <summary>
		/// Takes the first arg value and converts it to the specified type
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The converted object of the first arg value</returns>
		public T1 ToObjects<T1>(CandidConverter? candidConverter = null)
		{
			return this.Values[0].ToObject<T1>(candidConverter);
		}

		/// <summary>
		/// Takes the arg values 1->2 and converts them to the specified types
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <typeparam name="T2">The type to convert the second arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The tuple of all specified arg values</returns>
		public (T1, T2) ToObjects<T1, T2>(CandidConverter? candidConverter = null)
		{
			return (
				this.Values[0].ToObject<T1>(candidConverter),
				this.Values[1].ToObject<T2>(candidConverter)
			);
		}

		/// <summary>
		/// Takes the arg values 1->3 and converts them to the specified types
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <typeparam name="T2">The type to convert the second arg value to</typeparam>
		/// <typeparam name="T3">The type to convert the third arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The tuple of all specified arg values</returns>
		public (T1, T2, T3) ToObjects<T1, T2, T3>(CandidConverter? candidConverter = null)
		{
			return (
				this.Values[0].ToObject<T1>(candidConverter),
				this.Values[1].ToObject<T2>(candidConverter),
				this.Values[2].ToObject<T3>(candidConverter)
			);
		}

		/// <summary>
		/// Takes the arg values 1->4 and converts them to the specified types
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <typeparam name="T2">The type to convert the second arg value to</typeparam>
		/// <typeparam name="T3">The type to convert the third arg value to</typeparam>
		/// <typeparam name="T4">The type to convert the fourth arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The tuple of all specified arg values</returns>
		public (T1, T2, T3, T4) ToObjects<T1, T2, T3, T4>(CandidConverter? candidConverter = null)
		{
			return (
				this.Values[0].ToObject<T1>(candidConverter),
				this.Values[1].ToObject<T2>(candidConverter),
				this.Values[2].ToObject<T3>(candidConverter),
				this.Values[3].ToObject<T4>(candidConverter)
			);
		}

		/// <summary>
		/// Takes the arg values 1->5 and converts them to the specified types
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <typeparam name="T2">The type to convert the second arg value to</typeparam>
		/// <typeparam name="T3">The type to convert the third arg value to</typeparam>
		/// <typeparam name="T4">The type to convert the fourth arg value to</typeparam>
		/// <typeparam name="T5">The type to convert the fifth arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The tuple of all specified arg values</returns>
		public (T1, T2, T3, T4, T5) ToObjects<T1, T2, T3, T4, T5>(CandidConverter? candidConverter = null)
		{
			return (
				this.Values[0].ToObject<T1>(candidConverter),
				this.Values[1].ToObject<T2>(candidConverter),
				this.Values[2].ToObject<T3>(candidConverter),
				this.Values[3].ToObject<T4>(candidConverter),
				this.Values[4].ToObject<T5>(candidConverter)
			);
		}

		/// <summary>
		/// Takes the arg value 1->6 and converts them to the specified types
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <typeparam name="T2">The type to convert the second arg value to</typeparam>
		/// <typeparam name="T3">The type to convert the third arg value to</typeparam>
		/// <typeparam name="T4">The type to convert the fourth arg value to</typeparam>
		/// <typeparam name="T5">The type to convert the fifth arg value to</typeparam>
		/// <typeparam name="T6">The type to convert the sixth arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The tuple of all specified arg values</returns>
		public (T1, T2, T3, T4, T5, T6) ToObjects<T1, T2, T3, T4, T5, T6>(CandidConverter? candidConverter = null)
		{
			return (
				this.Values[0].ToObject<T1>(candidConverter),
				this.Values[1].ToObject<T2>(candidConverter),
				this.Values[2].ToObject<T3>(candidConverter),
				this.Values[3].ToObject<T4>(candidConverter),
				this.Values[4].ToObject<T5>(candidConverter),
				this.Values[5].ToObject<T6>(candidConverter)
			);
		}

		/// <summary>
		/// Takes the arg value 1->7 and converts them to the specified types
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <typeparam name="T2">The type to convert the second arg value to</typeparam>
		/// <typeparam name="T3">The type to convert the third arg value to</typeparam>
		/// <typeparam name="T4">The type to convert the fourth arg value to</typeparam>
		/// <typeparam name="T5">The type to convert the fifth arg value to</typeparam>
		/// <typeparam name="T6">The type to convert the sixth arg value to</typeparam>
		/// <typeparam name="T7">The type to convert the seventh arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The tuple of all specified arg values</returns>
		public (T1, T2, T3, T4, T5, T6, T7) ToObjects<T1, T2, T3, T4, T5, T6, T7>(CandidConverter? candidConverter = null)
		{
			return (
				this.Values[0].ToObject<T1>(candidConverter),
				this.Values[1].ToObject<T2>(candidConverter),
				this.Values[2].ToObject<T3>(candidConverter),
				this.Values[3].ToObject<T4>(candidConverter),
				this.Values[4].ToObject<T5>(candidConverter),
				this.Values[5].ToObject<T6>(candidConverter),
				this.Values[6].ToObject<T7>(candidConverter)
			);
		}

		/// <summary>
		/// Takes the arg value 1->8 and converts them to the specified types
		/// </summary>
		/// <typeparam name="T1">The type to convert the first arg value to</typeparam>
		/// <typeparam name="T2">The type to convert the second arg value to</typeparam>
		/// <typeparam name="T3">The type to convert the third arg value to</typeparam>
		/// <typeparam name="T4">The type to convert the fourth arg value to</typeparam>
		/// <typeparam name="T5">The type to convert the fifth arg value to</typeparam>
		/// <typeparam name="T6">The type to convert the sixth arg value to</typeparam>
		/// <typeparam name="T7">The type to convert the seventh arg value to</typeparam>
		/// <typeparam name="T8">The type to convert the eighth arg value to</typeparam>
		/// <param name="candidConverter">Optional. Specifies the converter to use, othewise uses the default</param>
		/// <returns>The tuple of all specified arg values</returns>
		public (T1, T2, T3, T4, T5, T6, T7, T8) ToObjects<T1, T2, T3, T4, T5, T6, T7, T8>(CandidConverter? candidConverter = null)
		{
			return (
				this.Values[0].ToObject<T1>(candidConverter),
				this.Values[1].ToObject<T2>(candidConverter),
				this.Values[2].ToObject<T3>(candidConverter),
				this.Values[3].ToObject<T4>(candidConverter),
				this.Values[4].ToObject<T5>(candidConverter),
				this.Values[5].ToObject<T6>(candidConverter),
				this.Values[6].ToObject<T7>(candidConverter),
				this.Values[7].ToObject<T8>(candidConverter)
			);
		}

		/// <summary>
		/// Decodes a byte array into a candid arg value. Must be a valid encoded candid arg value
		/// </summary>
		/// <param name="value">Encoded candid arg value</param>
		/// <exception cref="CandidDecodingException">Throws if the bytes are not valid Candid</exception>
		/// <exception cref="InvalidCandidException">Throws if the the candid does not follow the specification</exception>
		/// <returns>Candid arg value</returns>
		public static CandidArg FromBytes(byte[] value)
		{
			return CandidByteParser.Parse(value);
		}

		/// <summary>
		/// Converts an ordered list of typed values to a candid arg value
		/// </summary>
		/// <param name="values">Ordered list of typed values</param>
		/// <returns>Candid arg value</returns>
		public static CandidArg FromCandid(List<CandidTypedValue> values)
		{
			return new CandidArg(values);
		}

		/// <summary>
		/// Converts an ordered array of typed values to a candid arg value
		/// </summary>
		/// <param name="values">Ordered array of typed values</param>
		/// <returns>Candid arg value</returns>
		public static CandidArg FromCandid(params CandidTypedValue[] values)
		{
			return new CandidArg(values.ToList());
		}

		/// <summary>
		/// Helper method to create a candid arg with no typed values
		/// </summary>
		/// <returns>Candid arg value</returns>
		public static CandidArg Empty()
		{
			return new CandidArg(new List<CandidTypedValue>());
		}

		/// <inheritdoc />
		public override string ToString()
		{
			IEnumerable<string> args = this.Values.Select(v => v.Value.ToString()!);
			return $"({string.Join(",", args)})";
		}

		/// <inheritdoc />
		public bool Equals(CandidArg? other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			return this.Values.SequenceEqual(other.Values);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return this.Equals(obj as CandidArg);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Values.Select(v => v.GetHashCode()));
		}

		/// <inheritdoc />
		public static bool operator ==(CandidArg? v1, CandidArg? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		/// <inheritdoc />
		public static bool operator !=(CandidArg? v1, CandidArg? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}
	}
}