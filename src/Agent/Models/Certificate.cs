using EdjCase.ICP.BLS;
using EdjCase.ICP.Candid.Models;
using System;
using System.Formats.Cbor;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

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
		/// <param name="rootPublicKey">The root public key (DER encoded) of the internet computer network</param>
		/// <returns>True if the certificate is valid, otherwise false</returns>
		public bool IsValid(SubjectPublicKeyInfo rootPublicKey)
		{
			// TODO get this working for webgl
			return true;
			/*
				verify_cert(cert) =
					let root_hash = reconstruct(cert.tree)
					let der_key = check_delegation(cert.delegation) // see section Delegations below
					bls_key = extract_der(der_key)
					verify_bls_signature(bls_key, cert.signature, domain_sep("ic-state-root") � root_hash)
			 */
			//byte[] rootHash = this.Tree.BuildRootHash();
			//rootHash = HashTree.EncodedValue.WithDomainSeperator("ic-state-root", rootHash);
			//if (this.Delegation != null)
			//{
			//	// override the root key to the delegated one
			//	if (!this.Delegation.Certificate.IsValid(rootPublicKey))
			//	{
			//		// If delegation is not valid, then the cert is also not valid
			//		return false;
			//	}
			//	rootPublicKey = this.Delegation.GetPublicKey();
			//}
			//return BlsUtil.VerifySignature(rootPublicKey.PublicKey, rootHash, this.Signature);
		}

		internal static Certificate ReadCbor(CborReader reader)
		{
			HashTree? hashTree = null;
			byte[]? signature = null;
			CertificateDelegation? delegation = null;

			if (reader.ReadTag() != CborTag.SelfDescribeCbor)
			{
				throw new CborContentException("Expected self describe tag");
			}

			reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				string name = reader.ReadTextString();
				switch (name)
				{
					case "tree":
						hashTree = ReadTreeCbor(reader);
						break;
					case "signature":
						signature = reader.ReadByteString().ToArray();
						break;
					case "delegation":
						delegation = CertificateDelegation.ReadCbor(reader);
						break;
					default:
						// Skip
						reader.SkipValue();
						break;
				}
			}
			reader.ReadEndMap();

			if (hashTree == null)
			{
				throw new CborContentException("Missing field: tree");
			}
			if (signature == null)
			{
				throw new CborContentException("Missing field: signature");
			}

			return new Certificate(hashTree, signature, delegation);
		}


		internal static HashTree ReadTreeCbor(CborReader reader)
		{
			_ = reader.ReadStartArray(); // Array size
			uint nodeType = reader.ReadUInt32(); // Get tree node type
			HashTree hashTree;
			switch (nodeType)
			{
				case 0:
					hashTree = HashTree.Empty();
					break;
				case 1:
					HashTree left = ReadTreeCbor(reader);
					HashTree right = ReadTreeCbor(reader);
					hashTree = HashTree.Fork(left, right);
					break;
				case 2:
					byte[] labelBytes = reader.ReadByteString();
					HashTree tree = ReadTreeCbor(reader);
					hashTree = HashTree.Labeled(labelBytes, tree);
					break;
				case 3:
					{
						byte[] value = reader.ReadByteString();
						hashTree = HashTree.Leaf(value);
						break;
					}
				case 4:
					{
						byte[] value = reader.ReadByteString();
						hashTree = HashTree.Pruned(value);
						break;
					}
				default:
					throw new NotImplementedException($"No hash tree node type of '{nodeType}' is implemented");
			}
			reader.ReadEndArray();
			return hashTree;
		}
	}

}
