using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models.Types
{
	public enum FuncMode
	{
		Oneway = 2, // No response
		Query = 1 // Response
	}
	public class CandidFuncType : CandidCompoundType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Func;

		public List<FuncMode> Modes { get; }
		public List<(CandidId? Name, CandidType Type)> ArgTypes { get; }
		public List<(CandidId? Name, CandidType Type)> ReturnTypes { get; }

		public CandidFuncType(
			List<FuncMode> modes,
			List<CandidType> argTypes,
			List<CandidType> returnTypes,
			CandidId? recursiveId = null)
			: this(
				  modes,
				  argTypes.Select(t => ((CandidId?)null, t)).ToList(),
				  returnTypes.Select(t => ((CandidId?)null, t)).ToList(),
				  recursiveId
			)
		{

		}

		public CandidFuncType(
			List<FuncMode> modes,
			List<(CandidId? Name, CandidType Type)> argTypes,
			List<(CandidId? Name, CandidType Type)> returnTypes,
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
				.SelectMany(a => a.Type.Encode(compoundTypeTable));

			byte[] returnsCount = LEB128.EncodeSigned(this.ReturnTypes.Count);

			IEnumerable<byte> returnTypes = this.ReturnTypes
				.SelectMany(a => a.Type.Encode(compoundTypeTable));

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

		internal override IEnumerable<CandidType> GetInnerTypes()
		{
			return this.ArgTypes
				.Select(t => t.Type)
				.Concat(this.ReturnTypes.Select(t => t.Type));
		}

		public override bool Equals(object? obj)
		{
			if (obj is CandidFuncType sDef)
			{
				bool modesEqual = this.Modes
					.OrderBy(s => s)
					.SequenceEqual(sDef.Modes.OrderBy(s => s));
				bool argTypesEqual = this.ArgTypes.SequenceEqual(sDef.ArgTypes);
				bool returnTypesEqual = this.ReturnTypes.SequenceEqual(sDef.ReturnTypes);
				return modesEqual
					&& argTypesEqual
					&& returnTypesEqual;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Modes, this.ArgTypes, this.ReturnTypes);
		}
	}
}
