using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	/// <summary>
	/// A model representing a candid record
	/// </summary>
	public class CandidRecord : CandidValue
	{
		/// <inheritdoc />
		public override CandidValueType Type { get; } = CandidValueType.Record;

		/// <summary>
		/// The mapping of field name to field value for the record
		/// </summary>
		public Dictionary<CandidTag, CandidValue> Fields { get; }

		/// <param name="fields">The mapping of field name to field value for the record</param>
		public CandidRecord(Dictionary<CandidTag, CandidValue> fields)
		{
			this.Fields = fields;
		}

		/// <summary>
		/// Gets the candid value of the field with the specified name
		/// </summary>
		/// <param name="name">Name of the field value to get, case sensitive</param>
		/// <exception cref="KeyNotFoundException">Throws if field name is not found</exception>
		/// <returns>The field value</returns>
		public CandidValue this[string name]
		{
			get
			{
				return this.Fields[CandidTag.FromName(name)];
			}
		}

		/// <summary>
		/// Gets the candid value of the field with the specified id (name hash)
		/// </summary>
		/// <param name="id">Id (name hash) of the field value to get</param>
		/// <exception cref="KeyNotFoundException">Throws if field id is not found</exception>
		/// <returns>The field value</returns>
		public CandidValue this[uint id]
		{
			get
			{
				return this.Fields[CandidTag.FromId(id)];
			}
		}

		/// <summary>
		/// Gets the candid value of the field with the specified tag
		/// </summary>
		/// <param name="tag">Tag of the field value to get</param>
		/// <exception cref="KeyNotFoundException">Throws if field tag is not found</exception>
		/// <returns>The field value</returns>
		public CandidValue this[CandidTag tag]
		{
			get
			{
				return this.Fields[tag];
			}
		}

		/// <summary>
		/// Tries to get the field based on the specified name. If the field does not exist, will return false,
		/// otherwise true. The out value will only be set if returns true, otherwise value will be null
		/// </summary>
		/// <param name="name">Name of the field value to get, case sensitive</param>
		/// <param name="value">Out value that is set only if the method returns true</param>
		/// <returns>True if field exists, otherwise false</returns>
		public bool TryGetField(string name,
			[NotNullWhen(true)]
			out CandidValue? value)
		{
			CandidTag hashedName = CandidTag.FromName(name);
			return this.TryGetField(hashedName, out value);
		}

		/// <summary>
		/// Tries to get the field based on the specified id (name hash). If the field does not exist, will return false,
		/// otherwise true. The out value will only be set if returns true, otherwise value will be null
		/// </summary>
		/// <param name="id">Id (name hash) of the field value to get</param>
		/// <param name="value">Out value that is set only if the method returns true</param>
		/// <returns>True if field exists, otherwise false</returns>
		public bool TryGetField(uint id,
			[NotNullWhen(true)]
			out CandidValue? value)
		{
			CandidTag hashedName = CandidTag.FromId(id);
			return this.TryGetField(hashedName, out value);
		}

		/// <summary>
		/// Tries to get the field based on the specified tag. If the field does not exist, will return false,
		/// otherwise true. The out value will only be set if returns true, otherwise value will be null
		/// </summary>
		/// <param name="tag">Tag of the field value to get</param>
		/// <param name="value">Out value that is set only if the method returns true</param>
		/// <returns>True if field exists, otherwise false</returns>
		public bool TryGetField(CandidTag tag,
			[NotNullWhen(true)]
			out CandidValue? value)
		{
			return this.Fields.TryGetValue(tag, out value);
		}

		/// <summary>
		/// Helper method to create a record value from a dictionary of field names to values
		/// </summary>
		/// <param name="fields">Dictionary of field names to values for the record</param>
		/// <returns>A candid record from the fields specified</returns>
		public static CandidRecord FromDictionary(Dictionary<string, CandidValue> fields)
		{
			Dictionary<CandidTag, CandidValue> hashedFields = fields
				.ToDictionary(d => CandidTag.FromName(d.Key), d => d.Value);

			return new CandidRecord(hashedFields);
		}

		/// <summary>
		/// Helper method to create a record value from a dictionary of field ids (name hashes) to values
		/// </summary>
		/// <param name="fields">Dictionary of ids (name hashes) to values for the record</param>
		/// <returns>A candid record from the fields specified</returns>
		public static CandidRecord FromDictionary(Dictionary<uint, CandidValue> fields)
		{
			Dictionary<CandidTag, CandidValue> hashedFields = fields
				.ToDictionary(d => CandidTag.FromId(d.Key), d => d.Value);

			return new CandidRecord(hashedFields);
		}

		/// <summary>
		/// Helper method to create a record value from a dictionary of field tags to values
		/// </summary>
		/// <param name="fields">Dictionary of tags to values for the record</param>
		/// <returns>A candid record from the fields specified</returns>
		public static CandidRecord FromDictionary(Dictionary<CandidTag, CandidValue> fields)
		{
			Dictionary<CandidTag, CandidValue> hashedFields = fields
				.ToDictionary(d => d.Key, d => d.Value);

			return new CandidRecord(hashedFields);
		}

		/// <inheritdoc />
		internal override void EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType, IBufferWriter<byte> destination)
		{
			CandidRecordType t = DereferenceType<CandidRecordType>(type, getReferencedType);
			// bytes = ordered keys by hash hashes added together
			foreach(var f in t.Fields.OrderBy(l => l.Key))
			{
				this.Fields[f.Key].EncodeValue(f.Value, getReferencedType, destination); // Encode value
			}
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Fields);
		}

		/// <inheritdoc />
		public override bool Equals(CandidValue? other)
		{
			if (other is CandidRecord r)
			{
				return this.GetOrderedFields(this)
					.SequenceEqual(this.GetOrderedFields(r));
			}
			return false;
		}

		private IEnumerable<(uint, CandidValue)> GetOrderedFields(CandidRecord candidRecord)
		{
			return candidRecord.Fields
					   .Select(f => (f.Key.Id, f.Value))
					   .OrderBy(f => f.Id);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			IEnumerable<string> fields = this.Fields.Select(f => $"{f.Key}:{f.Value}");
			return $"{{{string.Join("; ", fields)}}}";
		}
	}

}
