using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// An identity that is anonymous.
	/// Should be used if there is no user context or the user hasn't logged in
	/// </summary>
	public class AnonymousIdentity : IIdentity
	{
		/// <inheritdoc/>
		public SignedContent SignContent(Dictionary<string, IHashable> request)
		{
			return new SignedContent(request, null, null, null);
		}

		/// <inheritdoc/>
		public Principal GetPrincipal() => Principal.Anonymous();
	}
}
