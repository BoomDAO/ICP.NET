using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Auth
{
	public interface IIdentity
	{
		Principal GetPrincipal();

		Task<SignedContent> CreateSignedContent(Dictionary<string, IHashable> content);
	}
}