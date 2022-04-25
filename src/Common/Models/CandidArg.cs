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

        public CandidArg(List<(CandidValue, CandidTypeDefinition)> value)
        {
            this.Value = value;
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
            var arg = CandidReader.Read(value);
            return new CandidArg(arg);
        }

        public static CandidArg FromCandid(List<(CandidValue, CandidTypeDefinition)> args)
        {
            return new CandidArg(args);
        }

        public static CandidArg FromCandid(params (CandidValue, CandidTypeDefinition)[] args)
        {
            return new CandidArg(args.ToList());
        }

        public override string ToString()
        {
            IEnumerable<string> args = this.Value.Select(v => v.Item2.ToString()!);
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