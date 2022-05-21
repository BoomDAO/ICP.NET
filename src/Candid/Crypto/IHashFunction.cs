using System;
using System.Security.Cryptography;

namespace EdjCase.ICP.Candid.Crypto
{
    public interface IHashFunction
    {
        public byte[] ComputeHash(byte[] value);
    }

    public class SHA256HashFunction : IHashFunction
    {
        public SHA256 Inner { get; }

        public SHA256HashFunction(SHA256 inner)
        {
            this.Inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public byte[] ComputeHash(byte[] value)
        {
            return this.Inner.ComputeHash(value);
        }

        public static implicit operator SHA256HashFunction(SHA256 inner)
        {
            return new SHA256HashFunction(inner);
        }

        public static SHA256HashFunction Create()
        {
            return new SHA256HashFunction(SHA256.Create());
        }
    }
}