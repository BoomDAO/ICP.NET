using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICP.Candid.Crypto;

namespace ICP.Candid.Models
{
    public class DerEncodedPublicKey : IHashable, IPublicKey
    {
        public byte[] Value { get; }

        public DerEncodedPublicKey(byte[] value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public byte[] ComputeHash(IHashFunction hashFunction)
        {
            return hashFunction.ComputeHash(this.Value);
        }

		public DerEncodedPublicKey GetDerEncodedBytes()
		{
            return this;
		}
	}
}
