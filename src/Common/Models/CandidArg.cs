using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Candid;
using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Crypto;
using ICP.Common.Encodings;

namespace ICP.Common.Models
{
    public class CandidArg : IHashable, IEquatable<CandidArg>
    {
        public List<(CandidValue, CandidTypeDefinition)> Value { get; }
        public byte[] OpaqueReferenceBytes { get; }

        public CandidArg(List<(CandidValue, CandidTypeDefinition)> value, byte[]? opaqueReferencesBytes = null)
        {
            this.Value = value;
            this.OpaqueReferenceBytes = opaqueReferencesBytes ?? new byte[0];
        }

        public byte[] ComputeHash(IHashFunction hashFunction)
        {
            return hashFunction.ComputeHash(this.Encode());
        }

        public byte[] Encode()
        {
            return IDLBuilder.FromArgs(this.Value).Encode();
        }

        public static CandidArg FromBytes(byte[] value)
        {
            return CandidReader.Read(value);
        }

        public static CandidArg FromCandid(List<(CandidValue, CandidTypeDefinition)> args, byte[]? opaqueReferencesBytes = null)
        {
            return new CandidArg(args, opaqueReferencesBytes);
        }

        public static CandidArg FromCandid(params (CandidValue, CandidTypeDefinition)[] args)
        {
            return new CandidArg(args.ToList(), null);
        }

        public override string ToString()
        {
            IEnumerable<string> args = this.Value.Select(v => v.Item1.ToString()!);
            return $"({string.Join(",", args)})";
        }

        public bool Equals(CandidArg? other)
        {
            if(other == null)
            {
                return false;
            }
            return this.Value.SequenceEqual(other.Value);
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as CandidArg);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Value.Select(v => v.GetHashCode()));
        }
    }
}