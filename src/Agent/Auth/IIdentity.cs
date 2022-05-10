using ICP.Candid.Models;
using System.Collections.Generic;

namespace ICP.Agent.Auth
{
	public interface IIdentity
	{
		Principal Principal { get; }

		SignedContent CreateSignedContent(Dictionary<string, IHashable> content);
	}
}