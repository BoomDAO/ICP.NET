using ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class ReferenceCandidTypeDefinition : CandidTypeDefinition
	{
		public override CandidTypeCode Type => this.getTypeFunc();

		private readonly Func<CandidTypeCode> getTypeFunc;
		public CandidId Id { get; }

		public ReferenceCandidTypeDefinition(CandidId id, Func<CandidTypeCode> getTypeFunc)
		{
			this.Id = id;
			this.getTypeFunc = getTypeFunc;
		}

		public ReferenceCandidTypeDefinition(CandidId id, CandidTypeCode type)
		{
			this.Id = id;
			this.getTypeFunc = () => type;
		}

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			uint index = compoundTypeTable.GetRecursiveReferenceIndex(this.Id);
			return LEB128.EncodeUnsigned(index);
		}

		public override bool Equals(object? obj)
		{
			if (obj is ReferenceCandidTypeDefinition r)
			{
				return this.Id == r.Id;
			}
			if (obj is CompoundCandidTypeDefinition c)
			{
				return this.Id == c.RecursiveId;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Id);
		}
	}
}
