using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.BLS
{
	/// <summary>
	/// An interface for all BLS crytography operations
	/// </summary>
	public interface IBlsCryptography
	{

		/// <summary>
		/// Verifies a BLS signature (ICP flavor only)
		/// </summary>
		/// <param name="publicKey">The signer public key</param>
		/// <param name="messageHash">The SHA256 hash of the message</param>
		/// <param name="signature">The signature of the message</param>
		/// <returns>True if the signature is valid, otherwise false</returns>
		bool VerifySignature(
			byte[] publicKey,
			byte[] messageHash,
			byte[] signature
		);
	}
}
