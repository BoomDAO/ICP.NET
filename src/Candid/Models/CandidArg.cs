using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Parsers;

namespace EdjCase.ICP.Candid.Models
{
    public class CandidArg : IHashable, IEquatable<CandidArg>
    {
        public List<CandidValueWithType> Values { get; }
        public byte[] OpaqueReferenceBytes { get; }

        public CandidArg(List<CandidValueWithType> values, byte[]? opaqueReferencesBytes = null)
        {
            this.Values = values;
            this.OpaqueReferenceBytes = opaqueReferencesBytes ?? new byte[0];
        }

        public byte[] ComputeHash(IHashFunction hashFunction)
        {
            return hashFunction.ComputeHash(this.Encode());
        }

        public byte[] Encode()
        {
            return CandidArgBuilder.FromArgs(this.Values).Encode();
        }

        public static CandidArg FromBytes(byte[] value)
        {
            return CandidByteParser.Parse(value);
        }

        public static CandidArg FromCandid(List<CandidValueWithType> args, byte[]? opaqueReferencesBytes = null)
        {
            return new CandidArg(args, opaqueReferencesBytes);
        }

        public static CandidArg FromCandid(params CandidValueWithType[] args)
        {
            return new CandidArg(args.ToList(), null);
        }

        public override string ToString()
        {
            IEnumerable<string> args = this.Values.Select(v => v.Value.ToString()!);
            return $"({string.Join(",", args)})";
        }

        public bool Equals(CandidArg? other)
        {
            if(object.ReferenceEquals(other, null))
            {
                return false;
            }
            return this.Values.SequenceEqual(other.Values);
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as CandidArg);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Values.Select(v => v.GetHashCode()));
        }

        public static bool operator ==(CandidArg? v1, CandidArg? v2)
        {
            if (object.ReferenceEquals(v1, null))
            {
                return object.ReferenceEquals(v2, null);
            }
            return v1.Equals(v2);
        }

        public static bool operator !=(CandidArg? v1, CandidArg? v2)
        {
            if (object.ReferenceEquals(v1, null))
            {
                return object.ReferenceEquals(v2, null);
            }
            return !v1.Equals(v2);
        }
    }
}