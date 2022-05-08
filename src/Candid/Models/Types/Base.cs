using ICP.Candid.Encodings;
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
		public abstract IDLTypeCode Type { get; }

		public abstract override bool Equals(object? obj);

		public abstract override int GetHashCode();
		public abstract override string ToString();

		public abstract byte[] Encode(CompoundTypeTable compoundTypeTable);

        public bool Equals(CandidTypeDefinition? other)
        {
			return this.Equals(other as object);
		}

		public static bool operator ==(CandidTypeDefinition? def1, CandidTypeDefinition? def2)
		{
			if(object.ReferenceEquals(def1, null))
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

	public class CandidLabel : IComparable<CandidLabel>, IComparable, IEquatable<CandidLabel>
	{
		public string? Name { get; }
		public UnboundedUInt Id { get; }

		private CandidLabel(UnboundedUInt id, string? name)
		{
			this.Id = id;
			this.Name = name;
		}

		public CandidLabel(UnboundedUInt id) : this(id, null)
		{

		}


		public bool Equals(CandidLabel? other)
		{
			return this.CompareTo(other) == 0;
		}

		public override bool Equals(object? obj)
		{
			return this.Equals(obj as CandidLabel);
		}

		public int CompareTo(object? obj)
		{
			return this.CompareTo(obj as CandidLabel);
		}

		public int CompareTo(CandidLabel? other)
		{
			if(other == null)
            {
				return 1;
            }
			return this.Id.CompareTo(other.Id);
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
			foreach(byte b in bytes)
            {
				digest = (digest * 223) + b;
            }
			return digest;
		}

		public static CandidLabel FromName(string name)
		{
			uint id = CandidLabel.HashName(name);

			return new CandidLabel(id, name);
		}

		public static CandidLabel FromId(UnboundedUInt id)
		{
			return new CandidLabel(id, null);
		}

		public static implicit operator CandidLabel(string value)
		{
			return CandidLabel.FromName(value);
		}

		public static implicit operator CandidLabel(UnboundedUInt value)
		{
			return CandidLabel.FromId(value);
		}

		public static implicit operator UnboundedUInt(CandidLabel value)
		{
			return value.Id;
		}

        public override string ToString()
        {
			string value = this.Id.ToString();
			if(this.Name != null)
            {
				value += $"({this.Name})";
            }
			return value;
        }
    }
}