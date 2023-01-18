using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;

namespace EdjCase.ICP.Candid.Mapping
{
	public interface IObjectMapper
	{
		CandidType CandidType { get; }
		Type Type { get; }
		object Map(CandidValue value, CandidConverterOptions options);
		CandidTypedValue Map(object obj, CandidConverterOptions options);
	}
}