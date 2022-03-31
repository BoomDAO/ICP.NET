using Dfinity.Common.Models;
using System;
using System.Collections.Generic;

namespace Dfinity.Agent.Requests
{
    public class ReadStateRequest : IRepresentationIndependentHashItem
    {
        public string REQUEST_TYPE { get; } = "query";
        public List<Path> Paths { get; }
        public PrincipalId Sender { get; }
        public ICTimestamp IngressExpiry { get; }

        public ReadStateRequest(List<Path> paths, PrincipalId sender, ICTimestamp ingressExpiry)
        {
            this.Paths = paths ?? throw new ArgumentNullException(nameof(paths));
            this.IngressExpiry = ingressExpiry ?? throw new ArgumentNullException(nameof(ingressExpiry));
            this.Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        public Dictionary<string, IHashable> BuildHashableItem()
        {
            return new Dictionary<string, IHashable>
            {
                {Properties.REQUEST_TYPE, "call".ToHashable()},
                {Properties.PATHS, this.Paths.ToHashable()},
                {Properties.SENDER, this.Sender},
                {Properties.INGRESS_EXPIRY, this.IngressExpiry},
            };
        }


        private static class Properties
        {
            public const string REQUEST_TYPE = "request_type";
            public const string PATHS = "paths";
            public const string SENDER = "sender";
            public const string INGRESS_EXPIRY = "ingress_expiry";
        }
    }
}