using Dahomey.Cbor.Attributes;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Requests
{
	/// <summary>
	/// A model for making a read state request to a canister
	/// </summary>
	public class ReadStateRequest : IRepresentationIndependentHashItem
	{
		/// <summary>
		/// The type of request to send. Will always be 'query'
		/// </summary>
		[CborProperty(Properties.REQUEST_TYPE)]
		public string REQUEST_TYPE { get; } = "query";

		/// <summary>
		/// A list of paths to different state data to obtain. If not specified, data will be pruned and 
		/// be unavailable in the response
		/// </summary>
		[CborProperty(Properties.PATHS)]
		public List<StatePath> Paths { get; }

		/// <summary>
		/// The user who is sending the request
		/// </summary>
		[CborProperty(Properties.SENDER)]
		public Principal Sender { get; }

		/// <summary>
		/// The expiration of the request to avoid replay attacks
		/// </summary>
		[CborProperty(Properties.INGRESS_EXPIRY)]
		public ICTimestamp IngressExpiry { get; }

		/// <param name="paths">A list of paths to different state data to obtain. If not specified, data will be pruned and 
		/// be unavailable in the response</param>
		/// <param name="sender">The user who is sending the request</param>
		/// <param name="ingressExpiry">The expiration of the request to avoid replay attacks</param>
		public ReadStateRequest(
			List<StatePath> paths,
			Principal sender,
			ICTimestamp ingressExpiry
		)
		{
			this.Paths = paths ?? throw new ArgumentNullException(nameof(paths));
			this.IngressExpiry = ingressExpiry ?? throw new ArgumentNullException(nameof(ingressExpiry));
			this.Sender = sender ?? throw new ArgumentNullException(nameof(sender));
		}

		/// <inheritdoc />
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