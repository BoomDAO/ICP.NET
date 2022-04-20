using ICP.Common.Candid;
using Xunit;

namespace Common.Tests
{
	public class CandidTests
	{
		[Theory]
		[InlineData(1, "")]
		public void Encode_Nat(ulong natValue, string expectedHex)
		{
			var nat = UnboundedUInt.FromUInt64(1);
			var candidNat = CandidPrimitive.Nat(nat);
			byte[] encodedValue = candidNat.EncodeValue();
			string actualHex = HexUtil.BytesToHex(encodedValue);
			Assert.Equal(expectedHex, actualHex);
		}
	}
}