using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class StatePath : IHashable
	{
		public IReadOnlyList<StatePathSegment> Segments { get; }
		public StatePath(IEnumerable<StatePathSegment> segments)
		{
			this.Segments = segments.ToList();
		}


		public static StatePath FromSegments(params StatePathSegment[] segments)
		{
			return new StatePath(segments);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return this.Segments
				.ToHashable()
				.ComputeHash(hashFunction);
		}
	}

	public class StatePathSegment : IHashable
	{
		public byte[] Value { get; }

		public StatePathSegment(byte[] value)
		{
			if (value == null || value.Length < 1)
			{
				throw new ArgumentException("Path segment bytes cannot be empty", nameof(value));
			}
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public static StatePathSegment FromString(string segment)
		{
			if (string.IsNullOrEmpty(segment))
			{
				throw new ArgumentException("Segment string value must have a value", nameof(segment));
			}
			return new StatePathSegment(Encoding.UTF8.GetBytes(segment));
		}

		public static implicit operator byte[](StatePathSegment value)
		{
			return value.Value;
		}

		public static implicit operator StatePathSegment(byte[] value)
		{
			return new StatePathSegment(value);
		}

		public static implicit operator StatePathSegment(string value)
		{
			return StatePathSegment.FromString(value);
		}
	}
}