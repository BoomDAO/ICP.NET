using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Mapping
{
	/// <summary>
	/// An abstract mapper to map a C# type to and from a candid type
	/// </summary>
	public abstract class CandidValueMapper<T> : ICandidValueMapper
	{
		/// <summary>
		/// The candid type that the value will map to
		/// </summary>
		public CandidType CandidType { get; }

		/// <summary>
		/// Default constructor, requires a candid type that it maps to
		/// </summary>
		/// <param name="candidType">The candid type that the value will map to</param>
		protected CandidValueMapper(CandidType candidType)
		{
			this.CandidType = candidType;
		}



		/// <summary>
		/// Maps a candid value to a C# value.
		/// </summary>
		/// <param name="value">Candid value to map to a C# value</param>
		/// <param name="converter">The converter to use for inner types</param>
		/// <returns>C# value converted from the candid value</returns>
		public abstract T MapGeneric(CandidValue value, CandidConverter converter);

		/// <summary>
		/// Maps a C# value to a candid value and type.
		/// </summary>
		/// <param name="value">C# value to map to a candid value</param>
		/// <param name="converter">The converter to use for inner types</param>
		/// <returns>Candid value and type converted from the C# value</returns>
		public abstract CandidValue MapGeneric(T value, CandidConverter converter);

		/// <inheritdoc />
		public CandidType? GetMappedCandidType(Type type)
		{
			return typeof(T) == type ? this.CandidType : null;
		}
		
		/// <inheritdoc />
		public object Map(CandidValue value, CandidConverter converter)
		{
			return this.MapGeneric(value, converter)!;
		}

		/// <inheritdoc />
		public CandidValue Map(object value, CandidConverter converter)
		{
			return this.MapGeneric((T)value, converter);
		}
	}

	/// <summary>
	/// A mapper interface to map a C# type to and from a candid type
	/// </summary>
	public interface ICandidValueMapper
	{
		/// <summary>
		/// Indicates if the mapper can map a certain type
		/// </summary>
		/// <param name="type">The type to check against</param>
		/// <returns>True if it can map, otherwise false</returns>
		CandidType? GetMappedCandidType(Type type);

		/// <summary>
		/// Maps a candid value to a C# value.
		/// Input value will match the `CandidType` type property.
		/// Returned value should match the `Type` type property.
		/// </summary>
		/// <param name="value">Candid value to map to a C# value</param>
		/// <param name="converter">The converter to use for inner types</param>
		/// <returns>C# value converted from the candid value</returns>
		object Map(CandidValue value, CandidConverter converter);

		/// <summary>
		/// Maps a C# value to a candid value and type.
		/// Input value will match the `Type` type property.
		/// Returned value should match the `CandidType` type property.
		/// </summary>
		/// <param name="value">C# value to map to a candid value</param>
		/// <param name="converter">The converter to use for inner types</param>
		/// <returns>Candid value and type converted from the C# value</returns>
		CandidValue Map(object value, CandidConverter converter);
	}
}