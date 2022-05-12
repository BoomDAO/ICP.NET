using ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class RecursiveReferenceCandidTypeDefinition : CandidTypeDefinition
	{
		public override CandidTypeCode Type => this.getTypeFunc();

		private readonly Func<CandidTypeCode> getTypeFunc;
		public string RecursiveId { get; }

		public RecursiveReferenceCandidTypeDefinition(string recursiveId, Func<CandidTypeCode> getTypeFunc)
		{
			this.RecursiveId = recursiveId;
			this.getTypeFunc = getTypeFunc;
		}

		public RecursiveReferenceCandidTypeDefinition(string recursiveId, CandidTypeCode type)
		{
			this.RecursiveId = recursiveId;
			this.getTypeFunc = () => type;
		}

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			uint index = compoundTypeTable.GetRecursiveReferenceIndex(this.RecursiveId);
			return LEB128.EncodeUnsigned(index);
		}

		public override bool Equals(object? obj)
		{
			if (obj is RecursiveReferenceCandidTypeDefinition r)
			{
				return this.RecursiveId == r.RecursiveId;
			}
			if (obj is CompoundCandidTypeDefinition c)
			{
				return this.RecursiveId == c.RecursiveId;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.RecursiveId);
		}
	}
}
