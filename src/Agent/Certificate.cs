using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Agent.Keys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent
{
	/// <summary>
	/// A model that contains a state tree along with a validation signature. If required
	/// the model can have a delegation to allow for subnet data/keys
	/// </summary>
	public class Certificate
	{
		/// <summary>
		/// A partial state tree of the requested state data
		/// </summary>
		public HashTree Tree { get; }

		/// <summary>
		/// A signature on the tree root hash. Used to validate the tree
		/// </summary>
		public byte[] Signature { get; }

		/// <summary>
		/// Optional. A signed delegation that links a public key to the root public key
		/// </summary>
		public CertificateDelegation? Delegation { get; }


		/// <inheritdoc cref="CertificateDelegation.SubnetId"/>
		/// <inheritdoc cref="CertificateDelegation.Certificate"/>
		/// <exception cref="ArgumentNullException">Throws if either `tree` or `signature` are null</exception>
		public Certificate(HashTree tree, byte[] signature, CertificateDelegation? delegation)
		{
			this.Tree = tree ?? throw new ArgumentNullException(nameof(tree));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
			this.Delegation = delegation;
		}

		public bool IsValid(byte[] rootPublicKey)
		{
			HashTree.EncodedValue rootHash = this.Tree.BuildRootHash();
			if (this.Delegation != null)
			{
				// override the root key to the delegated one
				if (!this.Delegation.IsValid(out rootPublicKey))
				{
					// If delegation is not valid, then the cert is also not valid
					return false;
				}
			}
			var blsKey = BlsPublicKey.FromDer(rootPublicKey);
			return blsKey.ValidateSignature(rootHash.Value, this.Signature);
		}
	}

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

		/// <inheritdoc cref="CertificateDelegation.SubnetId"/>
		/// <inheritdoc cref="CertificateDelegation.Certificate"/>
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
			HashTree? publicKey = this.Certificate.Tree.GetValue(path);
			if (publicKey == null)
			{
				throw new InvalidCertificateException("Certificate does not contain the subnet public key");
			}
			return publicKey.AsLeaf();
		}

		/// <summary>
		/// Checks if the Certificate signature is valid
		/// </summary>
		/// <returns>True if the certificate signature is valid, otherwise false</returns>
		public bool IsValid(out byte[] publicKey)
		{
			publicKey = this.GetPublicKey();
			return this.Certificate.IsValid(publicKey);
		}
	}

	public class InvalidCertificateException : Exception
	{
		public InvalidCertificateException(string message) : base(message)
		{

		}
	}
}
