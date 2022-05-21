using System;
using System.Diagnostics;

namespace EdjCase.ICP.Candid.Crypto
{
    internal abstract class BlockHash
    {

        protected abstract void Finish();
        protected abstract void TransformBlock(byte[] a_data, int a_index);
        protected abstract byte[] GetResult();
    }
}
