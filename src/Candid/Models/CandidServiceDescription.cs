using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class CandidServiceDescription
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
