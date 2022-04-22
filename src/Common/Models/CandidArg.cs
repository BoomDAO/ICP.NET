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
    public class CandidArg : IHashable
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
            var arg = CandidReader.ReadArg(value);
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
    }
}