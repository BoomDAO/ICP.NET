using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class VectorMapper : IObjectMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public Type InnerType { get; }
		public Func<object, CandidConverterOptions, IEnumerable<object>> ToEnumerableFunc { get; }
		public Func<IEnumerable<object>, CandidConverterOptions, object> FromEnumerableFunc { get; }
		public IObjectMapper? OverrideInnerMapper { get; }

		public VectorMapper(
			CandidVectorType candidType,
			Type type,
			Type innerType,
			Func<object, CandidConverterOptions, IEnumerable<object>> toEnumerableFunc,
			Func<IEnumerable<object>, CandidConverterOptions, object> fromEnumerableFunc,
			IObjectMapper? overrideInnerMapper)
		{
			this.CandidType = candidType;
			this.Type = type;
			this.InnerType = innerType;
			this.ToEnumerableFunc = toEnumerableFunc;
			this.FromEnumerableFunc = fromEnumerableFunc;
			this.OverrideInnerMapper = overrideInnerMapper;
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			CandidVector vector = value.AsVector();
			IObjectMapper innerMapper = this.OverrideInnerMapper ?? options.ResolveMapper(this.InnerType);
			IEnumerable<object> objEnumerable = vector.Values
				.Select(v => innerMapper.Map(v, options));

			return this.FromEnumerableFunc(objEnumerable, options);
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			IObjectMapper innerMapper = this.OverrideInnerMapper ?? options.ResolveMapper(this.InnerType);
			CandidValue[] values = this.ToEnumerableFunc(obj, options)
				.Select(v => innerMapper.Map(v, options).Value)
				.ToArray();
			return new CandidTypedValue(new CandidVector(values), this.CandidType);
		}
	}
}