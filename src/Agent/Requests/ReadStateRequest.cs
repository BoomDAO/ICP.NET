using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;

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
		public string REQUEST_TYPE { get; } = "query";

		/// <summary>
		/// A list of paths to different state data to obtain. If not specified, data will be pruned and 
		/// be unavailable in the response
		/// </summary>
		public List<StatePath> Paths { get; }

		/// <summary>
		/// The user who is sending the request
		/// </summary>
		public Principal Sender { get; }

		/// <summary>
		/// The expiration of the request to avoid replay attacks
		/// </summary>
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


		internal void WriteCbor(CborWriter writer)
		{
			writer.WriteStartMap(null);

			writer.WriteTextString(Properties.REQUEST_TYPE);
			writer.WriteTextString("read_state");

			writer.WriteTextString(Properties.PATHS);
			writer.WriteArray(this.Paths, CborWriterExtensions.WriteStatePath);

			writer.WriteTextString(Properties.SENDER);
			writer.WritePrincipal(this.Sender);

			writer.WriteTextString(Properties.INGRESS_EXPIRY);
			writer.WriteTimestamp(this.IngressExpiry);

			writer.WriteEndMap();
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