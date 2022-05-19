using ICP.Candid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
    public class BinarySequenceTests
    {
        [Theory]
        [InlineData("0A0B0C0D", "1010")]
        public void BigEndian(string hexBytes, string binary)
        {
            byte[] bytes = ByteUtil.FromHexString(hexBytes);
            var sequence = BinarySequence.FromBytes(bytes, isBigEndian: true);
            Assert.Equal(binary, sequence.ToString());
        }
    }
}
