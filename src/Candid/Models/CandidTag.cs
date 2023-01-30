using System;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A model representing a candid tag which is a positive number id with an optional name
	/// </summary>
	public class CandidTag : IComparable<CandidTag>, IComparable, IEquatable<CandidTag>
	{
		/// <summary>
		/// Optional. The name/label of the tag. If set, the `Id` is a hash of the specified name
		/// </summary>
		public string? Name { get; }

		/// <summary>
		/// A positive integer value that is either an index, a hash of a string name or arbitrary
		/// </summary>
		public uint Id { get; }

		private CandidTag(uint id, string? name)
		{
			this.Id = id;
			this.Name = name;
		}

		/// <param name="id">A positive integer value that is either an index, a hash of a string name or arbitrary</param>
		public CandidTag(uint id) : this(id, null)
		{

		}

		/// <inheritdoc />
		public bool Equals(CandidTag? other)
		{
			return this.CompareTo(other) == 0;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			return this.Equals(obj as CandidTag);
		}

		/// <inheritdoc />
		public int CompareTo(object? obj)
		{
			return this.CompareTo(obj as CandidTag);
		}

		/// <inheritdoc />
		public int CompareTo(CandidTag? other)
		{
			if (ReferenceEquals(other, null))
			{
				return 1;
			}
			return this.Id.CompareTo(other.Id);
		}

		/// <inheritdoc />
		public static bool operator ==(CandidTag? l1, CandidTag? l2)
		{
			if (ReferenceEquals(l1, null))
			{
				return ReferenceEquals(l2, null);
			}
			return l1.Equals(l2);
		}

		/// <inheritdoc />
		public static bool operator !=(CandidTag? l1, CandidTag? l2)
		{
			if (ReferenceEquals(l1, null))
			{
				return !ReferenceEquals(l2, null);
			}
			return !l1.Equals(l2);
		}

		/// <inheritdoc />
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

		/// <summary>
		/// Helper method to create a tag from a name. Will calculate the id by hashing the name
		/// </summary>
		/// <param name="name">The name of the tag</param>
		/// <returns>A candid tag</returns>
		public static CandidTag FromName(string name)
		{
			uint id = HashName(name);

			return new CandidTag(id, name);
		}

		/// <summary>
		/// Helper method to create a tag from an id. No name will be set
		/// </summary>
		/// <param name="id">The id of the tag</param>
		/// <returns>A candid tag</returns>
		public static CandidTag FromId(uint id)
		{
			return new CandidTag(id, null);
		}

		/// <summary>
		/// Converts a string value to a candid tag. Will calculate the id based off a hash of the name
		/// </summary>
		/// <param name="name">A string value of the name</param>
		public static implicit operator CandidTag(string name)
		{
			return FromName(name);
		}

		/// <summary>
		/// Converts a uint value into a candid tag. Will only set the id; name will not be set
		/// </summary>
		/// <param name="id"></param>
		public static implicit operator CandidTag(uint id)
		{
			return FromId(id);
		}

		/// <summary>
		/// Converts a candid tag value to a uint by using the id of the tag
		/// </summary>
		/// <param name="tag">The candid tag value</param>
		public static implicit operator uint(CandidTag tag)
		{
			return tag.Id;
		}

		/// <inheritdoc />
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
