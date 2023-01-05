using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class Path : IHashable
	{
		public IReadOnlyList<PathSegment> Segments { get; }
		public Path(IEnumerable<PathSegment> segments)
		{
			this.Segments = segments.ToList();
		}


		public static Path FromSegments(params PathSegment[] segments)
		{
			return new Path(segments);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return this.Segments
				.ToHashable()
				.ComputeHash(hashFunction);
		}
	}

	public class PathSegment : IHashable
	{
		public byte[] Value { get; }

		public PathSegment(byte[] value)
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

		public static PathSegment FromString(string segment)
		{
			if (string.IsNullOrEmpty(segment))
			{
				throw new ArgumentException("Segment string value must have a value", nameof(segment));
			}
			return new PathSegment(Encoding.UTF8.GetBytes(segment));
		}

		public static implicit operator byte[](PathSegment value)
		{
			return value.Value;
		}

		public static implicit operator PathSegment(byte[] value)
		{
			return new PathSegment(value);
		}

		public static implicit operator PathSegment(string value)
		{
			return PathSegment.FromString(value);
		}
	}
}