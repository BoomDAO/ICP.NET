using Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ICP.Common.Candid
{
	public class CandidRecord : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Record;

		public Dictionary<Label, CandidValue> Fields { get; }

		public CandidRecord(Dictionary<Label, CandidValue> fields)
		{
			this.Fields = fields;
		}

		public bool TryGetField(string name, [NotNullWhen(true)] out CandidValue? value)
		{
			Label hashedName = Label.FromName(name);
			return this.TryGetField(hashedName, out value);
		}

		public bool TryGetField(Label label, [NotNullWhen(true)] out CandidValue? value)
		{
			return this.Fields.TryGetValue(label, out value);
		}

		public static CandidRecord FromDictionary(Dictionary<string, CandidValue> dict)
		{
			Dictionary<Label, CandidValue> hashedDict = dict
				.ToDictionary(d => Label.FromName(d.Key), d => d.Value);

			return new CandidRecord(hashedDict);
		}

		public static CandidRecord FromDictionary(Dictionary<Label, CandidValue> dict)
		{
			Dictionary<Label, CandidValue> hashedDict = dict
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
