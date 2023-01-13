using System;
using System.Security.Cryptography;

namespace EdjCase.ICP.Candid.Crypto
{
	public interface IHashFunction
	{
		public byte[] ComputeHash(byte[] value);
	}
}