using ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
    public class BinarySequenceTests
    {
        [Theory]
        [InlineData("0A0B0C0D", true, "00001010000010110000110000001101")]
        [InlineData("0A0B0C0D", false, "00001101000011000000101100001010")]
        public void BigEndian(string hexBytes, bool isBigEndian, string binary)
        {
            byte[] bytes = ByteUtil.FromHexString(hexBytes);
            var sequence = BinarySequence.FromBytes(bytes, isBigEndian: isBigEndian);
            Assert.Equal(binary, sequence.ToString());
        }

        [Theory]
        [InlineData("0A", true, true, 10)]
        [InlineData("0A", false, true, 10)]
        [InlineData("0A", true, false, 10)]
        [InlineData("0A", false, false, 10)]

        [InlineData("FF", true, true, 255)]
        [InlineData("FF", false, true, -1)]
        [InlineData("FF", true, false, 255)]
        [InlineData("FF", false, false, -1)]

        [InlineData("FFFF", true, true, 65535)]
        [InlineData("FFFF", false, true, -1)]
        [InlineData("FFFF", true, false, 65535)]
        [InlineData("FFFF", false, false, -1)]

        [InlineData("FF0A", true, true, 65290)]
        [InlineData("FF0A", false, true, -246)]
        [InlineData("FF0A", true, false, 2815)]
        [InlineData("FF0A", false, false, 2815)]
        public void ToBigInteger(string hexBytes, bool isUnsigned, bool isBigEndian, long expected)
        {
            byte[] bytes = ByteUtil.FromHexString(hexBytes);
            BigInteger actual = bytes.ToBigInteger(isUnsigned: isUnsigned, isBigEndian: isBigEndian);

            Assert.Equal(expected, actual);
        }
    }
}
