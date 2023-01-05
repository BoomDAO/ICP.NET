using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models.Types
{
	public class CandidServiceType : CandidCompoundType
	{
		public override CandidTypeCode Type { get; } = CandidTypeCode.Service;

		public CandidId? Id { get; }
		public Dictionary<CandidId, CandidFuncType> Methods { get; }


		public CandidServiceType(Dictionary<CandidId, CandidFuncType> methods, CandidId? id, CandidId? recursiveId = null) : base(recursiveId)
		{
			this.Methods = methods;
			this.Id = id;
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

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Methods);
		}
	}
}
