using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using ICP.Candid.Encodings;
using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;

namespace ICP.Candid
{
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


        public void Add(CandidValueWithType value)
        {
            byte[] encodedType = value.Type.Encode(this.compoundTypeTable);
            this.EncodedTypes.Add(encodedType);
            byte[] encodedValue = value.Value.EncodeValue();
            this.EncodedValues.Add(encodedValue);
        }

        public static IDLBuilder FromArgs(IEnumerable<CandidValueWithType> values)
        {
            var builder = new IDLBuilder();
            foreach (CandidValueWithType vwt in values)
            {
                builder.Add(vwt);
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
