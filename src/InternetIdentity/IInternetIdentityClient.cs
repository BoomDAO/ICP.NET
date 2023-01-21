using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{
	internal interface IInternetIdentityClient
	{
		Principal GetCanisterId();
		Task<List<DeviceInfo>> LookupAsync(ulong anchor);
		Task<DelegationIdentity> PrepareAndGetDelegationAsync(
			ulong anchor,
			string hostname,
			IIdentity deviceIdentity,
			IIdentity sessionIdentity,
			TimeSpan? maxTimeToLive = null
		);
	}

}
