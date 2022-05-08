using ICP.Candid.Crypto;
using ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Agent.Auth
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
