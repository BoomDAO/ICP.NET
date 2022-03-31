using Dfinity.Common.Crypto;
using Dfinity.Common.Models;
using System;

namespace Dfinity.Agent
{
	public class Signature : IHashable
	{
		public byte[] Value { get; }

		public Signature(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}
	}
}