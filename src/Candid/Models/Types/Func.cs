using ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public enum FuncMode
	{
		Oneway = 2, // No response
		Query = 1 // Response
	}
	public class FuncCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Func;

		public IReadOnlyList<FuncMode> Modes { get; }
		public IReadOnlyList<CandidTypeDefinition> ArgTypes { get; }
		public IReadOnlyList<CandidTypeDefinition> ReturnTypes { get; }

		public FuncCandidTypeDefinition(
			List<FuncMode> modes,
			List<CandidTypeDefinition> argTypes,
			List<CandidTypeDefinition> returnTypes,
			CandidId? recursiveId = null)
			: base(recursiveId)
		{
			this.Modes = modes.Distinct().ToList();
			this.ArgTypes = argTypes;
			this.ReturnTypes = returnTypes;
		}

		internal override byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable)
		{
			byte[] argsCount = LEB128.EncodeSigned(this.ArgTypes.Count);

			IEnumerable<byte> argTypes = this.ArgTypes
				.SelectMany(a => a.Encode(compoundTypeTable));

			byte[] returnsCount = LEB128.EncodeSigned(this.ReturnTypes.Count);

			IEnumerable<byte> returnTypes = this.ReturnTypes
				.SelectMany(a => a.Encode(compoundTypeTable));

			byte[] modesCount = LEB128.EncodeSigned(this.Modes.Count);

			IEnumerable<byte> modeTypes = this.Modes
				.SelectMany(m => LEB128.EncodeSigned((UnboundedInt)(long)m));

			return argsCount
				.Concat(argTypes)
				.Concat(returnsCount)
				.Concat(returnTypes)
				.Concat(modesCount)
				.Concat(modeTypes)
				.ToArray();
		}

		internal override IEnumerable<CandidTypeDefinition> GetInnerTypes()
		{
			return this.ArgTypes.Concat(this.ReturnTypes);
		}

		public override bool Equals(object? obj)
		{
			if (obj is FuncCandidTypeDefinition sDef)
			{
				return this.Modes
					.OrderBy(s => s)
					.SequenceEqual(sDef.Modes.OrderBy(s => s))
					&& this.ArgTypes.SequenceEqual(sDef.ArgTypes)
					&& this.ReturnTypes.SequenceEqual(sDef.ArgTypes);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Modes, this.ArgTypes, this.ReturnTypes);
		}
    }
}
