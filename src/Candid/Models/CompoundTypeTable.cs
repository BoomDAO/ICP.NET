using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Types;
using System;
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

		private byte[] EncodeFunc(CandidCompoundType typeDef)
		{
			byte[] encodedInnerValue = typeDef.EncodeInnerTypes(this);
			return LEB128.EncodeSigned((UnboundedInt)(long)typeDef.Type)
				.Concat(encodedInnerValue)
				.ToArray();
		}

		public IEnumerable<byte> Encode()
		{
			byte[] compoundTypesCount = LEB128.EncodeUnsigned((UnboundedUInt)this.CompoundTypeIndexMap.Count);
			IEnumerable<byte> encodedTypes = this.CompoundTypeIndexMap
				.OrderBy(x => x.Value)
				.SelectMany(t => this.EncodeFunc(t.Key));
			return compoundTypesCount
				.Concat(encodedTypes);
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
