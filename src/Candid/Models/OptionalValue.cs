using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class OptionalValue<T>
	{
		public bool HasValue { get; }

		public T? Value { get; }

		public OptionalValue(bool hasValue, T? value)
		{
			this.HasValue = hasValue;
			this.Value = value;
		}

		public static OptionalValue<T> Null()
		{
			return new OptionalValue<T>(false, default);
		}

		public static OptionalValue<T> WithValue(T value)
		{
			return new OptionalValue<T>(true, value);
		}
	}
}
