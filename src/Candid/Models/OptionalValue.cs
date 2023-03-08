using System;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A helper class to represent a candid opt value. This is used instead of just a null value due to 
	/// ambiguity in certain scenarios
	/// </summary>
	/// <typeparam name="T">The inner type of the opt</typeparam>
	public class OptionalValue<T> : IEquatable<OptionalValue<T>>
	{
		/// <summary>
		/// Is true if there is a value, false if null
		/// </summary>
		public bool HasValue { get; private set; }

		/// <summary>
		/// The value, will be set if `HasValue`, otherwise will be default value
		/// </summary>
		public T? ValueOrDefault { get; private set; }

		/// <summary>
		/// Constructor to create an optional value with no value
		/// </summary>
		public OptionalValue()
		{
			this.HasValue = false;
			this.ValueOrDefault = default;
		}

		/// <summary>
		/// Constructor to create an optional value with a value
		/// </summary>
		/// <param name="value"></param>
		public OptionalValue(T value)
		{
			this.HasValue = true;
			this.ValueOrDefault = value;
		}

		/// <summary>
		/// Gets the value if exists, otherwise throws an exception
		/// </summary>
		/// <returns>The inner value of the opt</returns>
		/// <exception cref="InvalidOperationException">Throws if there is no value</exception>
		public T GetValueOrThrow()
		{
			if (!this.HasValue)
			{
				throw new InvalidOperationException("OptionalValue is not set, cannot get value");
			}
			return this.ValueOrDefault!;
		}

		/// <summary>
		/// Gets the value if exists, otherwise the default value
		/// </summary>
		/// <returns>The value if exists, otherwise the default value</returns>
		public T? GetValueOrDefault()
		{
			if (!this.HasValue)
			{
				return default;
			}
			return this.ValueOrDefault!;
		}

		/// <summary>
		/// Gets the type of the optional value, even if there is no value
		/// </summary>
		/// <returns>The type of the value</returns>
		public Type GetValueType()
		{
			return typeof(T);
		}

		/// <summary>
		/// Sets the value and sets `HasValue` to true
		/// </summary>
		/// <param name="value">The value to set</param>
		public void SetValue(T value)
		{
			this.HasValue = true;
			this.ValueOrDefault = value;
		}

		/// <summary>
		/// Removes the current value and sets `HasValue` to false
		/// </summary>
		public void UnsetValue()
		{
			this.HasValue = false;
			this.ValueOrDefault = default;
		}

		/// <summary>
		/// Tries to get the value from the opt. If a value exists it will return true and the out value will be set,
		/// otherwise it will return false and the out value will be the default value
		/// </summary>
		/// <param name="value">The value, if exists</param>
		/// <returns>True if there is a value, otherwise false</returns>
		public bool TryGetValue(out T? value)
		{
			if (this.HasValue)
			{
				value = this.ValueOrDefault!;
				return true;
			}
			else
			{
				value = default;
				return false;
			}
		}

		/// <summary>
		/// Casts the inner type of the optional value to the new type
		/// </summary>
		/// <typeparam name="T2">The type to cast to for the inner type</typeparam>
		/// <returns>An optional value with the new type</returns>
		public OptionalValue<T2> Cast<T2>()
		{
			if (!this.HasValue)
			{
				return new OptionalValue<T2>();
			}
			return new OptionalValue<T2>((T2)(object)this.ValueOrDefault!);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (obj is OptionalValue<T> o)
			{
				return this.Equals(o);
			}
			return false;
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public override int GetHashCode()
		{
			if (!this.HasValue)
			{
				return 0;
			}
			return this.GetValueOrThrow()!.GetHashCode();
		}

		/// <inheritdoc />
		public static bool operator ==(OptionalValue<T> x, OptionalValue<T> y)
		{
			if (ReferenceEquals(x, null))
			{
				return ReferenceEquals(y, null);
			}
			return x.Equals(y);
		}

		/// <inheritdoc />
		public static bool operator !=(OptionalValue<T> x, OptionalValue<T> y)
		{
			return !(x == y);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.HasValue ? this.GetValueOrThrow()!.ToString() : "";
		}

		/// <summary>
		/// A helper function to create a optional value with no value
		/// </summary>
		/// <returns>An empty optional value</returns>
		public static OptionalValue<T> NoValue()
		{
			return new OptionalValue<T>();
		}

		/// <summary>
		/// A helper function to create a optional value with a value
		/// </summary>
		/// <returns>An optional value with a value</returns>
		public static OptionalValue<T> WithValue(T value)
		{
			return new OptionalValue<T>(value);
		}
	}
}
