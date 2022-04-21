using ICP.Common.Encodings;
using ICP.Common.Models;
using System.Linq;

namespace ICP.Common.Candid
{
	public class CandidPrincipal : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Principal;
		private PrincipalId Value { get; }

		public CandidPrincipal(PrincipalId principalId)
		{
			this.Value = principalId;
		}

		public override byte[] EncodeValue()
		{
			byte[] value = this.Value.Raw;
			byte[] encodedValueLength = LEB128.FromUInt64((ulong)value.Length).Raw;
			return new byte[] { 1 }
				.Concat(encodedValueLength)
				.Concat(value)
				.ToArray();
		}
	}
}