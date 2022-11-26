using EdjCase.ICP.Candid.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class Blob
	{
		public byte[] Value { get; set; }
		public Blob(byte[] value)
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

		public static implicit operator byte[](Blob blob)
		{
			return blob.Value;
		}

		public static implicit operator Blob(byte[] bytes)
		{
			return new Blob(bytes);
		}
	}

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

		public (Blob Label, HashTree Tree) AsLabeled()
		{
			this.ValidateType(NodeType.Labeled);
			return ((Blob, HashTree))this.value!;
		}

		public Blob AsLeaf()
		{
			this.ValidateType(NodeType.Leaf);
			return (Blob)this.value!;
		}

		public Blob AsPruned()
		{
			this.ValidateType(NodeType.Pruned);
			return (Blob)this.value!;
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

		public static HashTree Labeled(string label, HashTree tree)
		{
			byte[] labelBytes = Encoding.UTF8.GetBytes(label);
			return HashTree.Labeled(labelBytes, tree);
		}

		public static HashTree Labeled(byte[] label, HashTree tree)
		{
			return new HashTree(NodeType.Labeled, (new Blob(label), tree));
		}
		public static HashTree Leaf(byte[] value)
		{
			return new HashTree(NodeType.Leaf, value);
		}

		public static HashTree Pruned(byte[] blob)
		{
			return new HashTree(NodeType.Pruned, blob);
		}

		public HashTree? GetValue(Path path)
		{
			if (!path.Segments.Any())
			{
				return this;
			}
			HashTree currentTree = this;
			foreach (PathSegment segment in path.Segments)
			{
				HashTree newTree;
				switch (currentTree.Type)
				{
					case NodeType.Leaf:
						return null;
					case NodeType.Labeled:
						(Blob label, HashTree tree) = currentTree.AsLabeled();
						bool areEqual = label.Value.AsSpan().SequenceEqual(segment.Value);
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
						HashTree? l = left.GetValue(path);
						if (l != null)
						{
							newTree = l;
							break;
						}
						HashTree? r = right.GetValue(path);
						if (r != null)
						{
							newTree = r;
							break;
						}
						return null;
					default:
						throw new NotImplementedException();

				}
				currentTree = newTree;
			}
			return currentTree;
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
