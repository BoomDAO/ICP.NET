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
		public SubjectPublicKeyInfo PublicKey { get; }

		/// <summary>
		/// Optional. The credential id bytes for the device
		/// </summary>
		public byte[]? CredentialId { get; }


		public DeviceInfo(SubjectPublicKeyInfo publicKey, byte[]? credentialId)
		{
			this.PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
			this.CredentialId = credentialId;
		}
	}
}
