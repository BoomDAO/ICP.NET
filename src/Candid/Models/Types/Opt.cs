using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class OptCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Opt;
		public CandidTypeDefinition Value { get; }

		public OptCandidTypeDefinition(CandidTypeDefinition value, string? recursiveId = null) : base(recursiveId)
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
			if (obj is OptCandidTypeDefinition def)
			{
				return this.Value == def.Value;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(CandidTypeCode.Opt, this.Value);
		}

		protected override string ToStringInternal()
		{
			return $"opt {this.Value}";
		}
    }
}
