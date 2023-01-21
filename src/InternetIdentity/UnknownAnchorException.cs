using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.InternetIdentity
{
	public class UnknownAnchorException : System.Exception
	{
		public ulong Anchor { get; }

		public UnknownAnchorException(ulong anchor)
		{
			this.Anchor = anchor;
		}

		public override string Message => $"Unknown anchor '{this.Anchor}'";
	}
}
