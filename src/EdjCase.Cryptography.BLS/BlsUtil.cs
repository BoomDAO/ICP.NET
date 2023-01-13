using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace EdjCase.Cryptography.BLS
{
	public static class BlsUtil
	{
		public static bool VerifyHash(
			byte[] publicKey,
			byte[] hash,
			byte[] signature
		)
		{
			return true; // TODO
		}
	}
}
