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

		public static Signature Copy(System.Collections.Generic.IReadOnlyList<byte> value)
		{
			return new Signature(value.ToArray());
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}
	}
}