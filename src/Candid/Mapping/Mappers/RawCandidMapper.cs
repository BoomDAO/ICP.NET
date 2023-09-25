using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class RawCandidMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; set; }
		public RawCandidMapper(CandidType candidType)
		{
			this.CandidType = candidType;
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			return value;
		}

		public CandidValue Map(object obj, CandidConverter converter)
		{
			return (CandidValue)obj;
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}
