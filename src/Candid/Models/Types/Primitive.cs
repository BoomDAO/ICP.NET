using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidPrimitiveType : CandidKnownType
	{
		public override CandidTypeCode Type { get; }
		public PrimitiveType PrimitiveType { get; }

		public CandidPrimitiveType(PrimitiveType type)
		{
			this.PrimitiveType = type;
			this.Type = type switch
			{
				PrimitiveType.Text => CandidTypeCode.Text,
				PrimitiveType.Nat => CandidTypeCode.Nat,
				PrimitiveType.Nat8 => CandidTypeCode.Nat8,
				PrimitiveType.Nat16 => CandidTypeCode.Nat16,
				PrimitiveType.Nat32 => CandidTypeCode.Nat32,
				PrimitiveType.Nat64 => CandidTypeCode.Nat64,
				PrimitiveType.Int => CandidTypeCode.Int,
				PrimitiveType.Int8 => CandidTypeCode.Int8,
				PrimitiveType.Int16 => CandidTypeCode.Int16,
				PrimitiveType.Int32 => CandidTypeCode.Int32,
				PrimitiveType.Int64 => CandidTypeCode.Int64,
				PrimitiveType.Float32 => CandidTypeCode.Float32,
				PrimitiveType.Float64 => CandidTypeCode.Float64,
				PrimitiveType.Bool => CandidTypeCode.Bool,
				PrimitiveType.Null => CandidTypeCode.Null,
				PrimitiveType.Empty => CandidTypeCode.Empty,
				PrimitiveType.Reserved => CandidTypeCode.Reserved,
				PrimitiveType.Principal => CandidTypeCode.Principal,
				_ => throw new NotImplementedException(),
			};
		}

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			return LEB128.EncodeSigned((long)this.Type);
		}

		public override bool Equals(object? obj)
		{
			if (obj is CandidPrimitiveType pDef)
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
