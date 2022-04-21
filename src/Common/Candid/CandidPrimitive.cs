using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Common.Candid
{
    public enum CandidPrimitiveType
    {
        Text,
        Nat,
        Nat8,
        Nat16,
        Nat32,
        Nat64,
        Int,
        Int8,
        Int16,
        Int32,
        Int64,
        Float32,
        Float64,
        Bool,
        Reserved,
        Empty,
        Null
    }

    public class CandidPrimitive : CandidValue
    {
        public override CandidValueType Type { get; } = CandidValueType.Primitive;
        public CandidPrimitiveType ValueType { get; }
        private readonly object? value;

        private CandidPrimitive(CandidPrimitiveType valueType, object? value)
        {
            this.ValueType = valueType;
            this.value = value;
        }

        public string AsText()
        {
            this.ValidateType(CandidPrimitiveType.Text);
            return (string)this.value!;
        }

        public UnboundedUInt AsNat()
        {
            this.ValidateType(CandidPrimitiveType.Nat);
            return (UnboundedUInt)this.value!;
        }

        public byte AsNat8()
        {
            this.ValidateType(CandidPrimitiveType.Nat8);
            return (byte)this.value!;
        }

        public ushort AsNat16()
        {
            this.ValidateType(CandidPrimitiveType.Nat16);
            return (ushort)this.value!;
        }

        public uint AsNat32()
        {
            this.ValidateType(CandidPrimitiveType.Nat32);
            return (uint)this.value!;
        }

        public ulong AsNat64()
        {
            this.ValidateType(CandidPrimitiveType.Nat64);
            return (ulong)this.value!;
        }

        public UnboundedInt AsInt()
        {
            this.ValidateType(CandidPrimitiveType.Int);
            return (UnboundedInt)this.value!;
        }

        public sbyte AsInt8()
        {
            this.ValidateType(CandidPrimitiveType.Int8);
            return (sbyte)this.value!;
        }

        public short AsInt16()
        {
            this.ValidateType(CandidPrimitiveType.Int16);
            return (short)this.value!;
        }

        public int AsInt32()
        {
            this.ValidateType(CandidPrimitiveType.Int32);
            return (int)this.value!;
        }

        public long AsInt64()
        {
            this.ValidateType(CandidPrimitiveType.Int64);
            return (long)this.value!;
        }

        public float AsFloat32()
        {
            this.ValidateType(CandidPrimitiveType.Float32);
            return (float)this.value!;
        }

        public double AsFloat64()
        {
            this.ValidateType(CandidPrimitiveType.Float64);
            return (double)this.value!;
        }

        public bool AsBool()
        {
            this.ValidateType(CandidPrimitiveType.Bool);
            return (bool)this.value!;
        }





        public override byte[] EncodeValue()
        {
            return this.ValueType switch
            {
                CandidPrimitiveType.Text => this.EncodeText(),
                CandidPrimitiveType.Nat => this.EncodeNat(),
                CandidPrimitiveType.Nat8 => this.EncodeNat8(),
                CandidPrimitiveType.Nat16 => this.EncodeNat16(),
                CandidPrimitiveType.Nat32 => this.EncodeNat32(),
                CandidPrimitiveType.Nat64 => this.EncodeNat64(),
                CandidPrimitiveType.Int => this.EncodeInt(),
                CandidPrimitiveType.Int8 => this.EncodeInt8(),
                CandidPrimitiveType.Int16 => this.EncodeInt16(),
                CandidPrimitiveType.Int32 => this.EncodeInt32(),
                CandidPrimitiveType.Int64 => this.EncodeInt64(),
                CandidPrimitiveType.Float32 => this.EncodeFloat32(),
                CandidPrimitiveType.Float64 => this.EncodeFloat64(),
                CandidPrimitiveType.Bool => this.EncodeBool(),
                CandidPrimitiveType.Null => this.EncodeNull(),
                CandidPrimitiveType.Reserved => this.EncodeReserved(),
                // exclude empty, will never be encoded
                _ => throw new NotImplementedException()
            };
        }

        private byte[] EncodeText()
        {
            string value = this.AsText();
            // bytes = Length (LEB128) + text (UTF8)
            return LEB128.FromUInt64((ulong)value.Length).Raw
                       .Concat(Encoding.UTF8.GetBytes(value))
                       .ToArray();
        }

        private byte[] EncodeNat()
        {
            UnboundedUInt value = this.AsNat();
            return LEB128.FromNat(value).Raw;
        }

        private byte[] EncodeNat8()
        {
            byte value = this.AsNat8();
            return new byte[] { value };
        }

        private byte[] EncodeNat16()
        {
            ushort value = this.AsNat16();
            return new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
        }

        private byte[] EncodeNat32()
        {
            uint value = this.AsNat32();
            return new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
        }

        private byte[] EncodeNat64()
        {
            ulong value = this.AsNat64();
            return new BigInteger(value).ToByteArray(isUnsigned: true, isBigEndian: false);
        }

        private byte[] EncodeInt()
        {
            UnboundedInt value = this.AsInt();
            return SLEB128.FromInt(value).Raw;
        }

        private byte[] EncodeInt8()
        {
            sbyte value = this.AsInt8();
            return new byte[] { (byte)value };
        }

        private byte[] EncodeInt16()
        {
            short value = this.AsInt16();
            return new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
        }

        private byte[] EncodeInt32()
        {
            int value = this.AsInt32();
            return new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
        }

        private byte[] EncodeInt64()
        {
            long value = this.AsInt64();
            return new BigInteger(value).ToByteArray(isUnsigned: false, isBigEndian: false);
        }

        private byte[] EncodeFloat32()
        {
            float value = this.AsFloat32();
            return BitConverter.GetBytes(value);
        }

        private byte[] EncodeFloat64()
        {
            double value = this.AsFloat64();
            return BitConverter.GetBytes(value);
        }

        private byte[] EncodeBool()
        {
            bool value = this.AsBool();
            return BitConverter.GetBytes(value);
        }

        private byte[] EncodeNull()
        {
            return new byte[0];
        }

        private byte[] EncodeReserved()
        {
            return new byte[0];
        }






        public static CandidPrimitive Text(string value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Text, value);
        }

        public static CandidPrimitive Nat(UnboundedUInt value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Nat, value);
        }

        public static CandidPrimitive Nat8(byte value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Nat8, value);
        }

        public static CandidPrimitive Nat16(ushort value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Nat16, value);
        }

        public static CandidPrimitive Nat32(uint value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Nat32, value);
        }

        public static CandidPrimitive Nat64(ulong value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Nat64, value);
        }

        public static CandidPrimitive Int(UnboundedInt value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Int, value);
        }

        public static CandidPrimitive Int8(sbyte value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Int8, value);
        }

        public static CandidPrimitive Int16(short value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Int16, value);
        }

        public static CandidPrimitive Int32(int value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Int32, value);
        }

        public static CandidPrimitive Int64(long value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Int64, value);
        }

        public static CandidPrimitive Float32(float value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Float32, value);
        }

        public static CandidPrimitive Float64(double value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Float64, value);
        }

        public static CandidPrimitive Bool(bool value)
        {
            return new CandidPrimitive(CandidPrimitiveType.Bool, value);
        }
        public static CandidValue Null()
        {
            return new CandidPrimitive(CandidPrimitiveType.Null, null);
        }

        public static CandidValue Reserved()
        {
            return new CandidPrimitive(CandidPrimitiveType.Reserved, null);
        }

        public static CandidValue Empty()
        {
            return new CandidPrimitive(CandidPrimitiveType.Empty, null);
        }







        protected void ValidateType(CandidPrimitiveType type)
        {
            if (this.ValueType != type)
            {
                throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
            }
        }
    }
}
