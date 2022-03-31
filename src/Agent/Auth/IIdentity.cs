using Candid;
using ICP.Agent.Requests;
using ICP.Common;
using ICP.Common.Crypto;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Agent.Auth
{
    public interface IIdentity
    {
        PrincipalId Principal { get; }

        SignedContent CreateSignedContent(Dictionary<string, IHashable> content);
    }
}