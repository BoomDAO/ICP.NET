using System.Collections.Generic;
using IIClient = EdjCase.ICP.InternetIdentity.Models;
using Fido2Net;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using System.Threading.Tasks;
using System;
using EdjCase.ICP.Agent.Identities;
using System.Linq;
using EdjCase.ICP.Agent;
using EdjCase.ICP.Candid.Models;
using System.Text;

namespace EdjCase.ICP.InternetIdentity
{
	public class WebAuthnOptions
	{
		public TimeSpan Timeout { get; } = TimeSpan.FromSeconds(60.0);

		public static readonly WebAuthnOptions Default = new WebAuthnOptions();
	}
}
