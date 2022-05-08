using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICP.Candid.Crypto;
using ICP.Candid.Encodings;
using ICP.Candid.Models.Values;
using ICP.Candid.Models.Types;

namespace ICP.Candid.Models
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
            return IDLBuilder.FromArgs(this.Values).Encode();
        }

        public static CandidArg FromBytes(byte[] value)
        {
            return CandidReader.Read(value);
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
            if(other == null)
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
    }
}