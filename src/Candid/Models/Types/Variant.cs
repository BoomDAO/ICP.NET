using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// A model representing a type definition of a candid variant
	/// </summary>
	public class CandidVariantType : CandidRecordOrVariantType
	{
		/// <inheritdoc />
		public override CandidTypeCode Type { get; } = CandidTypeCode.Variant;

		/// <inheritdoc />
		protected override string TypeString { get; } = "variant";

		/// <summary>
		/// All the potential options and the option types for a variant
		/// </summary>
		public Dictionary<CandidTag, CandidType> Options { get; }

		/// <param name="options">All the potential options and the option types for a variant</param>
		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
		public CandidVariantType(Dictionary<CandidTag, CandidType> options, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Options = options;
		}

		/// <inheritdoc />
		protected override Dictionary<CandidTag, CandidType> GetFieldsOrOptions()
		{
			return this.Options;
		}
	}
}
