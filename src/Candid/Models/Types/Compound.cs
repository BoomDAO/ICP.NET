using ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public abstract class CandidCompoundType : CandidKnownType
	{
		public CandidId? RecursiveId { get; set; }

		public CandidCompoundType(CandidId? recursiveId)
		{
			this.RecursiveId = recursiveId;
		}

		internal abstract byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable);

		internal abstract IEnumerable<CandidType> GetInnerTypes();

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			UnboundedUInt index = compoundTypeTable.GetOrAdd(this);
			return LEB128.EncodeSigned(index);
		}
	}


	public abstract class CandidRecordOrVariantType : CandidCompoundType
	{
		public override abstract CandidTypeCode Type { get; }
		protected abstract string TypeString { get; }

		public IReadOnlyDictionary<CandidTag, CandidType> Fields { get; }

		protected CandidRecordOrVariantType(Dictionary<CandidTag, CandidType> fields, CandidId? recursiveId) : base(recursiveId)
		{
			this.Fields = fields;
		}

		internal override byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable)
		{
			byte[] fieldCount = LEB128.EncodeSigned(this.Fields.Count);
			IEnumerable<byte> fieldTypes = this.Fields
				.SelectMany(f =>
				{
					return LEB128.EncodeUnsigned(f.Key.Id)
						.Concat(f.Value.Encode(compoundTypeTable));
				});
			return fieldCount
				.Concat(fieldTypes)
				.ToArray(); ;
		}

		internal override IEnumerable<CandidType> GetInnerTypes()
		{
			return this.Fields.Values;
		}

		public override bool Equals(object? obj)
		{
			bool exactType = this.GetType() == obj?.GetType();
			if (exactType && obj is CandidRecordOrVariantType def)
			{
				if (this.Fields.Count != def.Fields.Count)
				{
					return false;
				}
				foreach ((CandidTag fLabel, CandidType fDef) in this.Fields)
				{
					if (!def.Fields.TryGetValue(fLabel, out CandidType? otherFDef))
					{
						return false;
					}
					if (fDef != otherFDef)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Fields);
		}
	}
}
