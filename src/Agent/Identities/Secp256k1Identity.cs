using Cryptography.ECDSA;
using EdjCase.ICP.Agent.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace EdjCase.ICP.Agent.Identities
{
	public class Secp256k1Identity : IIdentity
	{
		/// <summary>
		/// The public key of the identity
		/// </summary>
		public DerEncodedPublicKey PublicKey { get; }

		/// <summary>
		/// The private key of the identity
		/// </summary>
		public byte[] PrivateKey { get; }

		/// <param name="publicKey">The public key of the identity</param>
		/// <param name="privateKey">The private key of the identity</param>
		public Secp256k1Identity(DerEncodedPublicKey publicKey, byte[] privateKey)
		{
			// TODO validate that pub+priv match?
			this.PublicKey = publicKey;
			this.PrivateKey = privateKey;
		}

		public DerEncodedPublicKey GetPublicKey()
		{
			return this.PublicKey;
		}

		public List<SignedDelegation>? GetSenderDelegations()
		{
			return null;
		}

		public byte[] Sign(byte[] data)
		{
			var parameters = new ECParameters
			{
				Curve = ECCurve.NamedCurves.nistP256,
				D = this.PrivateKey
			};
			using (ECDsa ecdsa = ECDsa.Create(parameters))
			{
				return ecdsa.SignData(data, HashAlgorithmName.SHA256);
			}
		}
	}
}
