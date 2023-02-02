using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Reflection;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class OptMapper : IObjectMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public CandidType InnerCandidType { get; }
		public Type InnerType { get; }
		public IObjectMapper? OverrideInnerMapper { get; }

		private PropertyInfo hasValueProp;
		private MethodInfo valueGetFunc;

		public OptMapper(CandidType innerCandidType, Type innerType, IObjectMapper? overrideInnerMapper)
		{
			this.InnerCandidType = innerCandidType ?? throw new ArgumentNullException(nameof(innerCandidType));
			this.InnerType = innerType ?? throw new ArgumentNullException(nameof(innerType));
			this.Type = typeof(OptionalValue<>).MakeGenericType(innerType);
			this.CandidType = new CandidOptionalType(this.InnerCandidType);
			this.OverrideInnerMapper = overrideInnerMapper;
			this.hasValueProp = this.Type.GetProperty(nameof(OptionalValue<object>.HasValue));
			this.valueGetFunc = this.Type.GetMethod(nameof(OptionalValue<object>.GetValueOrThrow));
		}

		public object Map(CandidValue value, CandidConverterOptions options)
		{
			CandidOptional opt = value.AsOptional();

			object? innerValue;
			bool hasValue;
			if (!opt.Value.IsNull())
			{
				innerValue = (this.OverrideInnerMapper ?? options.ResolveMapper(this.InnerType)).Map(opt.Value, options);
				hasValue = true;
			}
			else
			{
				innerValue = null;
				hasValue = false;
			}
			return Activator.CreateInstance(this.Type, BindingFlags.NonPublic | BindingFlags.Instance, null, new object?[] { hasValue, innerValue }, null);
		}

		public CandidTypedValue Map(object obj, CandidConverterOptions options)
		{
			bool hasValue = (bool)this.hasValueProp.GetValue(obj);
			CandidValue? v;
			if (!hasValue)
			{
				v = null;
			}
			else
			{
				object innerValue = this.valueGetFunc.Invoke(obj, new object[0]);
				v = (this.OverrideInnerMapper ?? options.ResolveMapper(this.InnerType)).Map(innerValue, options).Value;
			}
			return new CandidTypedValue(new CandidOptional(v), this.CandidType);
		}
	}
}