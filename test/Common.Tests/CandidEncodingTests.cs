using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Candid.Constants;
using ICP.Common.Models;
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
        [InlineData(0, "0000000000000000")]
        [InlineData(16, "1000000000000000")]
        [InlineData(543210, "EA49080000000000")]
        public void Encode_Nat64(ulong natValue, string expectedHex)
        {
            const string expectedPrefix = "000178";
            var candidNat = CandidPrimitive.Nat64(natValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64));
        }

        [Theory]
        [InlineData(0, "00000000")]
        [InlineData(16, "10000000")]
        [InlineData(543210, "EA490800")]
        public void Encode_Nat32(uint natValue, string expectedHex)
        {
            const string expectedPrefix = "000179";
            var candidNat = CandidPrimitive.Nat32(natValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidNat, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32));
        }

        [Theory]
        [InlineData(0, "0000")]
        [InlineData(16, "1000")]
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
        [InlineData(-4, "7C")]
        [InlineData(-15, "71")]
        [InlineData(-68, "BC7F")]
        [InlineData(624485, "E58E26")]
        [InlineData(-123456, "C0BB78")]
        [InlineData(128, "8001")]
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
            const string expectedHex = "8080E88B96CAB5957F";
            BigInteger bigInteger = -60000000000000000;
            var @int = UnboundedInt.FromBigInteger(bigInteger);
            var candidInt = CandidPrimitive.Int(@int);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int));
        }

        [Theory]
        [InlineData(0, "0000000000000000")]
        [InlineData(16, "1000000000000000")]
        [InlineData(-15, "F1FFFFFFFFFFFFFF")]
        [InlineData(4294967295, "FFFFFFFF00000000")]
        public void Encode_Int64(long intValue, string expectedHex)
        {
            const string expectedPrefix = "000174";
            var candidInt = CandidPrimitive.Int64(intValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int64));
        }

        [Theory]
        [InlineData(0, "00000000")]
        [InlineData(16, "10000000")]
        [InlineData(-15, "F1FFFFFF")]
        [InlineData(65535, "FFFF0000")]
        public void Encode_Int32(int intValue, string expectedHex)
        {
            const string expectedPrefix = "000175";
            var candidInt = CandidPrimitive.Int32(intValue);
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidInt, new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32));
        }

        [Theory]
        [InlineData(0, "0000")]
        [InlineData(16, "1000")]
        [InlineData(-15, "F1FF")]
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
            string expectedPrefix = "016E7C";
            var candidValue = new CandidOptional(null);
            string expectedHex = "010000";
            var typeDef = new OptCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int)); // opt int
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, typeDef);

            candidValue = new CandidOptional(CandidPrimitive.Int(UnboundedInt.FromInt64(42)));
            expectedHex = "0100012A";
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, typeDef);


            candidValue = new CandidOptional(candidValue);
            // TODO docs say this but I order the type table differently. Should work though?
            //expectedPrefix = "026E016E7C";
            //expectedHex = "010001012A";
            expectedPrefix = "026E7C6E00";
            expectedHex = "010101012A";
            typeDef = new OptCandidTypeDefinition(typeDef); // opt opt int
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, typeDef);
        }

        [Fact]
        public void Encode_Vec()
        {
            var candidValue = new CandidVector(new CandidValue[0]);
            string expectedPrefix = "016D7C0100";
            string expectedHex = "00";
            var typeDef = new VectorCandidTypeDefinition(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int));
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, typeDef);

            var vector = new CandidPrimitive[]
            {
                CandidPrimitive.Int(UnboundedInt.FromInt64(1)),
                CandidPrimitive.Int(UnboundedInt.FromInt64(2))
            };
            candidValue = new CandidVector(vector);
            expectedHex = "020102";
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, typeDef);
        }

        [Theory]
        [InlineData("id", 23515)]
        [InlineData("", 0)]
        [InlineData("description", 1595738364)]
        [InlineData("_1.23_", 1360503298)]
        public void Encode_Label(string name, uint hashedName)
        {
            uint digest = Label.HashName(name);
            Assert.Equal(hashedName, digest);

        }

        [Fact]
        public void Encode_Record_Ids()
        {
            var candidValue = new CandidRecord(new Dictionary<Label, CandidValue>
            {
                {new Label(1), CandidPrimitive.Int(UnboundedInt.FromInt64(42)) },
            });
            string expectedPrefix = "";
            string expectedHex = "016C01017C01002A";
            var typeDef = new RecordCandidTypeDefinition(new Dictionary<Label, CandidTypeDefinition>
            {
                {new Label(1), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int) },
            });
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, typeDef);
        }

        [Fact]
        public void Encode_Record_Named()
        {
            var candidValue = new CandidRecord(new Dictionary<Label, CandidValue>
            {
                {Label.FromName("foo"),CandidPrimitive.Int(UnboundedInt.FromInt64(42)) },
                {Label.FromName("bar"), CandidPrimitive.Bool(true) }
            });
            string expectedPrefix = "";
            // TODO ordering of types again
            //string expectedHex = "016C02D3E3AA027E868EB7027C0100012A";
            string expectedHex = "016C02868EB7027CD3E3AA027E0100012A";
            var typeDef = new RecordCandidTypeDefinition(new Dictionary<Label, CandidTypeDefinition>
            {
                {Label.FromName("foo"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int) },
                {Label.FromName("bar"), new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool) },
            });
            TestUtil.AssertEncodedCandid(expectedHex, expectedPrefix, candidValue, typeDef);
        }

        [Fact]
        public void Encode_Variant()
        {
            //4449444C Magic header
            //03 3 types
            //6B -21 Variant [0]
            //02 variant length
            //9CC201 'ok'
            //02 ref
            //E58EB402 'err'
            //01 ref
            //6B -21 Variant [1]
            //01 variant length
            //CFA0DEF206 'NotFound'
            //7F -1 Null
            //6C -20 record [2]
            //05 record length
            //C4A7C9A101 'total'
            //79 -7 NAT32
            //DC8BD3F401 'desktop'
            //79 -7 NAT32
            //8D98F3E704 'time'
            //7C INT
            //E2D8DEFB0B 'mobile'
            //79 -7 NAT32
            //89FB97EB0E 'route'
            //71 -15 Text
            //01 Arg count
            //00 ref
            //01 variant index/tag
            //00 variant value
            const string hex = "4449444C036B029CC20102E58EB402016B01CFA0DEF2067F6C05C4A7C9A10179DC8BD3F401798D98F3E7047CE2D8DEFB0B7989FB97EB0E7101000100";
            var value1 = new CandidVariant("err", new CandidVariant("NotFound"));
            var type1 = new VariantCandidTypeDefinition(new Dictionary<Label, CandidTypeDefinition>
            {
                {
                    Label.FromName("ok"),
                    new RecordCandidTypeDefinition(new Dictionary<Label, CandidTypeDefinition>
                    {
                        { "total", new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32) },
                        { "desktop", new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32) },
                        { "time", new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int) },
                        { "mobile", new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32) },
                        { "route", new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text) }
                    })
                },
                {
                    Label.FromName("err"),
                    new VariantCandidTypeDefinition(new Dictionary<Label, CandidTypeDefinition>
                    {
                        {"NotFound", new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Null) }
                    })
                }
            });
            var expectedArg = CandidArg.FromCandid(new List<(CandidValue, CandidTypeDefinition)>
            {
                (value1, type1)
            }, null);

            byte[] actualBytes = Convert.FromHexString(hex);
            CandidArg actualArg = CandidArg.FromBytes(actualBytes);

            Assert.Equal(expectedArg, actualArg);
        }
        [Fact]
        public void Encode_Record_Recursive()
        {
            //4449444C Magic header
            //02 2 types -- Compound Types start
            //6E Opt [0]
            //01 Ref [1]
            //6C -20 Record [1]
            //01 record length
            //A78A839908 'selfRef'
            //00 Ref [0]
            //01 Arg count -- Arg types start
            //01 Ref [1]
            //01 Opt has value -- Values start
            //00 Opt does not have value
            const string actualHex = "4449444C026E016C01A78A8399080001010100";

            var value1 = new CandidRecord(new Dictionary<Label, CandidValue>
            {
                {
                    Label.FromName("selfRef"),
                    new CandidOptional(new CandidRecord(new Dictionary<Label, CandidValue>
                    {
                        {
                            Label.FromName("selfRef"),
                            new CandidOptional()
                        }
                    }))
                }
            });
            var type1 = new RecordCandidTypeDefinition(new Dictionary<Label, CandidTypeDefinition>
            {
                {
                    Label.FromName("selfRef"),
                    new OptCandidTypeDefinition(new RecursiveReferenceCandidTypeDefinition("rec_1", IDLTypeCode.Record))
                }
            }, "rec_1");
            var expectedArg = CandidArg.FromCandid(new List<(CandidValue, CandidTypeDefinition)>
            {
                (value1, type1)
            });

            byte[] actualBytes = Convert.FromHexString(actualHex);
            CandidArg actualArg = CandidArg.FromBytes(actualBytes);

            string expectedHex = Convert.ToHexString(expectedArg.Encode());

            Assert.Equal(expectedHex, actualHex);
            Assert.Equal(expectedArg, actualArg);
        }
    }
}
