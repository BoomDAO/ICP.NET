using Candid;
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

        public static EncodedArgument FromCandid(CandidToken token)
		{
            byte[] bytes = token.;
            return new EncodedArgument(bytes);
		}
    }
}