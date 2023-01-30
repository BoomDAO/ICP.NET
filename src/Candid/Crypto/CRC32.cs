using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace EdjCase.ICP.Candid.Crypto
{
	/// <summary>
	/// Helper class for computing CRC32 hashes/checksums on byte data
	/// Useful for calculating checksums on data
	/// </summary>
	internal static class CRC32
	{
		private const uint Polynomial = 0xEDB88320;

		private static readonly Lazy<uint[]> ChecksumTable = new Lazy<uint[]>(() =>
		{
			// Intialize table values
			return Enumerable.Range(0, 0x100)
				.Select(i =>
				{
					return Enumerable.Range(0, 8)
						.Aggregate((uint)i, (acc, _) =>
						{
							bool isBitSet = (acc & 1) != 0;
							if (isBitSet)
							{
								return Polynomial ^ (acc >> 1);
							}
							return acc >> 1;
						});
				})
				.ToArray();
		}, true);


		/// <summary>
		/// Computes the 32-bit hash on the data bytes provided
		/// </summary>
		/// <param name="data">Byte data</param>
		/// <returns>Hash of the byte data as a byte array of length of 4</returns>
		public static byte[] ComputeHash(ReadOnlySpan<byte> data)
		{
			uint hash32Value = 0xFFFFFFFF;

			for(int i = 0; i < data.Length; i++)
			{
				// Use rightmost byte and xor it to the current byte to get the table index
				uint checksumIndex = (hash32Value & 0xFF) ^ data[i];
				// Use all other bytes besides that byte and xor it with the checksum table value
				uint otherBytes = hash32Value >> 8;
				hash32Value = ChecksumTable.Value[checksumIndex] ^ otherBytes;
			}
			// Invert and convert the 32-bit value into a byte array
			byte[] hash = BitConverter.GetBytes(~hash32Value);
			Array.Reverse(hash);
			return hash;
		}
	}
}
