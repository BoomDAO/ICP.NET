using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// Identity to use for requests to Internet Computer canisters
	/// </summary>
	public interface IIdentity
	{
		/// <summary>
		/// Gets the principal for this identity
		/// </summary>
		/// <returns>The identity principal</returns>
		Principal GetPrincipal();

		/// <summary>
		/// Signs the hashable content
		/// </summary>
		/// <param name="content">The data that needs to be signed</param>
		/// <returns>The content with signature(s) from the identity</returns>
		Task<SignedContent> SignContentAsync(Dictionary<string, IHashable> content);
	}
}