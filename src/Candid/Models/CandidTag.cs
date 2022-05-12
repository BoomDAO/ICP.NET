using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models
{
    public class CandidTag : IComparable<CandidTag>, IComparable, IEquatable<CandidTag>
    {
        public string? Name { get; }
        public UnboundedUInt Id { get; }

        private CandidTag(UnboundedUInt id, string? name)
        {
            this.Id = id;
            this.Name = name;
        }

        public CandidTag(UnboundedUInt id) : this(id, null)
        {

        }


        public bool Equals(CandidTag? other)
        {
            return this.CompareTo(other) == 0;
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as CandidTag);
        }

        public int CompareTo(object? obj)
        {
            return this.CompareTo(obj as CandidTag);
        }

        public int CompareTo(CandidTag? other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return this.Id.CompareTo(other.Id);
        }
        public static bool operator ==(CandidTag? l1, CandidTag? l2)
        {
            if (object.ReferenceEquals(l1, null))
            {
                return object.ReferenceEquals(l2, null);
            }
            return l1.Equals(l2);
        }

        public static bool operator !=(CandidTag? l1, CandidTag? l2)
        {
            if (object.ReferenceEquals(l1, null))
            {
                return !object.ReferenceEquals(l2, null);
            }
            return !l1.Equals(l2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }


        /// <summary>
        /// Hashes the name to get the proper id 
        /// hash(name) = ( Sum_(i=0..k) utf8(name)[i] * 223^(k-i) ) mod 2^32 where k = |utf8(name)|-1
        /// </summary>
        /// <param name="name">Name to hash</param>
        /// <returns>Unsigned 32 byte integer hash</returns>
        public static uint HashName(string name)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(name);
            uint digest = 0;
            foreach (byte b in bytes)
            {
                digest = (digest * 223) + b;
            }
            return digest;
        }

        public static CandidTag FromName(string name)
        {
            uint id = CandidTag.HashName(name);

            return new CandidTag(id, name);
        }

        public static CandidTag FromId(UnboundedUInt id)
        {
            return new CandidTag(id, null);
        }

        public static implicit operator CandidTag(string value)
        {
            return CandidTag.FromName(value);
        }

        public static implicit operator CandidTag(UnboundedUInt value)
        {
            return CandidTag.FromId(value);
        }

        public static implicit operator UnboundedUInt(CandidTag value)
        {
            return value.Id;
        }

        public override string ToString()
        {
            string value = this.Id.ToString();
            if (this.Name != null)
            {
                value = $"{this.Name}({value})";
            }
            return value;
        }
    }
}
