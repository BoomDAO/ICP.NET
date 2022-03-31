using System;
using ICP.Common.Models;

namespace ICP.Common.Models
{
    public interface IPublicKey
    {
        DerEncodedPublicKey GetDerEncodedBytes();
    }
}