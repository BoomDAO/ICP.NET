using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace EdjCase.ICP.Candid.Encodings
{
	/// <summary>
	/// Utility class to provide methods for LEB128 encoding (https://en.wikipedia.org/wiki/LEB128)
	/// </summary>
	public static class LEB128
	{
		/// <summary>
		/// Takes a byte encoded unsigned LEB128 and converts it to an `UnboundedUInt`
		/// </summary>
		/// <param name="encodedValue">Byte value of an unsigned LEB128</param>
		/// <returns>`UnboundedUInt` of LEB128 value</returns>
		public static UnboundedUInt DecodeUnsigned(ReadOnlySpan<byte> encodedValue)
		{
			BigInteger v = 0;
			for (int i = 0; i < encodedValue.Length; i++)
			{
				byte b = encodedValue[i];
				ulong valueToAdd = (b & 0b0111_1111u) << (7 * i); // Shift over 7 * i bits to get value to add
				v += valueToAdd;
			}
			return UnboundedUInt.FromBigInteger(v);
		}


		/// <summary>
		/// Takes a encoded unsigned LEB128 byte stream and converts it to an `UnboundedUInt`
		/// </summary>
		/// <param name="stream">Byte stream of an unsigned LEB128</param>
		/// <returns>`UnboundedUInt` of LEB128 value</returns>
		public static UnboundedUInt DecodeUnsigned(Stream stream)
		{
			BigInteger v = Decode(stream, isUnsigned: true);
			return UnboundedUInt.FromBigInteger(v);
		}

		/// <summary>
		/// Takes a encoded signed LEB128 byte stream and converts it to an `UnboundedInt`
		/// </summary>
		/// <param name="stream">Byte stream of a signed LEB128</param>
		/// <returns>`UnboundedInt` of LEB128 value</returns>
		public static UnboundedInt DecodeSigned(Stream stream)
		{
			BigInteger v = Decode(stream, isUnsigned: false);
			return UnboundedInt.FromBigInteger(v);
		}

		/// <summary>
		/// Takes an `UnboundedUInt` and converts it into an encoded unsigned LEB128 byte array
		/// </summary>
		/// <param name="value">Value to convert to LEB128 bytes</param>
		/// <returns>LEB128 bytes of value</returns>
		public static byte[] EncodeUnsigned(UnboundedUInt value)
		{
			ArrayBufferWriter<byte> destination = new();
			EncodeUnsigned(value.ToBigInteger(), destination);
			return destination.WrittenMemory.ToArray();
		}

		/// <summary>
		/// Takes an `UnboundedUInt` and converts it into an encoded unsigned LEB128 byte array
		/// </summary>
		/// <param name="value">Value to convert to LEB128 bytes</param>
		/// <param name="destination">Buffer writer to write bytes to</param>
		/// <returns>LEB128 bytes of value</returns>
		public static void EncodeUnsigned(UnboundedUInt value, IBufferWriter<byte> destination)
		{
			EncodeUnsigned(value.ToBigInteger(), destination);
		}

		/// <summary>
		/// Takes an `UnboundedInt` and converts it into an encoded signed LEB128 byte array
		/// </summary>
		/// <param name="value">Value to convert to LEB128 bytes</param>
		/// <returns>LEB128 bytes of value</returns>
		public static byte[] EncodeSigned(UnboundedInt value)
		{
			ArrayBufferWriter<byte> destination = new();
			EncodeSigned(value.ToBigInteger(), destination);
			return destination.WrittenMemory.ToArray();
		}

		/// <summary>
		/// Takes an `UnboundedInt` and converts it into an encoded signed LEB128 byte array
		/// </summary>
		/// <param name="value">Value to convert to LEB128 bytes</param>
		/// <param name="destination">Buffer writer to write bytes to</param>
		/// <returns>LEB128 bytes of value</returns>
		public static void EncodeSigned(UnboundedInt value, IBufferWriter<byte> destination)
		{
			EncodeSigned(value.ToBigInteger(), destination);
		}


		private static BigInteger Decode(Stream stream, bool isUnsigned)
		{
			IEnumerable<bool> bits = GetValueBits(stream, isUnsigned);
			byte[] pooledMemory = ArrayPool<byte>.Shared.Rent(10);
			try
			{
				int bitIndex = 0;
				int byteIndex = 0;
				byte byteValue = 0;
				foreach (bool bit in bits)
				{
					if (bit)
					{
						// Set bit on byte
						byteValue |= (byte)(1 << bitIndex);
					}
					if (bitIndex == 7)
					{
						SetPooledMemoryByte(ref pooledMemory, ref byteIndex, byteValue);
						byteValue = 0; // Reset byte to empty
						bitIndex = 0; // Reset index back to beginning of byte
					}
					else
					{
						bitIndex++;
					}
				}
				if (bitIndex != 0)
				{
					// Expect 8 byte chunks
					throw new EndOfStreamException();
				}
				return new BigInteger(pooledMemory.AsSpan()[..byteIndex], isUnsigned, isBigEndian: false);
			}
			finally
			{
				// Return shared memory
				ArrayPool<byte>.Shared.Return(pooledMemory);
			}
		}
		private static IEnumerable<bool> GetValueBits(Stream stream, bool isUnsigned)
		{
			int i = 0;
			while (true)
			{
				int byteOrEnd = stream.ReadByte();
				if (byteOrEnd == -1)
				{
					throw new EndOfStreamException();
				}

				bool more = (byteOrEnd & 0b1000_0000) == 0b1000_0000; // first bit is a flag if there is more bytes

				// See which of the 7 bits are set
				yield return GetBit(0b0000_0001);
				yield return GetBit(0b0000_0010);
				yield return GetBit(0b0000_0100);
				yield return GetBit(0b0000_1000);
				yield return GetBit(0b0001_0000);
				yield return GetBit(0b0010_0000);

				bool lastBitSet = GetBit(0b0100_0000);
				yield return lastBitSet;
				if (!more)
				{
					bool paddingValue;
					if (isUnsigned)
					{
						paddingValue = false;
					}
					else
					{
						paddingValue = lastBitSet; // 1 if signed and last bit is 1, otherwise 0
					}
					while (i % 8 != 0)
					{
						// Round out the 
						yield return paddingValue;
						i++;
					}
					break;
				}
				bool GetBit(byte bitMask)
				{
					i++;
					return (byteOrEnd & bitMask) == bitMask;
				}
			}
		}


		private static void EncodeUnsigned(BigInteger value, IBufferWriter<byte> destination)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(value), "Value must be 0 or greater");
			}
			if (value == 0)
			{
				destination.WriteOne<byte>(0);
				return;
			}

			// Unsigned LEB128 - https://en.wikipedia.org/wiki/LEB128#Unsigned_LEB128
			//       10011000011101100101  In raw binary
			//      010011000011101100101  Padded to a multiple of 7 bits
			//  0100110  0001110  1100101  Split into 7-bit groups
			// 00100110 10001110 11100101  Add high 1 bits on all but last (most significant) group to form bytes

			byte[] pooledMemory = ArrayPool<byte>.Shared.Rent(10); // Use shared memory to avoid allocations
			try
			{
				Span<byte> buffer = stackalloc byte[1];
				int byteIndex = 0;
				while (value != 0)
				{
					BigInteger nextByte = (value & 0b0111_1111); // Get the next 7 bits

					// Optimization to use the buffer to get byte value
					nextByte.TryWriteBytes(buffer, out _, isUnsigned: true, isBigEndian: false);

					byte byteValue = buffer[0]; // Get next byte from the buffer


					value = value >> 7; // Chop off last 7 bits
					if (value != 0)
					{
						// Have most left of byte be 1 if there is another byte
						byteValue |= 0b10000000;
					}

					SetPooledMemoryByte(ref pooledMemory, ref  byteIndex, byteValue);
				}
				destination.Write(pooledMemory.AsSpan()[..byteIndex]); // Write to the 
			}
			finally
			{
				// Return shared memory
				ArrayPool<byte>.Shared.Return(pooledMemory);
			}

		}

		private static void EncodeSigned(BigInteger value, IBufferWriter<byte> destination)
		{
			if (value == 0)
			{
				destination.WriteOne<byte>(0);
				return;
			}

			// Signed LEB128 - https://en.wikipedia.org/wiki/LEB128#Signed_LEB128
			//         11110001001000000  Binary encoding of 123456
			//     00001_11100010_01000000  As a 21-bit number (multiple of 7)
			//     11110_00011101_10111111  Negating all bits (one's complement)
			//     11110_00011101_11000000  Adding one (two's complement) (Binary encoding of signed -123456)
			// 1111000  0111011  1000000  Split into 7-bit groups
			//01111000 10111011 11000000  Add high 1 bits on all but last (most significant) group to form bytes

			int byteIndex = 0;
			byte[] pooledMemory = ArrayPool<byte>.Shared.Rent(10); // Use shared memory to avoid allocations
			try
			{
				bool more = true;
				Span<byte> buffer = stackalloc byte[1];
				while (more)
				{
					BigInteger nextByte = (value & 0b0111_1111); // Get the next 7 bits

					// Optimization to use the buffer to get byte value
					nextByte.TryWriteBytes(buffer, out _, isUnsigned: true, isBigEndian: false);

					byte byteValue = buffer[0]; // Get next byte from the buffer

					value >>= 7; // Shift over 7 bits to setup the next byte
					bool mostSignficantBitIsSet = (byteValue & 0b0100_0000) != 0;
					if (value == 0) // no more bits => end
					{
						if (mostSignficantBitIsSet)
						{
							// If last bit is a 1, add another 0 value byte
							AddByteWithMoreFlag(byteValue);
							AddByte(0b0000_0000);
							break;
						}
						AddByte(byteValue);
						break;
					}
					if (value == -1) // -1 == only bit remaining is a sign bit (with backfilled 1's from a right shift) => end
					{
						// -1 is equivalent to all 1's in the binary sequence/255 if unsigned
						// AND since if the number is negative, 1 is used to fill vacated bit positions
						// meaning the remaining value is just the sign bit

						// IF the most signficant bit is not set, set the final byte to 0b0111_1111
						// otherwise end

						if (!mostSignficantBitIsSet)
						{
							// If last bit is a 1, add another 111_1111 value byte
							AddByteWithMoreFlag(byteValue);
							AddByte(0b0111_1111);
							break;
						}
						AddByte(byteValue);
						break;
					}
					AddByteWithMoreFlag(byteValue);
				}
				destination.Write(pooledMemory.AsSpan()[..byteIndex]); // Write bytes to destination
			}
			finally
			{
				// Return shared memory
				ArrayPool<byte>.Shared.Return(pooledMemory);
			}

			void AddByteWithMoreFlag(byte byteValue)
			{
				AddByte((byte)(byteValue | 0b1000_0000));
			}

			void AddByte(byte byteValue)
			{
				SetPooledMemoryByte(ref pooledMemory, ref byteIndex, byteValue);
			}
		}

		private static void SetPooledMemoryByte(ref byte[] pooledMemory, ref int byteIndex, byte byteValue)
		{
			if (byteIndex >= pooledMemory.Length)
			{
				// If pooled memory is too small, create bigger array
				var newPooledMemory = ArrayPool<byte>.Shared.Rent(pooledMemory.Length + 10);
				pooledMemory.AsSpan().CopyTo(newPooledMemory);
				ArrayPool<byte>.Shared.Return(pooledMemory);
				pooledMemory = newPooledMemory;
			}
			pooledMemory[byteIndex++] = byteValue;
		}
	}
}
