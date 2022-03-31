using System;
using Dfinity.Common.Models;

namespace Dfinity.Common.Models
{
    public interface IPublicKey
    {
        DerEncodedPublicKey GetDerEncodedBytes();
    }
}