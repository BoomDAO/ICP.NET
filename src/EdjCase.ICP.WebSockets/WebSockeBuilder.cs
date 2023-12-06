using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.WebSockets
{
	public class WebSockeBuilder
	{

		public WebSockeBuilder(
			Principal canisterId,
			string networkUrl,
			IIdentity? identity = null,
			int? ackMessageTimeout = 450000,
			int? maxCertificateAgeInMinutes = 5,
			Func<OnCloseContext, Task>? onClose = null,
			Func<OnErrorContext, Task>? onError = null,
			Func<OnMessageContext, Task>? onMessage = null,
			Func<OnOpenContext, Task>? onOpen = null
		)
		{
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
			this.NetworkUrl = networkUrl ?? throw new ArgumentNullException(nameof(networkUrl));
			this.Identity = identity;
			this.AckMessageTimeout = ackMessageTimeout;
			this.MaxCertificateAgeInMinutes = maxCertificateAgeInMinutes;
			this.OnClose = onClose;
			this.OnError = onError;
			this.OnMessage = onMessage;
			this.OnOpen = onOpen;
		}
	}

	public class OnCloseContext
	{

	}

	public class OnErrorContext
	{

	}

	public class OnMessageContext
	{

	}

	public class OnOpenContext
	{

	}
}
