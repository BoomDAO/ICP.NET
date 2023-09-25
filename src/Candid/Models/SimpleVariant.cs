using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A simple representation of a variant
	/// </summary>
	public class SimpleVariant
	{
		/// <summary>
		/// The variant option name
		/// </summary>
		public string Tag { get; }

		/// <summary>
		/// The variant option value
		/// </summary>
		public object? Value { get; }

		/// <param name="tag">The variant option name</param>
		/// <param name="value">The variant option value</param>
		public SimpleVariant(string tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Helper function to cast and get value as a type, if value is unset, will return default(T)
		/// </summary>
		/// <typeparam name="T">Type to cast value to</typeparam>
		/// <returns>Option value</returns>
		public T? GetValueOrDefault<T>()
		{
			return this.Value == null ? default : (T)this.Value;
		}
	}
}
