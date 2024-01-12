using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.BLS
{
	/// <summary>
	/// Bls cryptography class that AWLAYS returns TRUE. This is intended only for
	/// development scenarios and is never recommended
	/// </summary>
	public class BypassedBlsCryptography : IBlsCryptography
	{
		/// <inheritdoc/>
		public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
		{
			return true;
		}
	}
}
