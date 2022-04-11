using ICP.Common.Models;
using System;
using System.Linq;

namespace ICP.Common.Candid
{
	public class CandidService : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Service;
		public PrincipalId PrincipalId { get; set; }

		public CandidService(PrincipalId principalId)
		{
			this.PrincipalId = principalId ?? throw new ArgumentNullException(nameof(principalId));
		}

		public override byte[] EncodeValue()
		{
			return new byte[] { 1 }
				.Concat(this.PrincipalId.Raw)
				.ToArray();
		}
	}
}