using ICP.Common.Crypto;

namespace ICP.Common.Models
{
    public class EncodedArgument : IHashable
    {
        public byte[] Value { get; }

		public EncodedArgument(byte[] value)
		{
			this.Value = value;
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
        {
            return hashFunction.ComputeHash(this.Value);
        }
    }
}