using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;

namespace ICP.Common.Models
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
            if(!this.CompoundTypeIndexMap.TryGetValue(typeDef, out uint index))
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

        public uint GetRecursiveReferenceIndex(string recursiveId)
        {
            uint? index = this.CompoundTypeIndexMap
                .Where(c => c.Key.RecursiveId == recursiveId)
                .Select(c => (uint?)c.Value)
                .SingleOrDefault();
            if(index == null)
            {
                // TODO
                throw new Exception();
            }
            return index.Value;
        }
    }
    public class IDLBuilder
    {
        /// <summary>
        /// Helper to capture compound types
        /// </summary>
        private readonly CompoundTypeTable compoundTypeTable = new CompoundTypeTable();
        /// <summary>
        /// Ordered list of encoded types (encoded with SLEB128).
        /// If SLEB value is positive, it is an index for `EncodedCompoundTypes` for a compound type
        /// If SLEB value is negative, it is type code for a primitive value
        /// </summary>
        private readonly List<byte[]> EncodedTypes = new List<byte[]>();
        /// <summary>
        /// Ordered list of encoded values
        /// </summary>
        private readonly List<byte[]> EncodedValues = new List<byte[]>();


        public void Add(CandidValue value, CandidTypeDefinition def)
        {
            byte[] encodedType = def.Encode(this.compoundTypeTable);
            this.EncodedTypes.Add(encodedType);
            byte[] encodedValue = value.EncodeValue();
            this.EncodedValues.Add(encodedValue);
        }

        public static IDLBuilder FromArgs(IEnumerable<(CandidValue, CandidTypeDefinition)> values)
        {
            var builder = new IDLBuilder();
            foreach ((CandidValue value, CandidTypeDefinition def) in values)
            {
                builder.Add(value, def);
            }
            return builder;
        }

        public byte[] Encode()
        {
            byte[] encodedPrefix = Encoding.UTF8.GetBytes("DIDL");

            IEnumerable<byte> encodedTypes = this.GenerateTypeEncoding();
            IEnumerable<byte> encodedValues = this.GenerateValueEncoding();
            return encodedPrefix
                .Concat(encodedTypes)
                .Concat(encodedValues)
                .ToArray();
        }

        private IEnumerable<byte> GenerateTypeEncoding()
        {
            byte[] encodedTypesCount = LEB128.EncodeSigned(this.EncodedTypes.Count);

            return this.compoundTypeTable.Encode()
                .Concat(encodedTypesCount)
                .Concat(this.EncodedTypes.SelectMany(t => t));
        }

        private IEnumerable<byte> GenerateValueEncoding()
        {
            return this.EncodedValues.SelectMany(v => v);
        }
    }
}
