using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Mapping
{
	/// <summary>
	/// A custom mapper interface to map a C# type to and from a candid type
	/// </summary>
	public interface IObjectMapper
	{
		/// <summary>
		/// Candid type to convert to/from
		/// </summary>
		CandidType CandidType { get; }

		/// <summary>
		/// C# type to convert to/from
		/// </summary>
		Type Type { get; }

		/// <summary>
		/// Maps a candid value to a C# value.
		/// Input value will match the `CandidType` type property.
		/// Returned value should match the `Type` type property.
		/// </summary>
		/// <param name="value">Candid value to map to a C# value</param>
		/// <param name="options">Options that are being used for the mappings</param>
		/// <returns>C# value converted from the candid value</returns>
		object Map(CandidValue value, CandidConverterOptions options);

		/// <summary>
		/// Maps a C# value to a candid value and type.
		/// Input value will match the `Type` type property.
		/// Returned value should match the `CandidType` type property.
		/// </summary>
		/// <param name="value">C# value to map to a candid value</param>
		/// <param name="options">Options that are being used for the mappings</param>
		/// <returns>Candid value and type converted from the C# value</returns>
		CandidTypedValue Map(object value, CandidConverterOptions options);
	}
}