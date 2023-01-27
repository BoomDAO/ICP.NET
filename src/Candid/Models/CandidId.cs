using System;
using System.Text.RegularExpressions;

namespace EdjCase.ICP.Candid.Models
{
	public class CandidId : IEquatable<CandidId>, IEquatable<string>, IComparable<CandidId>
	{
		private readonly static Regex idRegex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$", RegexOptions.Compiled);
		public string Value { get; }

		private CandidId(string value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public static CandidId Parse(string value)
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
			if (object.ReferenceEquals(other, null))
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
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
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
			return this.Value.ToString();
		}

		/// <inheritdoc />
		public int CompareTo(CandidId? other)
		{
			return this.Value.CompareTo(other?.Value);
		}
	}
}
