using ICP.Candid.Crypto;
using System;
using System.Text;

namespace ICP.Candid.Models
{
    public class PathSegment : IHashable
    {
		public byte[] Value { get; }

        public PathSegment(byte[] value)
        {
            if(value == null || value.Length < 1)
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
    }
}