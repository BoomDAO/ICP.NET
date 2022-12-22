using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class OptionalValue<T>
	{
		public bool HasValue { get; }

		private T? value { get; }

		public OptionalValue(bool hasValue, T? value)
		{
			this.HasValue = hasValue;
			this.value = value;
		}

		public T GetValueOrThrow()
		{
			if (!this.HasValue)
			{
				throw new InvalidOperationException("OptionalValue is not set, cannot get value");
			}
			return this.value!;
		}

		public T? GetValueOrDefault()
		{
			if (!this.HasValue)
			{
				return default;
			}
			return this.value!;
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
