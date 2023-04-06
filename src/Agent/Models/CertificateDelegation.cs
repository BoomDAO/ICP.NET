using EdjCase.ICP.Candid.Models;
using System;
using System.Formats.Cbor;
using System.Linq;

namespace EdjCase.ICP.Agent.Models
{

	/// <summary>
	/// A model that contains a certificate proving the delegation of a subnet for the subnet certificate
	/// </summary>
	public class CertificateDelegation
	{
		/// <summary>
		/// The principal of the subnet being delegated to
		/// </summary>
		public Principal SubnetId { get; }
		/// <summary>
		/// The signed certificate that is signed by the delegator
		/// </summary>
		public Certificate Certificate { get; }

		/// <param name="subnetId">The principal of the subnet being delegated to</param>
		/// <param name="certificate">The signed certificate that is signed by the delegator</param>
		/// <exception cref="ArgumentNullException">Throws if either `subnetId` or `certificate` are null</exception>
		public CertificateDelegation(Principal subnetId, Certificate certificate)
		{
			this.SubnetId = subnetId ?? throw new ArgumentNullException(nameof(subnetId));
			this.Certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
		}

		/// <summary>
		/// Gets the public key value from the hash tree in the certificate
		/// </summary>
		/// <returns>The delegation public key for the subnet</returns>
		/// <exception cref="InvalidOperationException">Throws if certificate is missing `subnet/{subnet_id}/public_key`</exception>
		public byte[] GetPublicKey()
		{
			StatePath path = StatePath.FromSegments(
				"subnet",
				this.SubnetId.Raw,
				"public_key"
			);
			HashTree? publicKey = this.Certificate.Tree.GetValueOrDefault(path);
			if (publicKey == null)
			{
				throw new InvalidCertificateException("Certificate does not contain the subnet public key");
			}
			return publicKey.AsLeaf();
		}

		/// <summary>
		/// Checks if the Certificate signature is valid and
		/// outputs the public key of the delegation
		/// </summary>
		/// <param name="publicKey">The public key of the delegation</param>
		/// <returns>True if the certificate signature is valid, otherwise false</returns>
		public bool IsValid(out byte[] publicKey)
		{
			publicKey = this.GetPublicKey();
			return this.Certificate.IsValid(publicKey);
		}

		internal static CertificateDelegation ReadCbor(CborReader reader)
		{
			Principal? subnetId = null;
			Certificate? certificate = null;

			reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				string field = reader.ReadTextString();

				switch (field)
				{
					case "subnet_id":
						var prinBytes = reader.ReadByteString()!;
						subnetId = Principal.FromBytes(prinBytes.ToArray());
						break;
					case "certificate":
						var certBytes = reader.ReadByteString()!;
						var certReader = new CborReader(certBytes);
						certificate = Certificate.ReadCbor(certReader);
						break;
					default:
						//skip
						reader.SkipValue();
						break;
				}
			}
			reader.ReadEndMap();

			if (subnetId == null)
			{
				throw new CborContentException("Missing field: subnet_id");
			}
			if (certificate == null)
			{
				throw new CborContentException("Missing field: certificate");
			}

			return new CertificateDelegation(subnetId, certificate);
		}
	}

}
