using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	public class CandidRecord : CandidValue
	{
		/// <inheritdoc />
		public override CandidValueType Type { get; } = CandidValueType.Record;

		public Dictionary<CandidTag, CandidValue> Fields { get; }

		public CandidRecord(Dictionary<CandidTag, CandidValue> fields)
		{
			this.Fields = fields;
		}
		public CandidValue this[string name]
		{
			get
			{
				return this.Fields[name];
			}
		}
		public CandidValue this[int id]
		{
			get
			{
				return this.Fields[CandidTag.FromId((uint)id)];
			}
		}
		public CandidValue this[CandidTag tag]
		{
			get
			{
				return this.Fields[tag];
			}
		}

		public bool TryGetField(string name,
#if !NETSTANDARD2_0
		[NotNullWhen(true)]
#endif
			out CandidValue? value)
		{
			CandidTag hashedName = CandidTag.FromName(name);
			return this.TryGetField(hashedName, out value);
		}

		public bool TryGetField(CandidTag label,
#if !NETSTANDARD2_0
		[NotNullWhen(true)]
#endif
			out CandidValue? value)
		{
			return this.Fields.TryGetValue(label, out value);
		}

		public static CandidRecord FromDictionary(Dictionary<string, CandidValue> dict)
		{
			Dictionary<CandidTag, CandidValue> hashedDict = dict
				.ToDictionary(d => CandidTag.FromName(d.Key), d => d.Value);

			return new CandidRecord(hashedDict);
		}

		public static CandidRecord FromDictionary(Dictionary<CandidTag, CandidValue> dict)
		{
			Dictionary<CandidTag, CandidValue> hashedDict = dict
				.ToDictionary(d => d.Key, d => d.Value);

			return new CandidRecord(hashedDict);
		}

		/// <inheritdoc />
		internal override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			CandidRecordType t;
			if (type is CandidReferenceType r)
			{
				t = (CandidRecordType)getReferencedType(r.Id);
			}
			else
			{
				t = (CandidRecordType)type;
			}
			// bytes = ordered keys by hash hashes added together
			return t.Fields
				.OrderBy(l => l.Key)
				.SelectMany(v => this.Fields[v.Key].EncodeValue(v.Value, getReferencedType))
				.ToArray();
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
