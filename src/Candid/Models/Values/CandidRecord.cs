using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ICP.Candid.Models.Values
{
	public class CandidRecord : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Record;

		public Dictionary<CandidLabel, CandidValue> Fields { get; }

		public CandidRecord(Dictionary<CandidLabel, CandidValue> fields)
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
				return this.Fields[CandidLabel.FromId((UnboundedUInt)id)];
			}
		}
		public CandidValue this[CandidLabel tag]
		{
			get
			{
				return this.Fields[tag];
			}
		}

		public bool TryGetField(string name, [NotNullWhen(true)] out CandidValue? value)
		{
			CandidLabel hashedName = CandidLabel.FromName(name);
			return this.TryGetField(hashedName, out value);
		}

		public bool TryGetField(CandidLabel label, [NotNullWhen(true)] out CandidValue? value)
		{
			return this.Fields.TryGetValue(label, out value);
		}

		public static CandidRecord FromDictionary(Dictionary<string, CandidValue> dict)
		{
			Dictionary<CandidLabel, CandidValue> hashedDict = dict
				.ToDictionary(d => CandidLabel.FromName(d.Key), d => d.Value);

			return new CandidRecord(hashedDict);
		}

		public static CandidRecord FromDictionary(Dictionary<CandidLabel, CandidValue> dict)
		{
			Dictionary<CandidLabel, CandidValue> hashedDict = dict
				.ToDictionary(d => d.Key, d => d.Value);

			return new CandidRecord(hashedDict);
		}

		public override byte[] EncodeValue()
		{
			// bytes = ordered keys by hash hashes added together
			return this.Fields
				.OrderBy(l => l.Key)
				.SelectMany(v => v.Value.EncodeValue())
				.ToArray();
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Fields);
		}

		public override bool Equals(CandidValue? other)
		{
			if (other is CandidRecord r)
			{
				return this.GetOrderedFields(this)
					.SequenceEqual(this.GetOrderedFields(r));
			}
			return false;
		}

        private IEnumerable<(UnboundedUInt, CandidValue)> GetOrderedFields(CandidRecord candidRecord)
        {
			return candidRecord.Fields
					   .Select(f => (f.Key.Id, f.Value))
					   .OrderBy(f => f.Id);
        }

        public override string ToString()
        {
			IEnumerable<string> fields = this.Fields.Select(f => $"{f.Key}:{f.Value}");
			return $"{{{string.Join("; ", fields)}}}";
        }
    }

}
