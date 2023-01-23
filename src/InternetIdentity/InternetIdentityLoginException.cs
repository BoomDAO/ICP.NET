using System;
using System.Runtime.Serialization;

namespace EdjCase.ICP.InternetIdentity
{
	[Serializable]
	internal class InternetIdentityLoginException : Exception
	{
		public ErrorType ErrorType { get; }

		public InternetIdentityLoginException(ErrorType errorType) : base("Internet identity login failed with error: " + errorType)
		{
			this.ErrorType = errorType;
		}
	}
}