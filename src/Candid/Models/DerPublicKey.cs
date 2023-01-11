namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// Public key type which makes no promises about the format.
	/// This is the canonical implementation of IPublicKey.
	/// </summary>
	public class DerPublicKey : IPublicKey
	{
		public byte[] DerEncodedPublicKey;

		public DerPublicKey(byte[] derEncodedPublicKey)
		{
			this.DerEncodedPublicKey = derEncodedPublicKey ?? throw new System.ArgumentNullException(nameof(derEncodedPublicKey));
		}

		public static DerPublicKey FromDer(byte[] derEncodedPublicKey)
		{
			return new DerPublicKey(derEncodedPublicKey);
		}

		public byte[] GetDerEncodedBytes() => this.DerEncodedPublicKey;
	}
}
