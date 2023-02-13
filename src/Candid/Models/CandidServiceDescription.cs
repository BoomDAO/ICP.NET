using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A model representing a `*.did` file with the definition of the candid 
	/// service and the types associated to it
	/// </summary>
	public class CandidServiceDescription
	{
		/// <summary>
		/// Optional. The id given to the service
		/// </summary>
		public CandidId? ServiceReferenceId { get; }

		/// <summary>
		/// The type information of the service
		/// </summary>
		public CandidServiceType Service { get; }

		/// <summary>
		/// The types declared outside of the service definition
		/// </summary>
		public Dictionary<CandidId, CandidType> DeclaredTypes { get; }

		/// <param name="service">The type information of the service</param>
		/// <param name="declaredTypes">The types declared outside of the service definition</param>
		/// <param name="serviceReferenceId">Optional. The id given to the service</param>
		public CandidServiceDescription(
			CandidServiceType service,
			Dictionary<CandidId, CandidType> declaredTypes,
			CandidId? serviceReferenceId = null
		)
		{
			this.ServiceReferenceId = serviceReferenceId;
			this.Service = service ?? throw new ArgumentNullException(nameof(service));
			this.DeclaredTypes = declaredTypes ?? throw new ArgumentNullException(nameof(declaredTypes));
		}

		/// <summary>
		/// Parse the service defintion from a `*.did` file contents
		/// </summary>
		/// <param name="text">The contents of the `*.did` file</param>
		/// <returns>The parsed candid service defintion</returns>
		public static CandidServiceDescription Parse(string text)
		{
			return CandidServiceFileParser.Parse(text);
		}
	}
}
