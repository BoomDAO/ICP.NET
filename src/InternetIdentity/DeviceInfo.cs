using EdjCase.ICP.Agent;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.InternetIdentity
{
	internal class DeviceInfo
	{
		/// <summary>
		/// DER encoded public key for the device
		/// </summary>
		public DerEncodedPublicKey PublicKey { get; }

		/// <summary>
		/// Optional. The credential id bytes for the device
		/// </summary>
		public byte[]? CredentialId { get; }

		public DeviceInfo(byte[] publicKey, byte[]? credentialId) : this(DerEncodedPublicKey.FromDer(publicKey), credentialId)
		{

		}

		public DeviceInfo(DerEncodedPublicKey publicKey, byte[]? credentialId)
		{
			this.PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
			this.CredentialId = credentialId;
		}
	}
}
