using ICP.Candid.Encodings;
using ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
    public class PrimitiveCandidTypeDefinition : CandidTypeDefinition
    {
        public override IDLTypeCode Type { get; }
        public CandidPrimitiveType PrimitiveType { get; }

        public PrimitiveCandidTypeDefinition(CandidPrimitiveType type)
        {
            this.PrimitiveType = type;
            this.Type = type switch
            {
                CandidPrimitiveType.Text => IDLTypeCode.Text,
                CandidPrimitiveType.Nat => IDLTypeCode.Nat,
                CandidPrimitiveType.Nat8 => IDLTypeCode.Nat8,
                CandidPrimitiveType.Nat16 => IDLTypeCode.Nat16,
                CandidPrimitiveType.Nat32 => IDLTypeCode.Nat32,
                CandidPrimitiveType.Nat64 => IDLTypeCode.Nat64,
                CandidPrimitiveType.Int => IDLTypeCode.Int,
                CandidPrimitiveType.Int8 => IDLTypeCode.Int8,
                CandidPrimitiveType.Int16 => IDLTypeCode.Int16,
                CandidPrimitiveType.Int32 => IDLTypeCode.Int32,
                CandidPrimitiveType.Int64 => IDLTypeCode.Int64,
                CandidPrimitiveType.Float32 => IDLTypeCode.Float32,
                CandidPrimitiveType.Float64 => IDLTypeCode.Float64,
                CandidPrimitiveType.Bool => IDLTypeCode.Bool,
                CandidPrimitiveType.Null => IDLTypeCode.Null,
                CandidPrimitiveType.Empty => IDLTypeCode.Empty,
                CandidPrimitiveType.Reserved => IDLTypeCode.Reserved,
                CandidPrimitiveType.Principal => IDLTypeCode.Principal,
                _ => throw new NotImplementedException(),
            };
        }

        public override byte[] Encode(CompoundTypeTable compoundTypeTable)
        {
            return LEB128.EncodeSigned((long)this.Type);
        }

        public override bool Equals(object? obj)
        {
            if (obj is PrimitiveCandidTypeDefinition pDef)
            {
                return this.Type == pDef.Type;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (int)this.Type;
        }

        public override string ToString()
        {
            return this.PrimitiveType switch
            {
                CandidPrimitiveType.Text => "text",
                CandidPrimitiveType.Nat => "nat",
                CandidPrimitiveType.Nat8 => "nat8",
                CandidPrimitiveType.Nat16 => "nat16",
                CandidPrimitiveType.Nat32 => "nat32",
                CandidPrimitiveType.Nat64 => "nat64",
                CandidPrimitiveType.Int => "int",
                CandidPrimitiveType.Int8 => "int8",
                CandidPrimitiveType.Int16 => "int16",
                CandidPrimitiveType.Int32 => "int32",
                CandidPrimitiveType.Int64 => "int64",
                CandidPrimitiveType.Float32 => "float32",
                CandidPrimitiveType.Float64 => "float64",
                CandidPrimitiveType.Bool => "bool",
                CandidPrimitiveType.Principal => "principal",
                CandidPrimitiveType.Reserved => "reserved",
                CandidPrimitiveType.Empty => "empty",
                CandidPrimitiveType.Null => "null",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
