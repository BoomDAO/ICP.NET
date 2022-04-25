using Common.Models;
using ICP.Common.Models;
using System;

namespace ICP.Common.Candid
{
    public enum CandidValueType
    {
        Primitive,
        Vector,
        Record,
        Variant,
        Func,
        Service,
        Optional,
        Principal
    }

    public abstract class CandidValue : IEquatable<CandidValue>
    {
        public abstract CandidValueType Type { get; }

        public abstract byte[] EncodeValue();
        public abstract override int GetHashCode();
        public abstract bool Equals(CandidValue? other);


        public override bool Equals(object? obj)
        {
            if (obj is CandidValue v)
            {
                return this.Equals(v);
            }
            return false;
        }

        public static bool operator ==(CandidValue v1, CandidValue v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(CandidValue v1, CandidValue v2)
        {
            return !v1.Equals(v2);
        }

        public CandidPrimitive AsPrimitive()
        {
            this.ValidateType(CandidValueType.Primitive);
            return (CandidPrimitive)this;
        }

        public CandidVector AsVector()
        {
            this.ValidateType(CandidValueType.Vector);
            return (CandidVector)this;
        }

        public CandidRecord AsRecord()
        {
            this.ValidateType(CandidValueType.Record);
            return (CandidRecord)this;
        }

        public CandidVariant AsVariant()
        {
            this.ValidateType(CandidValueType.Variant);
            return (CandidVariant)this;
        }

        public CandidFunc AsFunc()
        {
            this.ValidateType(CandidValueType.Func);
            return (CandidFunc)this;
        }

        public CandidService AsService()
        {
            this.ValidateType(CandidValueType.Service);
            return (CandidService)this;
        }

        public CandidOptional AsOptional()
        {
            this.ValidateType(CandidValueType.Optional);
            return (CandidOptional)this;
        }

        protected void ValidateType(CandidValueType type)
        {
            if (this.Type != type)
            {
                throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
            }
        }
    }
}
