using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent;
using Fido2Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{
	public class WebAuthnIdentity : SigningIdentityBase
	{
		public DeviceInfo Device { get; }
		public WebAuthnOptions SignerOptions { get; }

		public WebAuthnIdentity(DeviceInfo device, WebAuthnOptions? signerOptions = null)
		{
			this.Device = device ?? throw new ArgumentNullException(nameof(device));
			this.SignerOptions = signerOptions ?? WebAuthnOptions.Default;
		}

		public override DerEncodedPublicKey GetPublicKey() => this.Device.PublicKey;


		public override async Task<byte[]> SignAsync(byte[] sign)
		{
			using var assert = new FidoAssertion();
			IEnumerable<DeviceInfo> devices = Enumerable.Repeat(this.Device, 1);
			return await WebAuthnIdentitySigner.Fido2Assert(sign, assert, this.SignerOptions, devices);
		}
	}
}
