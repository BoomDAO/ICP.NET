using System;
using System.Collections.Generic;
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
			return ((HashTree, HashTree))this.value!;
		}

		public (string Label, HashTree Tree) AsLabeled()
		{
			return ((string, HashTree))this.value!;
		}

		public byte[] AsLeaf()
		{
			return (byte[])this.value!;
		}

		public byte[] AsPruned()
		{
			return (byte[])this.value!;
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
			return new HashTree(NodeType.Labeled, (label, tree));
		}

		public static HashTree Leaf(byte[] value)
		{
			return new HashTree(NodeType.Leaf, value);
		}

		public static HashTree Pruned(byte[] blob)
		{
			return new HashTree(NodeType.Pruned, blob);
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
