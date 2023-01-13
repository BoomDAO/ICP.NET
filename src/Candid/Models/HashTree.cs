using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class HashTree
	{
		public NodeType Type { get; }
		private object? value { get; }

		private HashTree(NodeType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public (HashTree Left, HashTree Right) AsFork()
		{
			this.ValidateType(NodeType.Fork);
			return ((HashTree, HashTree))this.value!;
		}

		public (EncodedValue Label, HashTree Tree) AsLabeled()
		{
			this.ValidateType(NodeType.Labeled);
			return ((EncodedValue, HashTree))this.value!;
		}

		public EncodedValue AsLeaf()
		{
			this.ValidateType(NodeType.Leaf);
			return (EncodedValue)this.value!;
		}

		public EncodedValue AsPruned()
		{
			this.ValidateType(NodeType.Pruned);
			return (EncodedValue)this.value!;
		}

		private void ValidateType(NodeType nodeType)
		{
			if (this.Type != nodeType)
			{
				throw new InvalidOperationException($"Node type '{this.Type}' cannot be cast as '{nodeType}'");
			}
		}






		public static HashTree Empty()
		{
			return new HashTree(NodeType.Empty, null);
		}

		public static HashTree Fork(HashTree left, HashTree right)
		{
			return new HashTree(NodeType.Fork, (left, right));
		}

		public static HashTree Labeled(EncodedValue label, HashTree tree)
		{
			return new HashTree(NodeType.Labeled, (label, tree));
		}
		public static HashTree Leaf(EncodedValue value)
		{
			return new HashTree(NodeType.Leaf, value);
		}

		public static HashTree Pruned(EncodedValue blob)
		{
			return new HashTree(NodeType.Pruned, blob);
		}

		public HashTree? GetValue(StatePathSegment path)
		{
			return this.GetValue(new StatePath(new List<StatePathSegment> { path }));
		}

		public HashTree? GetValue(StatePath path)
		{
			if (!path.Segments.Any())
			{
				return this;
			}
			HashTree currentTree = this;
			for (int i = 0; i < path.Segments.Count; i++)
			{
				StatePathSegment segment = path.Segments[i];
				HashTree newTree;
				switch (currentTree.Type)
				{
					case NodeType.Leaf:
						return null;
					case NodeType.Labeled:
						(EncodedValue label, HashTree tree) = currentTree.AsLabeled();
						bool areEqual = label == segment.Value;
						if (!areEqual)
						{
							return null;
						}
						newTree = tree;
						break;
					case NodeType.Pruned:
						return null;
					case NodeType.Empty:
						return null;
					case NodeType.Fork:
						(HashTree left, HashTree right) = currentTree.AsFork();
						var remainingPath = new StatePath(path.Segments.Skip(i));
						HashTree? l = left.GetValue(remainingPath);
						if (l != null)
						{
							return l;
						}
						HashTree? r = right.GetValue(remainingPath);
						if (r != null)
						{
							return r;
						}
						return null;
					default:
						throw new NotImplementedException();

				}
				currentTree = newTree;
			}
			return currentTree;
		}

		/// <summary>
		/// Computes the root SHA256 hash of the tree based on the IC certificate spec
		/// </summary>
		/// <returns>A blob of the hash digest</returns>
		public EncodedValue BuildRootHash()
		{
			/*
				verify_cert(cert) =
					let root_hash = reconstruct(cert.tree)
					let der_key = check_delegation(cert.delegation) // see section Delegations below
					bls_key = extract_der(der_key)
					verify_bls_signature(bls_key, cert.signature, domain_sep("ic-state-root") · root_hash)

				reconstruct(Empty)       = H(domain_sep("ic-hashtree-empty"))
				reconstruct(Fork t1 t2)  = H(domain_sep("ic-hashtree-fork") · reconstruct(t1) · reconstruct(t2))
				reconstruct(Labeled l t) = H(domain_sep("ic-hashtree-labeled") · l · reconstruct(t))
				reconstruct(Leaf v)      = H(domain_sep("ic-hashtree-leaf") · v)
				reconstruct(Pruned h)    = h

				domain_sep(s) = byte(|s|) · s
			 */
			SHA256HashFunction hashFunction = new(SHA256.Create());
			EncodedValue rootHash = this.BuildHashInternal(hashFunction);
			return EncodedValue.WithDomainSeperator("ic-state-root", rootHash);
		}

		private EncodedValue BuildHashInternal(SHA256HashFunction hashFunction)
		{
			EncodedValue encodedValue;
			switch (this.Type)
			{
				case NodeType.Empty:
					encodedValue = EncodedValue.WithDomainSeperator("ic-hashtree-empty");
					break;
				case NodeType.Fork:
					(HashTree left, HashTree right) = this.AsFork();
					EncodedValue leftHash = left.BuildHashInternal(hashFunction);
					EncodedValue rightHash = right.BuildHashInternal(hashFunction);
					encodedValue = EncodedValue.WithDomainSeperator("ic-hashtree-fork", leftHash, rightHash);
					break;
				case NodeType.Labeled:
					(EncodedValue label, HashTree tree) = this.AsLabeled();
					EncodedValue treeHash = tree.BuildHashInternal(hashFunction);
					encodedValue = EncodedValue.WithDomainSeperator("ic-hashtree-labeled", label, treeHash);
					break;
				case NodeType.Leaf:
					EncodedValue leaf = this.AsLeaf();
					encodedValue = EncodedValue.WithDomainSeperator("ic-hashtree-leaf", leaf);
					break;
				case NodeType.Pruned:
					encodedValue = this.AsPruned().Value;
					break;
				default:
					throw new NotImplementedException("Node type: " + this.Type);
			}
			return hashFunction.ComputeHash(encodedValue.Value);
		}


		public class EncodedValue : IEquatable<EncodedValue>
		{
			public byte[] Value { get; }
			public EncodedValue(byte[] value)
			{
				this.Value = value;
			}

			public string AsUtf8()
			{
				return Encoding.UTF8.GetString(this.Value);
			}

			public UnboundedUInt AsNat()
			{
				return LEB128.DecodeUnsigned(this.Value);
			}

			public override string ToString()
			{
				return this.AsUtf8();
			}

			public override bool Equals(object? obj)
			{
				if (obj is EncodedValue b)
				{
					return this.Equals(b);
				}
				if (obj is string s)
				{
					return this.AsUtf8() == s;
				}
				if (obj is byte[] by)
				{
					return this.Equals(by);
				}
				return false;
			}

			public bool Equals(EncodedValue? other)
			{
				return this.Equals(other?.Value);
			}

			public bool Equals(byte[]? other)
			{
				if (object.ReferenceEquals(other, null))
				{
					return false;
				};
				return this.Value.AsSpan().SequenceEqual(other);
			}
			public override int GetHashCode()
			{
				return this.Value.GetHashCode();
			}

			public static bool operator ==(EncodedValue? v1, EncodedValue? v2)
			{
				if (object.ReferenceEquals(v1, null))
				{
					return object.ReferenceEquals(v2, null);
				}
				return v1.Equals(v2);
			}

			public static bool operator !=(EncodedValue? v1, EncodedValue? v2)
			{
				if (object.ReferenceEquals(v1, null))
				{
					return object.ReferenceEquals(v2, null);
				}
				return !v1.Equals(v2);
			}



			public static EncodedValue WithDomainSeperator(string value, params EncodedValue[] encodedValues)
			{
				// domain_sep(s) = byte(|s|) · s
				byte[] textBytes = Encoding.UTF8.GetBytes(value);
				byte[] bytes = new byte[textBytes.Length + 1 + encodedValues.Sum(v => v.Value.Length)];
				bytes[0] = (byte)textBytes.Length;
				int index = 1;
				textBytes.CopyTo(bytes, index);
				index++;
				foreach (EncodedValue encodedValue in encodedValues)
				{
					encodedValue.Value.CopyTo(bytes, index);
					index += encodedValue.Value.Length;
				}
				return new EncodedValue(bytes);
			}




			public static implicit operator byte[](EncodedValue blob)
			{
				return blob.Value;
			}

			public static implicit operator EncodedValue(byte[] bytes)
			{
				return new EncodedValue(bytes);
			}
		}
	}

	public enum NodeType
	{
		Empty,
		Fork,
		Labeled,
		Leaf,
		Pruned
	}
}
