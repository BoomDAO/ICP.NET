using System;

namespace EdjCase.ICP.Candid.Models
{
	public class OptionalValue<T> : IEquatable<OptionalValue<T>>
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

		public bool TryGetValue(out T x)
		{
			if (this.HasValue)
			{
				x = this.value!;
				return true;
			}
			else
			{
				x = default;
				return false;
			}
		}

		public OptionalValue<T2> Cast<T2>()
		{
			return new OptionalValue<T2>(this.HasValue, (T2?)(object?)this.value);
		}

		public override bool Equals(object obj)
		{
			if (obj is OptionalValue<T> o)
			{
				return this.Equals(o);
			}
			return false;
		}

		public bool Equals(OptionalValue<T> other)
		{
			if (this.HasValue != other.HasValue)
			{
				return false;
			}
			if (!this.HasValue)
			{
				return true;
			}
			return this.GetValueOrThrow()!.Equals(this.GetValueOrThrow());
		}

		public override int GetHashCode()
		{
			if (!this.HasValue)
			{
				return 0;
			}
			return this.GetValueOrThrow()!.GetHashCode();
		}

		public static bool operator ==(OptionalValue<T> x, OptionalValue<T> y)
		{
			if (object.ReferenceEquals(x, null))
			{
				return object.ReferenceEquals(y, null);
			}
			return x.Equals(y);
		}

		public static bool operator !=(OptionalValue<T> x, OptionalValue<T> y)
		{
			return !(x == y);
		}

		public override string ToString()
		{
			return this.HasValue ? this.GetValueOrThrow()!.ToString() : "";
		}

		public static OptionalValue<T> NoValue()
		{
			return new OptionalValue<T>(false, default);
		}

		public static OptionalValue<T> WithValue(T value)
		{
			return new OptionalValue<T>(true, value);
		}
	}
}
