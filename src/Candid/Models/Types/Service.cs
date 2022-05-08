using ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
	public class ServiceCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Service;

		public IReadOnlyDictionary<string, FuncCandidTypeDefinition> Methods { get; }

		public ServiceCandidTypeDefinition(IReadOnlyDictionary<string, FuncCandidTypeDefinition> methods, string? recursiveId = null) : base(recursiveId)
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
					byte[] encodedName = Encoding.UTF8.GetBytes(m.Key);
					byte[] encodedNameLength = LEB128.EncodeSigned(encodedName.Length);
					return encodedNameLength
					.Concat(encodedName)
					.Concat(m.Value.Encode(compoundTypeTable));
				});
			return methodCount
				.Concat(methodTypes)
				.ToArray();
		}

		internal override IEnumerable<CandidTypeDefinition> GetInnerTypes()
		{
			return this.Methods.Values;
		}

		public override bool Equals(object? obj)
		{
			if (obj is ServiceCandidTypeDefinition sDef)
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

		protected override string ToStringInternal()
		{
			IEnumerable<string> methods = this.Methods.Select(f => $"{f.Key}:{f.Value}");
			return $"Service {{{string.Join("; ", methods)}}}";
		}
	}
}
