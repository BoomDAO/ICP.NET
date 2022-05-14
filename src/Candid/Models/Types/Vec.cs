using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class VectorCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Vector;

		public CandidTypeDefinition Value { get; }

		public VectorCandidTypeDefinition(CandidTypeDefinition value, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		internal override byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable)
		{
			return this.Value.Encode(compoundTypeTable);
		}

		internal override IEnumerable<CandidTypeDefinition> GetInnerTypes()
		{
			yield return this.Value;
		}

		public override bool Equals(object? obj)
		{
			if (obj is VectorCandidTypeDefinition def)
			{
				return this.Value == def.Value;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(CandidTypeCode.Vector, this.Value);
		}
	}
}
