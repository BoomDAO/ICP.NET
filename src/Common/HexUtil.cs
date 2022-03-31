using System;

namespace Dfinity.Common
{
	public class HexUtil
	{
		public static byte[] HexToBytes(ReadOnlySpan<char> hex)
		{
			if (hex.Length % 2 != 0)
			{
				throw new ArgumentException($"The binary key cannot have an odd number of digits: {hex.ToString()}");
			}
			int byteLength = hex.Length / 2;
			byte[] bytes = new byte[byteLength];
			for (int index = 0; index < bytes.Length; index++)
			{
				int hexIndex = index * 2;
				int hexValue1 = GetHexVal(hex[hexIndex]) << 4; // f_, << 4 gives its true value f_ vs _f
				int hexValue2 = GetHexVal(hex[hexIndex + 1]); // _f
				bytes[index] = (byte)(hexValue1 + hexValue2);
			}
			return bytes;

			int GetHexVal(char c)
			{
				int val = c;
				//For uppercase A-F letters:
				//return val - (val < 58 ? 48 : 55);
				//For lowercase a-f letters:
				//return val - (val < 58 ? 48 : 87);
				//Or the two combined, but a bit slower:
				return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
			}
		}

		public static string BytesToHex(ReadOnlySpan<byte> bytes)
		{
			char[] c = new char[bytes.Length * 2];

			byte b;

			for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
			{
				b = ((byte)(bytes[bx] >> 4));
				c[cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);

				b = ((byte)(bytes[bx] & 0x0F));
				c[++cx] = (char)(b > 9 ? b + 0x37 + 0x20 : b + 0x30);
			}

			return new string(c);
		}
	}
}