using EdjCase.ICP.Candid.Encodings;
using System;
using System.Buffers;
using System.Numerics;
using System.Text;

namespace EdjCase.ICP.Candid.Utilities
{
	internal static class ByteUtil
	{
		public static void WriteOne<T>(this IBufferWriter<T> destination, T value)
		{
			destination.GetSpan(1)[0] = value;
			destination.Advance(1);
		}

		public static void WriteUtf8LebAndValue(this IBufferWriter<byte> destination, ReadOnlySpan<char> utf8String)
		{
			Span<byte> buffer = stackalloc byte[utf8String.Length * 4]; // max size is 4 bytes a char
			int bytesWritten = Encoding.UTF8.GetBytes(utf8String, buffer);
			LEB128.EncodeSigned(bytesWritten, destination); // Encode name length
			destination.Write(buffer[..bytesWritten]); // Encode name
		}

		public static string ToHexString(this byte[] bytes)
		{
			return bytes.AsSpan().ToHexString();
		}

		public static string ToHexString(this Span<byte> bytes)
		{
			return ((ReadOnlySpan<byte>)bytes).ToHexString();
		}

		public static string ToHexString(this ReadOnlySpan<byte> bytes)
		{
			Span<char> stringValue = bytes.Length > 1_000
				? new char[bytes.Length * 2]
				: stackalloc char[bytes.Length * 2];
			int i = 0;
			foreach (byte b in bytes)
			{
				int charIndex = i++ * 2;
				int quotient = Math.DivRem(b, 16, out int remainder);
				stringValue[charIndex] = GetChar(quotient);
				stringValue[charIndex + 1] = GetChar(remainder);
			}

			return new string(stringValue); // returns: "48656C6C6F20776F726C64" for "Hello world"

		}

		public static byte[] FromHexString(this ReadOnlySpan<char> hexString)
		{
			if (hexString.Length % 2 != 0)
			{
				throw new ArgumentException("Hex string must have even hex characters");
			}
			var bytes = new byte[hexString.Length / 2];
			for (var i = 0; i < bytes.Length; i++)
			{
				char char1 = hexString[i * 2];
				char char2 = hexString[i * 2 + 1];
				bytes[i] = GetByte(char1, char2);
			}
			return bytes;
		}


		private static char GetChar(int value)
		{
			if (value < 10)
			{
				return (char)(value + 48); // 0->9
			}
			return (char)(value + 65 - 10); // A->F ASCII
		}

		private static byte GetByte(char char1, char char2)
		{
			int value = GetCharValue(char1) * 16;
			value += GetCharValue(char2);
			return (byte)value; // 0->255
		}

		private static byte GetCharValue(char c)
		{
			if (char.IsDigit(c))
			{
				return (byte)(c - 48); // 0->9
			}
			int offset = char.IsUpper(c) ? 65 : 97;
			return (byte)(c - offset + 10); // A->F
		}
	}
}
