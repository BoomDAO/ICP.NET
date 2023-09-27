using EdjCase.ICP.Candid.Encodings;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// All the possible options for function modes which
	/// define special attributes of the function
	/// </summary>
	public enum FuncMode
	{
		/// <summary>
		/// Mode where the function does not generate a response to a request
		/// </summary>
		Oneway = 2,
		/// <summary>
		/// Mode where the function does not update any state and returns immediately.
		/// Is useful for faster data retrieval
		/// </summary>
		Query = 1,
		/// <summary>
		/// Mode like Query, but can call other query methods in it
		/// </summary>
		CompositeQuery = 3
	}

	/// <summary>
	/// A candid type model that defines a func
	/// </summary>
	public class CandidFuncType : CandidCompoundType
	{
		/// <inheritdoc />
		public override CandidTypeCode Type { get; } = CandidTypeCode.Func;

		/// <summary>
		/// A set of different modes the function supports
		/// </summary>
		public List<FuncMode> Modes { get; }

		/// <summary>
		/// A list of all the argument types the function will need in a request.
		/// The name is optional and is only used for ease of use, the index/position
		/// of the argument is all that really matters
		/// </summary>
		public List<(CandidId? Name, CandidType Type)> ArgTypes { get; }

		/// <summary>
		/// A list of all the return types the function will supply in respose to a request.
		/// The name is optional and is only used for ease of use, the index/position
		/// of the argument is all that really matters
		/// </summary>
		public List<(CandidId? Name, CandidType Type)> ReturnTypes { get; }

		/// <param name="modes">A set of different modes the function supports</param>
		/// <param name="argTypes">A list of all the argument types the function will need in a request.
		/// The name is optional and is only used for ease of use, the index/position
		/// of the argument is all that really matters</param>
		/// <param name="returnTypes">A list of all the return types the function will supply in respose to a request.
		/// The name is optional and is only used for ease of use, the index/position
		/// of the argument is all that really matters</param>
		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
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

		/// <param name="modes">A set of different modes the function supports</param>
		/// <param name="argTypes">A list of all the argument types the function will need in a request.
		/// The name is optional and is only used for ease of use, the index/position
		/// of the argument is all that really matters</param>
		/// <param name="returnTypes">A list of all the return types the function will supply in respose to a request.
		/// The name is optional and is only used for ease of use, the index/position
		/// of the argument is all that really matters</param>
		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
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

		internal override void EncodeInnerTypes(CompoundTypeTable compoundTypeTable, IBufferWriter<byte> destination)
		{
			// Arg types
			LEB128.EncodeSigned(this.ArgTypes.Count, destination); // Encode arg count
			
			foreach(var argType in this.ArgTypes)
			{
				argType.Type.Encode(compoundTypeTable, destination); // Encode arg types
			}

			// Return types
			LEB128.EncodeSigned(this.ReturnTypes.Count, destination); // Encode return type count

			foreach (var returnType in this.ReturnTypes)
			{
				returnType.Type.Encode(compoundTypeTable, destination); // Encode return types
			}

			// Modes
			LEB128.EncodeSigned(this.Modes.Count,destination); // Encode mode count

			foreach(FuncMode mode in this.Modes)
			{
				LEB128.EncodeSigned((UnboundedInt)(long)mode, destination); // Encode modes
			}
		}

		internal override IEnumerable<CandidType> GetInnerTypes()
		{
			return this.ArgTypes
				.Select(t => t.Type)
				.Concat(this.ReturnTypes.Select(t => t.Type));
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Modes, this.ArgTypes, this.ReturnTypes);
		}
	}
}
