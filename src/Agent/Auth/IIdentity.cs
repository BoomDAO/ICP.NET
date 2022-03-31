using Candid;
using Dfinity.Agent.Requests;
using Dfinity.Common;
using Dfinity.Common.Crypto;
using Dfinity.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfinity.Agent.Auth
{
    public interface IIdentity
    {
        PrincipalId Principal { get; }

        SignedContent CreateSignedContent(Dictionary<string, IHashable> content);
    }
}