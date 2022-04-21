using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Candid.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Common.Tests
{
    // https://github.com/dfinity/candid/blob/master/test/prim.test.did
    // https://github.com/dfinity/candid/blob/master/test/reference.test.did
    public class CandidEncodingTests
    {
        [Theory]
        [InlineData(0, "00")]
        [InlineData(1, "01")]
        [InlineData(127, "7F")]
        [InlineData(624485, "E58E26")]
        public void Encode_Nat(ulong natValue, string expectedHex)
        {
            const string expectedPrefix = "00017D";
            var nat = UnboundedUInt.FromUInt64(natValue);
            var candidNat = CandidPrimitive.Nat(nat);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat));
        }

        [Fact]
        public void Encode_Nat_Big()
        {
            const string expectedPrefix = "00017D";
            const string expectedHex = "808098F4E9B5CA6A";
            BigInteger bigInteger = 60000000000000000;
            var nat = UnboundedUInt.FromBigInteger(bigInteger);
            var candidNat = CandidPrimitive.Nat(nat);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(543210, "EA4908")]
        public void Encode_Nat64(ulong natValue, string expectedHex)
        {
            const string expectedPrefix = "000178";
            var candidNat = CandidPrimitive.Nat64(natValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(543210, "EA4908")]
        public void Encode_Nat32(uint natValue, string expectedHex)
        {
            const string expectedPrefix = "000179";
            var candidNat = CandidPrimitive.Nat32(natValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(9999, "0F27")]
        public void Encode_Nat16(ushort natValue, string expectedHex)
        {
            const string expectedPrefix = "00017A";
            var candidNat = CandidPrimitive.Nat16(natValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat16));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(99, "63")]
        public void Encode_Nat8(byte natValue, string expectedHex)
        {
            const string expectedPrefix = "00017B";
            var candidNat = CandidPrimitive.Nat8(natValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(-15, "71")]
        [InlineData(624485, "E58E26")]
        [InlineData(-123456, "C0BB78")]
        public void Encode_Int(long intValue, string expectedHex)
        {
            const string expectedPrefix = "00017C";
            var @int = UnboundedInt.FromInt64(intValue);
            var candidInt = CandidPrimitive.Int(@int);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int));
        }

        [Fact]
        public void Encode_Int_Big()
        {
            const string expectedPrefix = "00017C";
            const string expectedHex = "FFFFFFFFFFFFFFFFFFFFFFFFFFFF03";
            BigInteger bigInteger = BigInteger.Pow(2, 100) - 1;
            var @int = UnboundedInt.FromBigInteger(bigInteger);
            var candidInt = CandidPrimitive.Int(@int);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(-15, "F1")]
        [InlineData(543210, "EA4908")]
        public void Encode_Int64(long intValue, string expectedHex)
        {
            const string expectedPrefix = "000174";
            var candidInt = CandidPrimitive.Int64(intValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int64));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(-15, "F1")]
        [InlineData(543210, "EA4908")]
        public void Encode_Int32(int intValue, string expectedHex)
        {
            const string expectedPrefix = "000175";
            var candidInt = CandidPrimitive.Int32(intValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(-15, "F1")]
        [InlineData(9999, "0F27")]
        public void Encode_Int16(short intValue, string expectedHex)
        {
            const string expectedPrefix = "000176";
            var candidInt = CandidPrimitive.Int16(intValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int16));
        }

        [Theory]
        [InlineData(0, "00")]
        [InlineData(16, "10")]
        [InlineData(99, "63")]
        public void Encode_Int8(byte intValue, string expectedHex)
        {
            const string expectedPrefix = "000177";
            var candidInt = CandidPrimitive.Int8((sbyte)intValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int8));
        }
        [Fact]
        public void Encode_Int8_Neg()
        {
            const string expectedPrefix = "000177";
            const string expectedHex = "F1";
            var candidInt = CandidPrimitive.Int8((sbyte)-15);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int8));
        }

        [Theory]
        [InlineData(1.0, "0000803F")]
        [InlineData(1.23456, "10069E3F")]
        [InlineData(-98765.4321, "B7E6C0C7")]
        public void Encode_Float32(float value, string expectedHex)
        {
            const string expectedPrefix = "000173";
            var candidValue = CandidPrimitive.Float32(value);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Float32));
        }

        [Theory]
        [InlineData(1.0, "000000000000F03F")]
        [InlineData(1.23456, "38328FFCC1C0F33F")]
        [InlineData(-98765.4321, "8AB0E1E9D61CF8C0")]
        public void Encode_Float64(double value, string expectedHex)
        {
            const string expectedPrefix = "000172";
            var candidValue = CandidPrimitive.Float64(value);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Float64));
        }

        [Theory]
        [InlineData(false, "00")]
        [InlineData(true, "01")]
        public void Encode_Bool(bool value, string expectedHex)
        {
            const string expectedPrefix = "00017E";
            var candidValue = CandidPrimitive.Bool(value);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool));
        }

        [Theory]
        [InlineData("", "00")]
        [InlineData("A", "0141")]
        [InlineData("The quick brown fox jumps over the lazy dog", "2B54686520717569636B2062726F776E20666F78206A756D7073206F76657220746865206C617A7920646F67")]
        public void Encode_Text(string value, string expectedHex)
        {
            const string expectedPrefix = "000171";
            var candidValue = CandidPrimitive.Text(value);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text));
        }

        [Fact]
        public void Encode_Null()
        {
            const string expectedPrefix = "00017F";
            var candidValue = CandidPrimitive.Null();
            string expectedHex = "";
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Null));
        }

        [Fact]
        public void Encode_Reserved()
        {
            const string expectedPrefix = "000170";
            var candidValue = CandidPrimitive.Reserved();
            string expectedHex = "";
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Reserved));
        }

        [Fact]
        public void Encode_Opt()
        {
            const string expectedPrefix = "016E7C";
            var candidValue = new CandidOptional();
            string expectedHex = "010000";
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Null)));

            candidValue = new CandidOptional(CandidPrimitive.Int(UnboundedInt.FromInt64(42)));
            expectedHex = "0100012A";
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool)));


            candidValue = new CandidOptional(new CandidOptional(CandidPrimitive.Null()));
            expectedHex = "026e016e000100010100";
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool)));
        }

        //[Fact]
        //public void Encode_Vec()
        //{
        //    var candidValue = new CandidVector(new CandidValue[0]);
        //    string expectedHex = "00";
        //    TestUtil.AssertEncodedCandid(expectedHex, candidValue);

        //    var vector = new CandidValue[]
        //    {
        //        CandidPrimitive.Bool(false),
        //        CandidPrimitive.Int32(-234),
        //        CandidPrimitive.Text("Test"),
        //    };
        //    candidValue = new CandidVector(vector);
        //    expectedHex = "080016FF0454657374";
        //    TestUtil.AssertEncodedCandid(expectedHex, candidValue);
        //}

        //[Fact]
        //public void Encode_Record()
        //{
        //    var candidValue = new CandidRecord(new Dictionary<Label, CandidValue>());
        //    string expectedHex = "";
        //    TestUtil.AssertEncodedCandid(expectedHex, candidValue);

        //    candidValue = new CandidRecord(new Dictionary<Label, CandidValue>
        //    {
        //        {Label.FromName("bool"), CandidPrimitive.Bool(true) },
        //        {Label.FromName("int32"), CandidPrimitive.Int32(-234) },
        //        {Label.FromName("test"), CandidPrimitive.Text("Test") }
        //    });
        //    expectedHex = "01045465737416FF";
        //    TestUtil.AssertEncodedCandid(expectedHex, candidValue);
        //}
    }
}
