using EdjCase.ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid
{
	internal class BinarySequence
	{
		// Least signifcant bit (index 0) => Most signifcant bit (index n - 1)
		private readonly bool[] bits;

		public bool MostSignificantBit => this.bits[this.bits.Length - 1];

		/// <param name="bits">Least signifcant to most ordered bits</param>
		public BinarySequence(bool[] bits)
		{
			this.bits = bits;
		}

		public BinarySequence ToTwosCompliment()
		{
			// If value most significant bit is `1`, the 2's compliment needs to be 1 bit larger to hold sign bit
			if (this.MostSignificantBit)
			{
				bool[] newBits = new bool[this.bits.Length + 1];
				this.bits.CopyTo(newBits, 0);
				return new BinarySequence(newBits);
			}
			bool[] bits = this.ToTwosComplimentInternal().ToArray();
			return new BinarySequence(bits);
		}

		public BinarySequence ToReverseTwosCompliment()
		{
			if (this.bits.Last())
			{
				throw new InvalidOperationException("Cannot reverse two's compliment on a negative number");
			}
			bool[] bits = this.ToTwosComplimentInternal().ToArray();
			return new BinarySequence(bits);
		}

		public byte[] ToByteArray(bool bigEndian = false)
		{
			IEnumerable<byte> bytes = this.bits
				.Chunk(8)
				.Select(BitsToByte);
			// Reverse if need big endian
			if (bigEndian)
			{
				bytes = bytes.Reverse();
			}

			return bytes.ToArray();

			byte BitsToByte(bool[] bits)
			{
				if (bits.Length > 8)
				{
					throw new ArgumentException("Bit length must be less than or equal to 8");
				}
				// Bits are in least significant first order
				int value = 0;
				for (int i = 0; i < bits.Length; i++)
				{
					bool b = bits[i];
					if (b)
					{
						value |= 1 << i;
					}
				}
				return (byte)value;
			}
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();
			foreach (bool bit in this.bits.Reverse()) // Reverse to show LSB on right (normal display)
			{
				stringBuilder.Append(bit ? "1" : "0");
			}
			return stringBuilder.ToString();
		}

		public static BinarySequence FromBytes(byte[] bytes, bool isBigEndian)
		{
			IEnumerable<byte> byteSeq = bytes;
			if (isBigEndian)
			{
				byteSeq = byteSeq.Reverse();
			}
			bool[] bits = byteSeq
				.SelectMany(ByteToBits)
				.ToArray();
			return new BinarySequence(bits);

			IEnumerable<bool> ByteToBits(byte b)
			{
				// Least significant first
				yield return (b & 0b00000001) == 0b00000001;
				yield return (b & 0b00000010) == 0b00000010;
				yield return (b & 0b00000100) == 0b00000100;
				yield return (b & 0b00001000) == 0b00001000;
				yield return (b & 0b00010000) == 0b00010000;
				yield return (b & 0b00100000) == 0b00100000;
				yield return (b & 0b01000000) == 0b01000000;
				yield return (b & 0b10000000) == 0b10000000;
			}
		}

		private IEnumerable<bool> ToTwosComplimentInternal()
		{
			// Invert all numbers left of the right-most `1` to get 2's compliment

			bool flipBits = false;
			foreach (bool bit in this.bits.Reverse())
			{
				yield return flipBits ? !bit : bit;
				// If bit is `1`, all bits to left are flipped
				if (bit)
				{
					flipBits = true;
				}
			}
		}
	}
}
