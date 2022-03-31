using Dfinity.Common.Crypto;
using System;

namespace Dfinity.Common.Models
{
    public class Path : IHashable
    {
		public byte[] Value { get; } // TODO 

        public Path(byte[] value)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public byte[] ComputeHash(IHashFunction hashFunction)
        {
            //TODO 
            throw new NotImplementedException();
        }
    }
}