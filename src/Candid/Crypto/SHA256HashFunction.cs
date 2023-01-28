using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EdjCase.ICP.Candid.Crypto
{
	/// <summary>
	/// A SHA256 implementation of the `IHashFunction`
	/// </summary>
	public class SHA256HashFunction : IHashFunction
	{
		private SHA256 inner { get; }

		internal SHA256HashFunction(SHA256 algorithm)
		{
			this.inner = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
		}

		/// <inheritdoc />
		public byte[] ComputeHash(byte[] value)
		{
			return this.inner.ComputeHash(value);
		}

		/// <summary>
		/// Helper method to create the hash function object
		/// </summary>
		/// <returns>Hash function object</returns>
		public static SHA256HashFunction Create()
		{
			return new SHA256HashFunction(SHA256.Create());
		}
	}
}
