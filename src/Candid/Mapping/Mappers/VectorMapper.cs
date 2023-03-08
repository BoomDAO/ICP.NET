using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class VectorMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public Type InnerType { get; }
		public Func<object, CandidConverter, IEnumerable<object>> ToEnumerableFunc { get; }
		public Func<IEnumerable<object>, CandidConverter, object> FromEnumerableFunc { get; }

		public VectorMapper(
			CandidVectorType candidType,
			Type type,
			Type innerType,
			Func<object, CandidConverter, IEnumerable<object>> toEnumerableFunc,
			Func<IEnumerable<object>, CandidConverter, object> fromEnumerableFunc)
		{
			this.CandidType = candidType;
			this.Type = type;
			this.InnerType = innerType;
			this.ToEnumerableFunc = toEnumerableFunc;
			this.FromEnumerableFunc = fromEnumerableFunc;
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidVector vector = value.AsVector();
			IEnumerable<object> objEnumerable = vector.Values
				.Select(v => converter.ToObject(this.InnerType, v));

			return this.FromEnumerableFunc(objEnumerable, converter);
		}

		public CandidValue Map(object obj, CandidConverter converter)
		{
			CandidValue[] values = this.ToEnumerableFunc(obj, converter)
				.Select(converter.FromObject)
				.ToArray();
			return new CandidVector(values);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}