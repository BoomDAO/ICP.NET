namespace EdjCase.ICP.Agent.Keys
{
	/// <summary>
	/// A cryptographic public key that can be DER encoded
	/// </summary>
	public interface IPublicKey
	{
		/// <summary>
		/// Gets the DER encoded bytes of the key
		/// </summary>
		/// <returns>DER encoded key bytes</returns>
		byte[] GetDerEncodedBytes();

		/// <summary>
		/// Gets the raw bytes of the key
		/// </summary>
		/// <returns>Key bytes</returns>
		byte[] GetRawBytes();
	}
}