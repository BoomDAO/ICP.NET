using System.Diagnostics;

namespace EdjCase.ICP.Candid.Crypto
{
	internal static class CryptoUtil
	{
		public static void ConvertULongToBytesSwapOrder(ulong a_in, byte[] a_out, int a_index)
		{
			Debug.Assert(a_index + 8 <= a_out.Length);

			a_out[a_index++] = (byte)(a_in >> 56);
			a_out[a_index++] = (byte)(a_in >> 48);
			a_out[a_index++] = (byte)(a_in >> 40);
			a_out[a_index++] = (byte)(a_in >> 32);
			a_out[a_index++] = (byte)(a_in >> 24);
			a_out[a_index++] = (byte)(a_in >> 16);
			a_out[a_index++] = (byte)(a_in >> 8);
			a_out[a_index++] = (byte)a_in;
		}

		public static void ConvertBytesToUIntsSwapOrder(byte[] a_in, int a_index, int a_length, uint[] a_result, int a_index_out)
		{
			for (int i = a_index_out; a_length > 0; a_length -= 4)
			{
				a_result[i++] =
					((uint)a_in[a_index++] << 24) |
					((uint)a_in[a_index++] << 16) |
					((uint)a_in[a_index++] << 8) |
					a_in[a_index++];
			}
		}

		public static byte[] ConvertUIntsToBytesSwapOrder(uint[] a_in, int a_index = 0, int a_length = -1)
		{
			if (a_length == -1)
				a_length = a_in.Length;

			byte[] result = new byte[a_length * 4];

			for (int j = 0; a_length > 0; a_length--, a_index++)
			{
				result[j++] = (byte)(a_in[a_index] >> 24);
				result[j++] = (byte)(a_in[a_index] >> 16);
				result[j++] = (byte)(a_in[a_index] >> 8);
				result[j++] = (byte)a_in[a_index];
			}

			return result;
		}
	}
}