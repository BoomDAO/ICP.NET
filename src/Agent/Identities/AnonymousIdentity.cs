using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// An identity that is anonymous.
	/// Should be used if there is no user context or the user hasn't logged in
	/// </summary>
	public class AnonymousIdentity : IIdentity
	{
		/// <inheritdoc/>
		public Task<SignedContent> SignContentAsync(Dictionary<string, IHashable> request)
		{
			return Task.FromResult(new SignedContent(request, null, null, null));
		}

		/// <inheritdoc/>
		public Principal GetPrincipal() => Principal.Anonymous();
	}
}
