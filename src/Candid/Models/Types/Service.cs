using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Models.Types
{
	/// <summary>
	/// A model for a candid service type
	/// </summary>
	public class CandidServiceType : CandidCompoundType
	{
		/// <inheritdoc />
		public override CandidTypeCode Type { get; } = CandidTypeCode.Service;

		/// <summary>
		/// A mapping of ids to function types that the service contains
		/// </summary>
		public Dictionary<CandidId, CandidFuncType> Methods { get; }

		/// <param name="methods">A mapping of ids to function types that the service contains</param>
		/// <param name="recursiveId">Optional. Used if this type can be referenced by an inner type recursively.
		/// The inner type will use `CandidReferenceType` with this id</param>
		public CandidServiceType(Dictionary<CandidId, CandidFuncType> methods, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Methods = methods;
		}

		internal override byte[] EncodeInnerTypes(CompoundTypeTable compoundTypeTable)
		{
			byte[] methodCount = LEB128.EncodeSigned(this.Methods.Count);
			IEnumerable<byte> methodTypes = this.Methods
				.OrderBy(m => m.Key) // Ordered by method name
				.SelectMany(m =>
				{
					byte[] encodedName = Encoding.UTF8.GetBytes(m.Key.ToString());
					byte[] encodedNameLength = LEB128.EncodeSigned(encodedName.Length);
					return encodedNameLength
					.Concat(encodedName)
					.Concat(m.Value.Encode(compoundTypeTable));
				});
			return methodCount
				.Concat(methodTypes)
				.ToArray();
		}

		internal override IEnumerable<CandidType> GetInnerTypes()
		{
			return this.Methods.Values;
		}

		/// <inheritdoc />
		public override bool Equals(object? obj)
		{
			if (obj is CandidServiceType sDef)
			{
				return this.Methods
					.OrderBy(s => s.Key)
					.SequenceEqual(sDef.Methods.OrderBy(s => s.Key));
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Methods);
		}
	}
}
