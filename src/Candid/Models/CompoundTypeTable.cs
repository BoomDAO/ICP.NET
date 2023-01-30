using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models
{
	internal class CompoundTypeTable
	{

		/// <summary>
		/// A mapping of compound type definition to `EncodedCompoundTypes` index to be used as reference
		/// </summary>
		private readonly Dictionary<CandidCompoundType, uint> CompoundTypeIndexMap = new Dictionary<CandidCompoundType, uint>();

		public UnboundedUInt GetOrAdd(CandidCompoundType typeDef)
		{
			foreach (CandidType innerType in typeDef.GetInnerTypes())
			{
				// Recursively go through and add the inner types before adding the parent type
				if (innerType is CandidCompoundType innerCompoundType)
				{
					this.GetOrAdd(innerCompoundType);
				}
			}
			if (!this.CompoundTypeIndexMap.TryGetValue(typeDef, out uint index))
			{
				index = (uint)this.CompoundTypeIndexMap.Count;
				this.CompoundTypeIndexMap.Add(typeDef, index);
			}
			return index;
		}

		private void EncodeFunc(CandidCompoundType typeDef, IBufferWriter<byte> destination)
		{
			LEB128.EncodeSigned((UnboundedInt)(long)typeDef.Type, destination);
			typeDef.EncodeInnerTypes(this, destination);
		}

		public void Encode(IBufferWriter<byte> destination)
		{
			LEB128.EncodeUnsigned((UnboundedUInt)this.CompoundTypeIndexMap.Count, destination);

			foreach(var t in this.CompoundTypeIndexMap.OrderBy(x => x.Value))
			{
				this.EncodeFunc(t.Key, destination);
			}
		}

		public static CompoundTypeTable FromTypes(List<CandidCompoundType> types)
		{
			var table = new CompoundTypeTable();
			foreach (CandidCompoundType? type in types)
			{
				table.GetOrAdd(type);
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
