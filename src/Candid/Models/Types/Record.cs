using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// A model for candid record types
	/// </summary>
	public class CandidRecordType : CandidRecordOrVariantType
	{
		/// <inheritdoc />
		public override CandidTypeCode Type { get; } = CandidTypeCode.Record;

		/// <inheritdoc />
		protected override string TypeString { get; } = "record";

		/// <summary>
		/// The collection of field names with the associate type for that field
		/// </summary>
		public Dictionary<CandidTag, CandidType> Fields { get; }

		/// <param name="fields">The collection of field names with the associate type for that field</param>
		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
		public CandidRecordType(Dictionary<CandidTag, CandidType> fields, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Fields = fields;
		}

		/// <inheritdoc />
		protected override Dictionary<CandidTag, CandidType> GetFieldsOrOptions()
		{
			return this.Fields;
		}
	}
}
