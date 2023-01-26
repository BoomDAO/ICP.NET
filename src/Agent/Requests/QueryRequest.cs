using Dahomey.Cbor.Attributes;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Requests
{
	/// <summary>
	/// A model for making query requests to a canister
	/// </summary>
	public class QueryRequest : IRepresentationIndependentHashItem
	{
		/// <summary>
		/// The type of request to send. Will always be 'query'
		/// </summary>
		[CborProperty(Properties.REQUEST_TYPE)]
		public string RequestType { get; } = "query";

		/// <summary>
		/// The principal of the canister to call
		/// </summary>
		[CborProperty(Properties.CANISTER_ID)]
		public Principal CanisterId { get; }

		/// <summary>
		/// Name of the canister method to call
		/// </summary>
		[CborProperty(Properties.METHOD_NAME)]
		public string Method { get; }

		/// <summary>
		/// Arguments to pass to the canister method
		/// </summary>
		[CborProperty(Properties.ARG)]
		public CandidArg Arg { get; }

		/// <summary>
		/// The user who issued the request
		/// </summary>
		[CborProperty(Properties.SENDER)]
		public Principal Sender { get; }

		/// <summary>
		/// An upper limit on the validity of the request, expressed in nanoseconds since 1970-01-01
		/// </summary>
		[CborProperty(Properties.INGRESS_EXPIRY)]
		public ICTimestamp IngressExpiry { get; }

		/// <summary>
		/// Optional. Arbitrary user-provided data, typically randomly generated. 
		/// This can be used to create distinct requests with otherwise identical fields.
		/// </summary>
		[CborIgnoreIfDefault]
		[CborProperty(Properties.NONCE)]
		public byte[]? Nonce { get; }

		/// <param name="canisterId">The principal of the canister to call</param>
		/// <param name="method">Name of the canister method to call</param>
		/// <param name="arg">Arguments to pass to the canister method</param>
		/// <param name="sender">The user who issued the request</param>
		/// <param name="ingressExpiry"> The expiration of the request to avoid replay attacks</param>
		public QueryRequest(
			Principal canisterId,
			string method,
			CandidArg arg,
			Principal sender,
			ICTimestamp ingressExpiry
		)
		{
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
			this.Method = method ?? throw new ArgumentNullException(nameof(method));
			this.Arg = arg ?? throw new ArgumentNullException(nameof(arg));
			this.Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			this.IngressExpiry = ingressExpiry ?? throw new ArgumentNullException(nameof(ingressExpiry));
		}

		/// <inheritdoc />
		public Dictionary<string, IHashable> BuildHashableItem()
		{
			var properties = new Dictionary<string, IHashable>
			{
				{Properties.REQUEST_TYPE, this.RequestType.ToHashable()},
				{Properties.CANISTER_ID, this.CanisterId},
				{Properties.METHOD_NAME, this.Method.ToHashable()},
				{Properties.ARG, this.Arg},
				{Properties.SENDER, this.Sender},
				{Properties.INGRESS_EXPIRY, this.IngressExpiry}
			};
			if (this.Nonce != null)
			{
				properties.Add(Properties.NONCE, this.Nonce.ToHashable());
			}
			return properties;
		}


		private static class Properties
		{
			public const string REQUEST_TYPE = "request_type";
			public const string CANISTER_ID = "canister_id";
			public const string METHOD_NAME = "method_name";
			public const string ARG = "arg";
			public const string SENDER = "sender";
			public const string INGRESS_EXPIRY = "ingress_expiry";
			public const string NONCE = "nonce";
		}
	}
}