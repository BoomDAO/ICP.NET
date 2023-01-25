using Dahomey.Cbor.Attributes;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Requests
{
	public class ReadStateRequest : IRepresentationIndependentHashItem
	{
		[CborProperty(Properties.REQUEST_TYPE)]
		public string REQUEST_TYPE { get; } = "query";

		[CborProperty(Properties.PATHS)]
		public List<StatePath> Paths { get; }

		[CborProperty(Properties.SENDER)]
		public Principal Sender { get; }

		[CborProperty(Properties.INGRESS_EXPIRY)]
		public ICTimestamp IngressExpiry { get; }

		public ReadStateRequest(List<StatePath> paths, Principal sender, ICTimestamp ingressExpiry)
		{
			this.Paths = paths ?? throw new ArgumentNullException(nameof(paths));
			this.IngressExpiry = ingressExpiry ?? throw new ArgumentNullException(nameof(ingressExpiry));
			this.Sender = sender ?? throw new ArgumentNullException(nameof(sender));
		}

		public Dictionary<string, IHashable> BuildHashableItem()
		{
			return new Dictionary<string, IHashable>
			{
				{Properties.REQUEST_TYPE, "read_state".ToHashable()},
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