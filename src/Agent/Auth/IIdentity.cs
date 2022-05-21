using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Auth
{
	public interface IIdentity
	{
		Principal Principal { get; }

		SignedContent CreateSignedContent(Dictionary<string, IHashable> content);
	}
}