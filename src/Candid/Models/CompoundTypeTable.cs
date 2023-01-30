using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EdjCase.ICP.Candid.Models
{
	internal class CompoundTypeTable
	{

		/// <summary>
		/// A mapping of compound type definition to `EncodedCompoundTypes` index to be used as reference
		/// </summary>
		private readonly Dictionary<CandidCompoundType, uint> CompoundTypeIndexMap = new();

		public UnboundedUInt GetOrThrow(CandidCompoundType typeDef)
		{
			if (!this.TryGet(typeDef, out UnboundedUInt index))
			{
				throw new InvalidOperationException("Compound type not found in the type table");
			}
			return index;
		}

		public bool TryGet(CandidCompoundType typeDef, out UnboundedUInt index)
		{
			bool found = this.CompoundTypeIndexMap.TryGetValue(typeDef, out uint i);
			index = i;
			return found;
		}

		/// <summary>
		/// Adds type to the table unless the type already exists
		/// </summary>
		/// <param name="typeDef">Type to add to table</param>
		/// <returns>True if added, otherwise false</returns>
		public bool TryAdd(CandidCompoundType typeDef)
		{
			if (this.TryGet(typeDef, out _))
			{
				return false; // already added
			}
			foreach (CandidType innerType in typeDef.GetInnerTypes())
			{
				// Recursively go through and add the inner types before adding the parent type
				if (innerType is CandidCompoundType innerCompoundType)
				{
					this.TryAdd(innerCompoundType);
				}
			}
			uint i = (uint)this.CompoundTypeIndexMap.Count;
			this.CompoundTypeIndexMap.Add(typeDef, i);
			return false;
		}

		private void EncodeFunc(CandidCompoundType typeDef, IBufferWriter<byte> destination)
		{
			LEB128.EncodeSigned((UnboundedInt)(long)typeDef.Type, destination);
			typeDef.EncodeInnerTypes(this, destination);
		}

		public void Encode(IBufferWriter<byte> destination)
		{
			LEB128.EncodeUnsigned((UnboundedUInt)this.CompoundTypeIndexMap.Count, destination);

			foreach (var t in this.CompoundTypeIndexMap.OrderBy(x => x.Value))
			{
				this.EncodeFunc(t.Key, destination);
			}
		}

		public static CompoundTypeTable FromTypes(List<CandidTypedValue> types)
		{
			var table = new CompoundTypeTable();
			foreach (CandidTypedValue type in types)
			{
				if (type.Type is CandidCompoundType c)
				{
					table.TryAdd(c);
				}
			}
			return table;
		}

		public (CandidCompoundType Type, uint Index) GetReferenceById(CandidId referenceId)
		{
			(CandidCompoundType Type, uint Index)? reference = this.CompoundTypeIndexMap
				.Where(c => c.Key.RecursiveId == referenceId)
				.Select(c => (c.Key, c.Value))
				.SingleOrDefault();
			if (reference == null)
			{
				throw new Exception($"Unable to find type with reference id '{referenceId}'");
			}
			return reference.Value;
		}
	}
}
