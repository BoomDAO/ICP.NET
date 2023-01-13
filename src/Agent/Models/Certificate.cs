using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Agent.Keys;
using System;

namespace EdjCase.ICP.Agent.Models
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

		/// <param name="tree">A partial state tree of the requested state data</param>
		/// <param name="signature">A signature on the tree root hash. Used to validate the tree</param>
		/// <param name="delegation">Optional. A signed delegation that links a public key to the root public key</param>
		/// <exception cref="ArgumentNullException">Throws if either `tree` or `signature` are null</exception>
		public Certificate(HashTree tree, byte[] signature, CertificateDelegation? delegation = null)
		{
			this.Tree = tree ?? throw new ArgumentNullException(nameof(tree));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
			this.Delegation = delegation;
		}

		/// <summary>
		/// Checks the validity of the certificate based off the 
		/// specified root public key
		/// </summary>
		/// <param name="rootPublicKey">The root public key of the internet computer network</param>
		/// <returns>True if the certificate is valid, otherwise false</returns>
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

}
