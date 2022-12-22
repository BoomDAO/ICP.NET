using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EdjCase.ICP.Candid.Mapping
{
	public interface IObjectMapper
	{
		bool CanMap(Type type);
		object Map(CandidValue value, CandidConverterOptions options);
		CandidTypedValue Map(object obj, CandidConverterOptions options);
	}
}