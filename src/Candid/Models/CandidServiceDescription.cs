using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Parsers;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Candid.Models
{
	internal class CandidServiceDescription
	{
		public CandidId? ServiceReferenceId { get; }
		public CandidServiceType Service { get; }
		public Dictionary<CandidId, CandidType> DeclaredTypes { get; }

		public CandidServiceDescription(
			CandidId? serviceReferenceId,
			CandidServiceType service,
			Dictionary<CandidId, CandidType> declaredTypes)
		{
			this.ServiceReferenceId = serviceReferenceId;
			this.Service = service ?? throw new ArgumentNullException(nameof(service));
			this.DeclaredTypes = declaredTypes ?? throw new ArgumentNullException(nameof(declaredTypes));
		}

		public static CandidServiceDescription Parse(string text)
		{
			return CandidServiceFileParser.Parse(text);
		}
	}
}
