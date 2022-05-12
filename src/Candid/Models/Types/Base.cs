using ICP.Candid.Encodings;
using ICP.Candid.Exceptions;
using ICP.Candid.Models;
using ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
    public abstract class CandidTypeDefinition : IEquatable<CandidTypeDefinition>
    {
        public abstract CandidTypeCode Type { get; }

        public abstract override bool Equals(object? obj);

        public abstract override int GetHashCode();
        public override string ToString()
        {
            return CandidTextGenerator.Generate(this);
        }

        public abstract byte[] Encode(CompoundTypeTable compoundTypeTable);

        public bool Equals(CandidTypeDefinition? other)
        {
            return this.Equals(other as object);
        }

        public static bool operator ==(CandidTypeDefinition? def1, CandidTypeDefinition? def2)
        {
            if (object.ReferenceEquals(def1, null))
            {
                return object.ReferenceEquals(def2, null);
            }
            return def1.Equals(def2);
        }

        public static bool operator !=(CandidTypeDefinition? def1, CandidTypeDefinition? def2)
        {
            if (object.ReferenceEquals(def1, null))
            {
                return !object.ReferenceEquals(def2, null);
            }
            return !def1.Equals(def2);
        }

    }
}