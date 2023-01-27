using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// A candid type that is not primitive or a reference. These types are considered
	/// more complex and have multiple data structures within them
	/// </summary>
	public abstract class CandidCompoundType : CandidKnownType
	{
		/// <summary>
		/// Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id
		/// </summary>
		public CandidId? RecursiveId { get; set; }

		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
		public CandidCompoundType(CandidId? recursiveId)
		{
			this.RecursiveId = recursiveId;
		}

		/// <summary>
		/// Adds all inner types to the compound table if applicable and returns its encoded type value
		/// </summary>
		/// <param name="compoundTypeTable">The collection of compound types for a candid arg</param>
		/// <returns>Byte array of encoded type</returns>
		internal abstract byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable);

		internal abstract IEnumerable<CandidType> GetInnerTypes();

		internal override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			UnboundedUInt index = compoundTypeTable.GetOrAdd(this);
			return LEB128.EncodeSigned(index);
		}
	}

	/// <summary>
	/// A shared class for candid records and variants. Both have a mapping of 
	/// keys with associated types
	/// </summary>
	public abstract class CandidRecordOrVariantType : CandidCompoundType
	{
		/// <inheritdoc />
		public override abstract CandidTypeCode Type { get; }

		/// <summary>
		/// The string name of the parent type (record or variant)
		/// </summary>
		protected abstract string TypeString { get; }

		/// <summary>
		/// Gets the record fields or variant options to be used for encoding
		/// </summary>
		/// <returns></returns>
		protected abstract Dictionary<CandidTag, CandidType> GetFieldsOrOptions();

		/// <inheritdoc />
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

		/// <inheritdoc />
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

		/// <inheritdoc />
		public override int GetHashCode()
		{
			Dictionary<CandidTag, CandidType> fieldsOrOptions = this.GetFieldsOrOptions();
			return HashCode.Combine(this.Type, fieldsOrOptions);
		}
	}
}
