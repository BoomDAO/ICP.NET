using System;
using ICP.Candid.Models;

namespace ICP.Candid.Models
{
    public interface IPublicKey
    {
        DerEncodedPublicKey GetDerEncodedBytes();
    }
}