using System;
using System.Text.RegularExpressions;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A helper model to store and validate a valid candid id value
	/// </summary>
	public class CandidId : IEquatable<CandidId>, IEquatable<string>, IComparable<CandidId>
	{
		private readonly static Regex idRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
		
		/// <summary>
		/// The string value of the id
		/// </summary>
		public string Value { get; }

		private CandidId(string value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		/// <summary>
		/// Helper method to create a candid id from a string value. Will validate if the string is a valid id
		/// </summary>
		/// <param name="value">The string value to use as the id</param>
		/// <returns>A candid id value</returns>
		/// <exception cref="ArgumentException">Throws if the string is not a valid candid id</exception>
		public static CandidId Create(string value)
		{
			if (!idRegex.IsMatch(value))
			{
				throw new ArgumentException($"Invalid id '{value}'. Ids can only have letters, numbers and underscores and cannot start with a number");
			}
			return new CandidId(value);
		}

		/// <inheritdoc />
		public override bool Equals(object? other)
		{
			if (other is CandidId id)
			{
				return this.Equals(id);
			}
			if (other is string s)
			{
				return this.Equals(s);
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <inheritdoc />
		public bool Equals(CandidId? other)
		{
			if (ReferenceEquals(other, null))
			{
				return false;
			}
			return this.Value == other.Value;
		}

		/// <inheritdoc />
		public bool Equals(string? other)
		{
			return this.Value == other;
		}

		/// <inheritdoc />
		public static bool operator ==(CandidId? v1, CandidId? v2)
		{
			if (ReferenceEquals(v1, null))
			{
				return ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		/// <inheritdoc />
		public static bool operator !=(CandidId? v1, CandidId? v2)
		{
			return !(v1 == v2);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Value;
		}

		/// <inheritdoc />
		public int CompareTo(CandidId? other)
		{
			return this.Value.CompareTo(other?.Value);
		}
	}
}
