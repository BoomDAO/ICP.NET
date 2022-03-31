using Candid;
using ICP.Common.Crypto;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Agent.Auth
{
    public abstract class SignIdentity : IIdentity
    {
        public PrincipalId Principal { get; }

        public SignIdentity(PrincipalId principal)
        {
            this.Principal = principal;
        }

        public abstract IPublicKey GetPublicKey();

        public abstract Signature Sign(byte[] sign);

        public SignedContent CreateSignedContent(Dictionary<string, IHashable> content)
        {
            DerEncodedPublicKey senderPublicKey = this.GetPublicKey().GetDerEncodedBytes();
            byte[] domainSeparator = Encoding.UTF8.GetBytes("\x0Aic-request");
            var sha256 = SHA256HashFunction.Create();
            byte[] contentHash = content.ToHashable().ComputeHash(sha256);
            Signature senderSignature = this.Sign(domainSeparator.Concat(contentHash).ToArray());
            return new SignedContent(content, senderPublicKey, senderSignature);
        }
    }
}
