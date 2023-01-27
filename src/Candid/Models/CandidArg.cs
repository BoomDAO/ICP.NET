using System;
using System.Collections.Generic;
using System.Linq;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Parsers;

namespace EdjCase.ICP.Candid.Models
{
	public class CandidArg : IHashable, IEquatable<CandidArg>
	{
		public List<CandidTypedValue> Values { get; }

		public CandidArg(List<CandidTypedValue> values)
		{
			this.Values = values;
		}

		/// <inheritdoc />
		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Encode());
		}

		public byte[] Encode()
		{
			return CandidArgBuilder.FromArgs(this.Values).Encode();
		}

		public static CandidArg FromBytes(byte[] value)
		{
			return CandidByteParser.Parse(value);
		}

		public static CandidArg FromCandid(List<CandidTypedValue> args)
		{
			return new CandidArg(args);
		}

		public static CandidArg FromCandid(params CandidTypedValue[] args)
		{
			return new CandidArg(args.ToList());
		}

		public static CandidArg Empty()
		{
			return new CandidArg(new List<CandidTypedValue>());
		}

		/// <inheritdoc />
		public override string ToString()
		{
			IEnumerable<string> args = this.Values.Select(v => v.Value.ToString()!);
			return $"({string.Join(",", args)})";
		}

		/// <inheritdoc />
		public bool Equals(CandidArg? other)
		{
			if (object.ReferenceEquals(other, null))
			{
				return false;
			}
			return this.Values.SequenceEqual(other.Values);
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return this.Equals(obj as CandidArg);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Values.Select(v => v.GetHashCode()));
		}

		/// <inheritdoc />
		public static bool operator ==(CandidArg? v1, CandidArg? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return v1.Equals(v2);
		}

		/// <inheritdoc />
		public static bool operator !=(CandidArg? v1, CandidArg? v2)
		{
			if (object.ReferenceEquals(v1, null))
			{
				return object.ReferenceEquals(v2, null);
			}
			return !v1.Equals(v2);
		}
	}
}