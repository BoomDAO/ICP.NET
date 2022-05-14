using ICP.Candid.Encodings;
using ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models
{
    public class CompoundTypeTable
    {

        /// <summary>
        /// A mapping of compound type definition to `EncodedCompoundTypes` index to be used as reference
        /// </summary>
        private readonly Dictionary<CompoundCandidTypeDefinition, uint> CompoundTypeIndexMap = new Dictionary<CompoundCandidTypeDefinition, uint>();

        public UnboundedUInt GetOrAdd(CompoundCandidTypeDefinition typeDef)
        {
            foreach (CandidTypeDefinition innerType in typeDef.GetInnerTypes())
            {
                // Recursively go through and add the inner types before adding the parent type
                if (innerType is CompoundCandidTypeDefinition innerCompoundType)
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

        private byte[] EncodeFunc(CompoundCandidTypeDefinition typeDef)
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

        public static CompoundTypeTable FromTypes(List<CompoundCandidTypeDefinition> types)
        {
            var table = new CompoundTypeTable();
            foreach (CompoundCandidTypeDefinition? type in types)
            {
                table.GetOrAdd(type);
            }
            return table;
        }

        public uint GetRecursiveReferenceIndex(CandidId recursiveId)
        {
            uint? index = this.CompoundTypeIndexMap
                .Where(c => c.Key.RecursiveId == recursiveId)
                .Select(c => (uint?)c.Value)
                .SingleOrDefault();
            if (index == null)
            {
                throw new Exception($"Unable to find type with recursive id '{recursiveId}'");
            }
            return index.Value;
        }
    }
}
