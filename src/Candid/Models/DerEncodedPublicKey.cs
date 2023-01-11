namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// Public key type for when you don't care about the particular format and just want to interchange keys between different parties,
	/// for example for serializing keys of unknown type to disk or taking user keys and sending them to the II.
	/// </summary>
	public class DerEncodedPublicKey: IPublicKey
	{
		public DerEncodedPublicKey(byte[] value)
		{
			this._value = value ?? throw new System.ArgumentNullException(nameof(value));
		}

		public byte[] GetDerEncodedBytes()
		{
			return this._value;
		}

		private byte[] _value;
	}
}
