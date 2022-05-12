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
        public override CandidTypeCode Type { get; }
        public CandidPrimitiveType PrimitiveType { get; }

        public PrimitiveCandidTypeDefinition(CandidPrimitiveType type)
        {
            this.PrimitiveType = type;
            this.Type = type switch
            {
                CandidPrimitiveType.Text => CandidTypeCode.Text,
                CandidPrimitiveType.Nat => CandidTypeCode.Nat,
                CandidPrimitiveType.Nat8 => CandidTypeCode.Nat8,
                CandidPrimitiveType.Nat16 => CandidTypeCode.Nat16,
                CandidPrimitiveType.Nat32 => CandidTypeCode.Nat32,
                CandidPrimitiveType.Nat64 => CandidTypeCode.Nat64,
                CandidPrimitiveType.Int => CandidTypeCode.Int,
                CandidPrimitiveType.Int8 => CandidTypeCode.Int8,
                CandidPrimitiveType.Int16 => CandidTypeCode.Int16,
                CandidPrimitiveType.Int32 => CandidTypeCode.Int32,
                CandidPrimitiveType.Int64 => CandidTypeCode.Int64,
                CandidPrimitiveType.Float32 => CandidTypeCode.Float32,
                CandidPrimitiveType.Float64 => CandidTypeCode.Float64,
                CandidPrimitiveType.Bool => CandidTypeCode.Bool,
                CandidPrimitiveType.Null => CandidTypeCode.Null,
                CandidPrimitiveType.Empty => CandidTypeCode.Empty,
                CandidPrimitiveType.Reserved => CandidTypeCode.Reserved,
                CandidPrimitiveType.Principal => CandidTypeCode.Principal,
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
    }
}
