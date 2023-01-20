using EdjCase.ICP.Agent;
using EdjCase.ICP.InternetIdentity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.InternetIdentity
{
	public class DeviceInfo
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

		internal static DeviceInfo FromModel(DeviceData data)
		{
			byte[] publicKey = data.Pubkey;
			byte[]? credentialId = data.CredentialId.GetValueOrDefault();
			return new DeviceInfo(publicKey, credentialId);
		}
	}
}
