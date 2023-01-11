using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System;
using System.Linq;

namespace EdjCase.ICP.Agent
{
	public class Signature : IHashable
	{
		public byte[] Value { get; }

		public Signature(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}
		public Signature(System.Collections.Generic.IReadOnlyList<byte> value)
		{
			this.Value = value.ToArray() ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}
	}
}