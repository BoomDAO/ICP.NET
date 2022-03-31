using Dfinity.Common.Crypto;
using Dfinity.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfinity.Agent.Auth
{
    public class AnonymousIdentity : IIdentity
    {
        public PrincipalId Principal { get; } = PrincipalId.Anonymous();

        public SignedContent CreateSignedContent(Dictionary<string, IHashable> request)
        {
            return new SignedContent(request, null, null);
        }
    }
}
