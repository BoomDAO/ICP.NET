using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Reflection;

namespace EdjCase.ICP.Candid.Mapping.Mappers
{
	internal class OptMapper : ICandidValueMapper
	{
		public CandidType CandidType { get; }
		public Type Type { get; }
		public CandidType InnerCandidType { get; }
		public Type InnerType { get; }

		private PropertyInfo hasValueProp;
		private MethodInfo valueGetFunc;

		public OptMapper(
			Type type,
			CandidType innerCandidType,
			Type innerType
		)
		{
			this.InnerCandidType = innerCandidType ?? throw new ArgumentNullException(nameof(innerCandidType));
			this.InnerType = innerType ?? throw new ArgumentNullException(nameof(innerType));
			this.Type = type;
			this.CandidType = new CandidOptionalType(this.InnerCandidType);
			this.hasValueProp = this.Type.GetProperty(nameof(OptionalValue<object>.HasValue));
			this.valueGetFunc = this.Type.GetMethod(nameof(OptionalValue<object>.GetValueOrThrow));
		}

		public object Map(CandidValue value, CandidConverter converter)
		{
			CandidOptional opt = value.AsOptional();

			if (!opt.Value.IsNull())
			{
				object innerValue = converter.ToObject(this.InnerType, opt.Value);

				return Activator.CreateInstance(
					this.Type,
					BindingFlags.Public | BindingFlags.Instance,
					null,
					new object?[] { innerValue },
					null
				);
			}
			else
			{
				// Empty constructor
				return Activator.CreateInstance(
					this.Type,
					BindingFlags.Public | BindingFlags.Instance,
					null,
					new object?[] { },
					null
				);
			}
		}

		public CandidValue Map(object obj, CandidConverter converter)
		{
			bool hasValue = (bool)this.hasValueProp.GetValue(obj);
			if (!hasValue)
			{
				return new CandidOptional();
			}
			object innerValue = this.valueGetFunc.Invoke(obj, new object[0]);
			CandidValue innerTypedValue = converter.FromObject(innerValue);
			return new CandidOptional(innerTypedValue);
		}

		public CandidType? GetMappedCandidType(Type type)
		{
			return this.CandidType;
		}
	}
}