using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.BLS.Models
{
	public class BlsException : Exception
	{
		public BlsException(string message, Exception? inner = null) : base(message, inner) { }
	}

	public class SizeMismatchException : BlsException
	{
		public SizeMismatchException() : base("Size mismatch") { }
	}

	public class IoException : BlsException
	{
		public IoException(Exception innerException) : base("Io error", innerException) { }
	}

	public class GroupDecodeException : BlsException
	{
		public GroupDecodeException() : base("Group decode error") { }
	}

	public class CurveDecodeException : BlsException
	{
		public CurveDecodeException() : base("Curve decode error") { }
	}

	public class FieldDecodeException : BlsException
	{
		public FieldDecodeException() : base("Prime field decode error") { }
	}

	public class InvalidPrivateKeyException : BlsException
	{
		public InvalidPrivateKeyException() : base("Invalid Private Key") { }
	}

	public class ZeroSizedInputException : BlsException
	{
		public ZeroSizedInputException() : base("Zero sized input") { }
	}


}
