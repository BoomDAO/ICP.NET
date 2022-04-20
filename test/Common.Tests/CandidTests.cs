using ICP.Common.Candid;
using Xunit;

namespace Common.Tests
{
    public class CandidTests
    {
        [Theory]
        [InlineData(1, "")]
        public void Test1()
        {
            var nat = UnboundedUInt.FromUInt64(1);
            var candidNat = CandidPrimitive.Nat(nat);
            byte[] encodedValue = candidNat.EncodeValue();
        }
    }
}