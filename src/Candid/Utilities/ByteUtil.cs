using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ICP.Candid.Utilities
{
    public static class ByteUtil
    {
        public static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        public static byte[] FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        public static BigInteger ToBigInteger(this byte[] bytes, bool isUnsigned, bool isBigEndian)
        {
            // BigInteger takes a twos compliment little endian value
            if (isUnsigned || isBigEndian)
            {
                BinarySequence bits = BinarySequence.FromBytes(bytes, isBigEndian);
                if (isUnsigned)
                {
                    // Convert unsigned to signed
                    bits = bits.ToTwosCompliment();
                }
                bytes = bits.ToByteArray(bigEndian: false);
            }
            return new BigInteger(bytes);
        }
    }
}
