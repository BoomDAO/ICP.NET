using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ICP.Common.Candid
{
	public class CandidRecord : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Record;

		private readonly Dictionary<uint, CandidTokenInfo> fields;

		private CandidRecord(Dictionary<uint, CandidTokenInfo> fields)
		{
			this.fields = fields;
		}

		public bool TryGetField(string name, [NotNullWhen(true)] out CandidToken? value)
		{
			uint hashedName = CandidRecord.HashFieldName(name);
			return this.TryGetField(hashedName, out value);
		}

		public bool TryGetField(uint nameHash, [NotNullWhen(true)] out CandidToken? value)
		{
			bool exists = this.fields.TryGetValue(nameHash, out CandidTokenInfo? v);
			value = v?.Value;
			return exists;
		}

		private static uint HashFieldName(string value)
		{
			byte[] utf8Bytes = Encoding.UTF8.GetBytes(value);
			double sum = utf8Bytes
				.Select((v, i) => v * Math.Pow(223, utf8Bytes.Length - i))
				.Sum();
			return (uint)(sum % Math.Pow(2, 32));
		}

		public static CandidRecord FromDictionary(Dictionary<string, CandidToken> dict)
		{
			Dictionary<uint, CandidTokenInfo> hashedDict = dict
				.ToDictionary(d => CandidRecord.HashFieldName(d.Key), d => new CandidTokenInfo(d.Key, d.Value));

			return new CandidRecord(hashedDict);
		}

		public static CandidRecord FromDictionary(Dictionary<uint, CandidToken> dict)
		{
			Dictionary<uint, CandidTokenInfo> hashedDict = dict
				.ToDictionary(d => d.Key, d => new CandidTokenInfo(null, d.Value));

			return new CandidRecord(hashedDict);
		}

		public Dictionary<uint, CandidToken> GetFields()
		{
			return this.fields.ToDictionary(f => f.Key, f => f.Value.Value);
		}

		private class CandidTokenInfo
		{
			public string? Name { get; }
			public CandidToken Value { get; }

			public CandidTokenInfo(string? name, CandidToken value)
			{
				this.Name = name;
				this.Value = value;
			}
		}
	}

}
