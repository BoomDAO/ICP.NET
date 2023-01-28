using System;
using System.Security.Cryptography;

namespace EdjCase.ICP.Candid.Crypto
{

	/// <summary>
	/// Interface to implement different hash function algorithms against
	/// </summary>
	public interface IHashFunction
	{
		/// <summary>
		/// Computes the hash of the byte array based on the algorithm implemented
		/// </summary>
		/// <param name="value">Byte array to get the hash of</param>
		/// <returns>Hash in the form of a byte array</returns>
		public byte[] ComputeHash(byte[] value);
	}
}