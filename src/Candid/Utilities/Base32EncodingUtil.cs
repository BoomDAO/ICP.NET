using System;
using System.Linq;

namespace EdjCase.ICP.Candid.Utilities
{
	internal static class Base32EncodingUtil
	{
		public static byte[] ToBytes(ReadOnlySpan<char> input)
		{
			if (input.IsEmpty)
			{
				throw new ArgumentNullException("input");
			}
			bool grouped = input.IndexOf('-') >= 0;
			if (grouped)
			{				
				input = input.Slice(8); // Remove checksum
			}
			else
			{
				input = input.TrimEnd('='); //remove padding characters
			}

			int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
			byte[] returnArray = new byte[byteCount];

			byte curByte = 0, bitsRemaining = 8;
			int arrayIndex = 0;

			foreach (char c in input)
			{
				if (c == '-')
				{
					// Skip dashes
					continue;
				}
				int cValue = CharToValue(c);
				int mask;
				if (bitsRemaining > 5)
				{
					mask = cValue << (bitsRemaining - 5);
					curByte = (byte)(curByte | mask);
					bitsRemaining -= 5;
				}
				else
				{
					mask = cValue >> (5 - bitsRemaining);
					curByte = (byte)(curByte | mask);
					returnArray[arrayIndex++] = curByte;
					curByte = (byte)(cValue << (3 + bitsRemaining));
					bitsRemaining += 3;
				}
			}

			//if we didn't end with a full byte
			if (arrayIndex != byteCount)
			{
				returnArray[arrayIndex] = curByte;
			}

			return returnArray;
		}

		public static string FromBytes(ReadOnlySpan<byte> input, bool groupedWithChecksum = true)
		{
			if (input == null || input.Length == 0)
			{
				throw new ArgumentNullException("input");
			}

			int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
			Span<char> characterArray = stackalloc char[charCount];

			byte nextChar = 0, bitsRemaining = 5;
			int arrayIndex = 0;

			foreach (byte b in input)
			{
				nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
				characterArray[arrayIndex++] = ValueToChar(nextChar);

				if (bitsRemaining < 4)
				{
					nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
					characterArray[arrayIndex++] = ValueToChar(nextChar);
					bitsRemaining += 5;
				}

				bitsRemaining -= 3;
				nextChar = (byte)((b << bitsRemaining) & 31);
			}

			//if we didn't end with a full char
			if (arrayIndex != charCount)
			{
				characterArray[arrayIndex++] = ValueToChar(nextChar);

				// dont pad if grouping 
				if (!groupedWithChecksum)
				{
					while (arrayIndex != charCount) characterArray[arrayIndex++] = '='; //padding
				}
			}

			if (groupedWithChecksum)
			{
				// Add a dash every 5 characters
				int charLength = arrayIndex;
				int dashCount = charLength / 5;
				char[] chars = new char[charLength + dashCount];
				int offset = 0;
				for (int i = 0; i < charLength; i++)
				{
					if (i % 5 == 0 && i != 0)
					{
						chars[i + offset] = '-';
						offset += 1;
					}
					chars[i + offset] = characterArray[i];
				}
				characterArray = chars;
			}

			return new string(characterArray);
		}

		private static int CharToValue(char c)
		{
			int value = (int)c;

			//65-90 == uppercase letters
			if (value < 91 && value > 64)
			{
				return value - 65;
			}
			//50-55 == numbers 2-7
			if (value < 56 && value > 49)
			{
				return value - 24;
			}
			//97-122 == lowercase letters
			if (value < 123 && value > 96)
			{
				return value - 97;
			}

			throw new ArgumentException("Character is not a Base32 character.", "c");
		}

		private static char ValueToChar(byte b)
		{
			if (b < 26)
			{
				return (char)(b + 97);
			}

			if (b < 32)
			{
				return (char)(b + 24);
			}

			throw new ArgumentException("Byte is not a value Base32 value.", "b");
		}

	}
}