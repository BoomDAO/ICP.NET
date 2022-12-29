using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models.Types
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

		protected abstract Dictionary<CandidTag, CandidType> GetFieldsOrOptions();

		protected CandidRecordOrVariantType(CandidId? recursiveId) : base(recursiveId)
		{

		}

		internal override byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable)
		{
			Dictionary<CandidTag, CandidType> fieldsOrOptions = this.GetFieldsOrOptions();
			byte[] fieldCount = LEB128.EncodeSigned(fieldsOrOptions.Count);
			IEnumerable<byte> fieldTypes = fieldsOrOptions
				.OrderBy(f => f.Key)
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
			return this.GetFieldsOrOptions().Values;
		}

		public override bool Equals(object? obj)
		{
			Dictionary<CandidTag, CandidType> fieldsOrOptions = this.GetFieldsOrOptions();
			bool exactType = this.GetType() == obj?.GetType();
			if (exactType && obj is CandidRecordOrVariantType def)
			{
				if (fieldsOrOptions.Count != fieldsOrOptions.Count)
				{
					return false;
				}
				foreach ((CandidTag fLabel, CandidType fDef) in fieldsOrOptions)
				{
					if (!fieldsOrOptions.TryGetValue(fLabel, out CandidType? otherFDef))
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
			Dictionary<CandidTag, CandidType> fieldsOrOptions = this.GetFieldsOrOptions();
			return HashCode.Combine(this.Type, fieldsOrOptions);
		}
	}
}
