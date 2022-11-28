using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;

namespace EdjCase.ICP.Candid
{
    public class CandidArgBuilder
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
            byte[] encodedValue = value.Value.EncodeValue(value.Type);
            this.EncodedValues.Add(encodedValue);
        }

        public static CandidArgBuilder FromArgs(IEnumerable<CandidValueWithType> values)
        {
            var builder = new CandidArgBuilder();
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
